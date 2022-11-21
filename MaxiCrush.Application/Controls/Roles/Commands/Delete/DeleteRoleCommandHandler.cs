using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MediatR;

namespace MaxiCrush.Application.Controls.Roles.Commands.Delete;

public class DeleteRoleCommandHandler
    : IRequestHandler<DeleteRoleCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public DeleteRoleCommandHandler(IRoleRepository roleRepository,
                                    IUserRepository userRepository,
                                    IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var senderUser = await _userRepository.GetByIdAsync(request.SenderId);

        if (senderUser == null)
            return Result.Fail(AppErrors.Unexpected);

        var defaultRole = await _roleRepository.GetByNameAsync("User");

        if (defaultRole == null)
            return Result.Fail(AppErrors.Unexpected);

        var role = await _roleRepository.GetByIdAsync(request.RoleId);

        if (role == null)
            return Result.Fail(AppErrors.Roles.NotFound);

        //Before removing this role we have to ensure that this role is not user
        if (role.Name == "User")
            return Result.Fail(AppErrors.Roles.CannotDeleteDefaultRole);

        //Ensure that the role is less than the role of the sender user
        if (senderUser.Role.Power != 999 && senderUser.Role.Power <= role.Power)
            return Result.Fail(AppErrors.Permissions.InsufficientPermission);

        //Ensure that every user that has this role have the user role to prevent them from being removed
        var users = _userRepository.GetAll().Where(x => x.Role.Id == role.Id);
        foreach (var user in users)
        {
            user.Role = defaultRole;
            await _userRepository.UpdateAsync(user);
        }

        await _roleRepository.RemoveAsync(role);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
}

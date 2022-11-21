using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Users.Commands.SetRole;

public class SetUserRoleCommandHandler
    : IRequestHandler<SetUserRoleCommand, Result<User>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public SetUserRoleCommandHandler(IUserRepository userRepository,
                                     IRoleRepository roleRepository,
                                     IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<User>> Handle(SetUserRoleCommand command, CancellationToken cancellationToken)
    {
        var senderUser = await _userRepository.GetByIdAsync(command.SenderId);

        if (senderUser == null)
            return Result.Fail(AppErrors.Users.NotFound);

        var user = await _userRepository.GetByIdAsync(command.TargetUserId);

        if (user == null)
            return Result.Fail(AppErrors.Users.NotFound);

        if (senderUser.Role.Power != 999 && senderUser.Role.Power <= user.Role.Power)
            return Result.Fail(AppErrors.Permissions.InsufficientPermission);

        var role = await _roleRepository.GetByNameAsync(command.Role);

        if (role == null)
            return Result.Fail(AppErrors.Roles.NotFound);

        if (senderUser.Role.Power != 999 && senderUser.Role.Power <= role.Power)
            return Result.Fail(AppErrors.Permissions.InsufficientPermission);

        if (user.Role.Name == role.Name)
            return Result.Fail(AppErrors.Users.RoleAlreadyGiven);

        user.Role = role;

        await _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return user;
    }
}

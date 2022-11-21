using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Roles.Commands.Create;

public class CreateRoleCommandHandler
    : IRequestHandler<CreateRoleCommand, Result<Role>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public CreateRoleCommandHandler(IRoleRepository roleRepository,
                                    IUserRepository userRepository,
                                    IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Role>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var senderUser = await _userRepository.GetByIdAsync(command.SenderId);

        if (senderUser == null)
            return Result.Fail(AppErrors.Users.NotFound);

        var role = await _roleRepository.GetByNameAsync(command.Name);

        if (role != null)
            return Result.Fail(AppErrors.Roles.DuplicateName);

        if (senderUser.Role.Power != 999 && senderUser.Role.Power <= command.Power)
            return Result.Fail(AppErrors.Permissions.InsufficientPermission);

        role = new Role
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Power = command.Power
        };

        await _roleRepository.AddAsync(role);
        await _unitOfWork.SaveChangesAsync();

        return role;
    }
}

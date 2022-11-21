using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;
using System.Runtime.CompilerServices;

namespace MaxiCrush.Application.Controls.Roles.Commands.Update;

public class UpdateRoleCommandHandler
    : IRequestHandler<UpdateRoleCommand, Result<Role>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;

    public UpdateRoleCommandHandler(IRoleRepository roleRepository,
                                    IUnitOfWork unitOfWork,
                                    IUserRepository userRepository,
                                    IPermissionRepository permissionRepository)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _permissionRepository = permissionRepository;
    }

    public async Task<Result<Role>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var senderUser = await _userRepository.GetByIdAsync(request.SenderId);

        if (senderUser == null)
            return Result.Fail(AppErrors.Unexpected);

        var role = await _roleRepository.GetByIdAsync(request.RoleId);

        if (role == null)
            return Result.Fail(AppErrors.Roles.NotFound);

        if (senderUser.Role.Power != 999 && senderUser.Role.Power <= request.Power)
            return Result.Fail(AppErrors.Permissions.InsufficientPermission);

        if (senderUser.Role.Power != 999 && senderUser.Role.Power <= role.Power)
            return Result.Fail(AppErrors.Permissions.InsufficientPermission);

        role.Name = request.Name ?? role.Name;
        role.Power = request.Power ?? role.Power;

        if (request.PermissionIds != null)
        {
            var permissions = new List<Permission>();

            for (int i = 0; i < request.PermissionIds.Length; i++)
            {
                var permission = await _permissionRepository.GetByIdAsync(request.PermissionIds[i]);
                if (permission == null) continue;

                permissions.Add(permission);
            }

            role.Permissions = permissions;
        }

        await _roleRepository.UpdateAsync(role);
        await _unitOfWork.SaveChangesAsync();

        return role;
    }
}

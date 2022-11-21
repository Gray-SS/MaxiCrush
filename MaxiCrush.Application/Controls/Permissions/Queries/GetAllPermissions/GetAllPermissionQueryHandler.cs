using FluentResults;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Permissions.Queries.GetPermission;

public class GetAllPermissionsQueryHandler
    : IRequestHandler<GetAllPermissionsQuery, Result<ICollection<Permission>>>
{
    private IPermissionRepository _permissionRepository;

    public GetAllPermissionsQueryHandler(IPermissionRepository permissionRepository)
        => _permissionRepository = permissionRepository;

    public async Task<Result<ICollection<Permission>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = _permissionRepository.GetAll();

        return Result.Ok(permissions);
    }
}
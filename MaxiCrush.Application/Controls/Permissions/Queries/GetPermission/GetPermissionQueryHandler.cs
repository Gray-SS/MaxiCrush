using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Permissions.Queries.GetPermission;

public class GetPermissionQueryHandler
    : IRequestHandler<GetPermissionQuery, Result<Permission>>
{
    private IPermissionRepository _permissionRepository;

    public GetPermissionQueryHandler(IPermissionRepository permissionRepository)
        => _permissionRepository = permissionRepository;


    public async Task<Result<Permission>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
    {
        var permission = await _permissionRepository.GetByIdAsync(request.Id);

        if (permission == null)
            return Result.Fail(AppErrors.Permissions.NotFound);

        return permission;
    }
}

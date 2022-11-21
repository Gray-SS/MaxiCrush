using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Roles.Queries.GetRole;

public class GetRoleQueryHandler
    : IRequestHandler<GetRoleQuery, Result<Role>>
{
    private readonly IRoleRepository _roleRepository;

    public GetRoleQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Result<Role>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id);

        if (role == null)
            return Result.Fail(AppErrors.Roles.NotFound);

        return role;
    }
}

using FluentResults;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Roles.Queries.GetAllRoles;

public class GetAllRolesQueryHandler
    : IRequestHandler<GetAllRolesQuery, Result<IEnumerable<Role>>>
{

    private readonly IRoleRepository _roleRepository;

    public GetAllRolesQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Result<IEnumerable<Role>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = _roleRepository.GetAll();

        return Result.Ok(roles.AsEnumerable());
    }
}

using FluentResults;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Roles.Queries.GetAllRoles;

public record GetAllRolesQuery(
    ) : IRequest<Result<IEnumerable<Role>>>;

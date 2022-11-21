
using FluentResults;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Roles.Queries.GetRole;

public record GetRoleQuery(
    Guid Id) : IRequest<Result<Role>>;
using FluentResults;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Permissions.Queries.GetPermission;

public record GetPermissionQuery(
    Guid Id) : IRequest<Result<Permission>>;

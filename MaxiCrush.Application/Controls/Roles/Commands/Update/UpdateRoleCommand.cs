using FluentResults;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Roles.Commands.Update;

public record UpdateRoleCommand(
    Guid SenderId,
    Guid RoleId,
    string? Name = null,
    int? Power = null,
    Guid[]? PermissionIds = null) : IRequest<Result<Role>>;

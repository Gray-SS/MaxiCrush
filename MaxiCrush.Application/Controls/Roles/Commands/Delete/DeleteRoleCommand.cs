using FluentResults;
using MediatR;

namespace MaxiCrush.Application.Controls.Roles.Commands.Delete;

public record DeleteRoleCommand(
    Guid SenderId,
    Guid RoleId) : IRequest<Result>;
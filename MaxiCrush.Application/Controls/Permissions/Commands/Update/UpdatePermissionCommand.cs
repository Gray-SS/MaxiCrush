using FluentResults;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Permissions.Commands.Update;

public record UpdatePermissionCommand(
    Guid Id,
    string Name) : IRequest<Result<Permission>>;

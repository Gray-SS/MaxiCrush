using FluentResults;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Permissions.Commands.Create;

public record CreatePermissionCommand(
    string Name) : IRequest<Result<Permission>>;

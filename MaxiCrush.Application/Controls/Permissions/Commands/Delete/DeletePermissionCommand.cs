using FluentResults;
using MediatR;

namespace MaxiCrush.Application.Controls.Permissions.Commands.Delete;

public record DeletePermissionCommand(
    Guid Id) : IRequest<Result>;

using FluentResults;
using MediatR;

namespace MaxiCrush.Application.Controls.Users.Commands.Delete;

public record DeleteUserCommand(
    Guid SenderId,
    Guid UserId) : IRequest<Result>;

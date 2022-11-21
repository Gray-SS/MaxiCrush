using FluentResults;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Users.Commands.Deban;

public record DebanUserCommand(
    Guid SenderId,
    Guid TargetId) : IRequest<Result<User>>;

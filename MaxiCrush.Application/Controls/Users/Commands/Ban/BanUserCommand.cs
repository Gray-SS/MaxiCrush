using FluentResults;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Users.Commands.Ban;

public record BanUserCommand(
    Guid SenderId,
    Guid TargetId,
    TimeSpan Duration,
    string? Reason = null) : IRequest<Result<User>>;

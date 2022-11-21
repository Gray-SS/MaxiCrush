using FluentResults;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Users.Commands.SetRole;

public record SetUserRoleCommand(
    Guid SenderId,
    Guid TargetUserId,
    string Role) : IRequest<Result<User>>;
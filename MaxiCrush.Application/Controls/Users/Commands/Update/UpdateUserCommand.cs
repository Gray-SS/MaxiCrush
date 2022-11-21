using FluentResults;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Users.Commands.Update;

public record UpdateUserCommand(
    Guid SenderId,
    Guid UserId,
    string? Firstname = null,
    string? Lastname = null,
    string? Gender = null,
    string? GenderInterestedIn = null,
    string? Biography = null) : IRequest<Result<User>>;

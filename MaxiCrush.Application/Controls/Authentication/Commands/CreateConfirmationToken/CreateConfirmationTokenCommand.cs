using FluentResults;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Authentication.Commands.CreateConfirmationToken;

public record CreateConfirmationTokenCommand(
    string Email) : IRequest<Result<ConfirmationToken>>;

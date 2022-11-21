using FluentResults;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Authentication.Commands.Confirmation;

public record ConfirmationCommand(
    string Email) : IRequest<Result<ConfirmationCode>>;

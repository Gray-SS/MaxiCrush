using FluentResults;
using MaxiCrush.Application.Controls.Authentication.Common;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Authentication.Commands.Register;

public record RegisterCommand(
    string Firstname,
    string Lastname,
    string Email,
    string Password,
    string Biography,
    string Gender,
    string GenderInterest,
    DateTime Birthday,
    string? Code = null) : IRequest<Result<AuthenticationResult>>;
using FluentResults;
using MediatR;

namespace MaxiCrush.Application.Controls.Authentication.Queries.IsConfirmationValid;

public record VerifyConfirmationQuery(
    string Email,
    string Code) : IRequest<Result<bool>>;

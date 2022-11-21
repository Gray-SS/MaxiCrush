using FluentResults;
using MaxiCrush.Application.Controls.Authentication.Common;
using MediatR;

namespace MaxiCrush.Application.Controls.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<Result<AuthenticationResult>>;

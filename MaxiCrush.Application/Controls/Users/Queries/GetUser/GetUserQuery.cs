using FluentResults;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Users.Queries.GetUser;

public record GetUserQuery(
    Guid Id) : IRequest<Result<User>>;

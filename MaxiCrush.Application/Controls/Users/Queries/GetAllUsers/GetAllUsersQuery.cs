using FluentResults;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Users.Queries.GetAllUsers;

public record GetAllUsersQuery()
    : IRequest<Result<IEnumerable<User>>>;

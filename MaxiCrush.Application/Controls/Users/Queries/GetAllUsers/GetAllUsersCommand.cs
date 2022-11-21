using FluentResults;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Users.Queries.GetAllUsers;

public class GetAllUsersCommand
    : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<User>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersCommand(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<IEnumerable<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _userRepository.GetAll();

        return Result.Ok(users.AsEnumerable());
    }
}

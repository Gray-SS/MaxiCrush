using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Authentication;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Users.Queries.GetUser;

public class GetUserQueryHandler
    : IRequestHandler<GetUserQuery, Result<User>>
{
    private readonly IUserRepository _userRepos;

    public GetUserQueryHandler(IUserRepository userRepos)
    {
        _userRepos = userRepos;
    }

    public async Task<Result<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepos.GetByIdAsync(request.Id);

        if (user == null)
            return Result.Fail(AppErrors.Users.NotFound);

        return user;
    }
}

using FluentResults;
using MapsterMapper;
using MaxiCrush.Application.Controls.Authentication.Common;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Authentication;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MediatR;
using System.Runtime.CompilerServices;
using System.Text;

namespace MaxiCrush.Application.Controls.Authentication.Queries.Login;

public class LoginQueryHandler : 
    IRequestHandler<LoginQuery, Result<AuthenticationResult>>
{

    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepos;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator,
                             IPasswordHasher passwordHasher,
                             IUserRepository userRepos,
                             IMapper mapper)
    {
        _mapper = mapper;
        _userRepos = userRepos;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepos.GetByEmailAsync(query.Email);

        if (user == null)
            return Result.Fail(AppErrors.Authentication.InvalidCredentials);

        var passwordHash = _passwordHasher.ComputeHash(query.Password);
        if (!_passwordHasher.Equivalent(passwordHash, user.Password))
            return Result.Fail(AppErrors.Authentication.InvalidCredentials);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}

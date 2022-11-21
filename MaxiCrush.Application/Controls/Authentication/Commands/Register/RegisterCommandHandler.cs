using FluentResults;
using MapsterMapper;
using MaxiCrush.Application.Controls.Authentication.Common;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Authentication;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;
using System.Text;

namespace MaxiCrush.Application.Controls.Authentication.Commands.Register;

public class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, Result<AuthenticationResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepos;
    private readonly IRoleRepository _roleRepos;
    private readonly IConfirmationCodeRepository _confirmationTokenRepos;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator,
                                  IPasswordHasher passwordHasher,
                                  IUserRepository userRepos,
                                  IRoleRepository roleRepos,
                                  IUnitOfWork unitOfWork,
                                  IConfirmationCodeRepository confirmationTokenRepos)
    {
        _userRepos = userRepos;
        _roleRepos = roleRepos;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _confirmationTokenRepos = confirmationTokenRepos;
    }


    public async Task<Result<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepos.GetByEmailAsync(command.Email);

        if (user != null)
            return Result.Fail(AppErrors.Authentication.DuplicateEmail);

        var confirmationToken = await _confirmationTokenRepos.GetByEmailAsync(command.Email);

        if (confirmationToken == null)
            return Result.Fail(AppErrors.Authentication.NoConfirmationToken);

        if (confirmationToken.Value != command.Code)
            return Result.Fail(AppErrors.Authentication.WrongConfirmationCode);


        var defaultRole = await _roleRepos.GetByNameAsync("User");

        if (defaultRole == null)
            return Result.Fail(AppErrors.Roles.NoDefaultRole);


        user = new User
        {
            Id = Guid.NewGuid(),
            Firstname = command.Firstname,
            Lastname = command.Lastname,
            Email = command.Email,
            Biography = command.Biography,
            Gender = command.Gender,
            GenderInterestedIn = command.GenderInterest,
            Password = _passwordHasher.ComputeHash(command.Password),
            CreatedAt = DateTime.UtcNow,
            Birthday = command.Birthday,
            EndBanDate = DateTime.UtcNow,
            Role = defaultRole
        };


        await _userRepos.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}

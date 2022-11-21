using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Application.Common.Interfaces.Services;
using MaxiCrush.Domain.Entities;
using MaxiCrush.Infrastructure.Mailing;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace MaxiCrush.Application.Controls.Authentication.Commands.Confirmation;

public class ConfirmationCommandHandler
    : IRequestHandler<ConfirmationCommand, Result<ConfirmationCode>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;
    private readonly IConfirmationCodeRepository _confirmationTokenRepository;

    public ConfirmationCommandHandler(IConfirmationCodeRepository confirmationTokenRepository,
                                      IUnitOfWork unitOfWork,
                                      IEmailService emailService,
                                      IUserRepository userRepository)
    {
        _confirmationTokenRepository = confirmationTokenRepository;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _userRepository = userRepository;
    }

    public async Task<Result<ConfirmationCode>> Handle(ConfirmationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user != null)
            return Result.Fail(AppErrors.Authentication.Confirmation_UserAlreadyExist);

        var confirmationToken = await _confirmationTokenRepository.GetByEmailAsync(request.Email);

        if (confirmationToken != null)
        {
            await _confirmationTokenRepository.RemoveAsync(confirmationToken);
            await _unitOfWork.SaveChangesAsync();
        }

        confirmationToken = new ConfirmationCode()
        {
            Id = Guid.NewGuid(),
            Value = GenerateCode(4),
            Email = request.Email,
        };

        var message = new EmailMessageBuilder()
                          .From("kronotaz@gmail.com")
                          .To(request.Email)
                          .WithSubject("Confirmation Code")
                          .WithBody($"Code: {confirmationToken.Value}")
                          .Build();

        await _emailService.SendAsync(message);

        await _confirmationTokenRepository.AddAsync(confirmationToken);
        await _unitOfWork.SaveChangesAsync();

        return confirmationToken;
    }

    private string GenerateCode(int length)
    {
        var sb = new StringBuilder(length);

        for (int i = 0; i < length; i++)
            sb.Append(Random.Shared.Next(10));

        return sb.ToString();
    }
}

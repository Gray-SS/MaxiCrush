using FluentResults;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;
using System.Text;

namespace MaxiCrush.Application.Controls.Authentication.Commands.CreateConfirmationToken;

public class CreateConfirmationTokenCommandHandler
    : IRequestHandler<CreateConfirmationTokenCommand, Result<ConfirmationToken>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfirmationTokenRepository _confirmationTokenRepository;

    public CreateConfirmationTokenCommandHandler(IConfirmationTokenRepository confirmationTokenRepository, IUnitOfWork unitOfWork)
    {
        _confirmationTokenRepository = confirmationTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ConfirmationToken>> Handle(CreateConfirmationTokenCommand request, CancellationToken cancellationToken)
    {
        var confirmationToken = await _confirmationTokenRepository.GetByEmailAsync(request.Email);

        if (confirmationToken != null)
        {
            await _confirmationTokenRepository.RemoveAsync(confirmationToken);
            await _unitOfWork.SaveChangesAsync();
        }

        confirmationToken = new ConfirmationToken()
        {
            Id = Guid.NewGuid(),
            Code = GenerateCode(4),
            Email = request.Email,
            ExpirationDate = DateTime.UtcNow.AddMinutes(15)
        };

        //TODO: Add mailing service

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

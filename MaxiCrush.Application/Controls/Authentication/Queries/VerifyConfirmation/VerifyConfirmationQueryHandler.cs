using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MediatR;

namespace MaxiCrush.Application.Controls.Authentication.Queries.IsConfirmationValid;

public class VerifyConfirmationQueryHandler
    : IRequestHandler<VerifyConfirmationQuery, Result<bool>>
{
    private readonly IConfirmationCodeRepository _confirmationCodeRepository;

    public VerifyConfirmationQueryHandler(IConfirmationCodeRepository confirmationCodeRepository)
    {
        _confirmationCodeRepository = confirmationCodeRepository;
    }

    public async Task<Result<bool>> Handle(VerifyConfirmationQuery request, CancellationToken cancellationToken)
    {
        var confirmationCode = await _confirmationCodeRepository.GetByEmailAsync(request.Email);

        if (confirmationCode == null)
            return Result.Fail(AppErrors.Authentication.NoConfirmationToken);

        if (confirmationCode.Value != request.Code)
            return false;

        return true;
    }
}

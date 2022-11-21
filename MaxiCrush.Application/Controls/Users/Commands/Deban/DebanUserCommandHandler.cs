using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Users.Commands.Deban;

public class DebanUserCommandHandler
    : IRequestHandler<DebanUserCommand, Result<User>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public DebanUserCommandHandler(IUserRepository userRepository,
                                   IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<User>> Handle(DebanUserCommand request, CancellationToken cancellationToken)
    {
        var senderUser = await _userRepository.GetByIdAsync(request.SenderId);

        if (senderUser == null)
            return Result.Fail(AppErrors.Unexpected);

        var user = await _userRepository.GetByIdAsync(request.TargetId);

        if (user == null)
            return Result.Fail(AppErrors.Users.NotFound);

        if (!user.IsBanned)
            return Result.Fail(AppErrors.Users.NotBanned);

        if (senderUser.Role.Power != 999 && senderUser.Role.Power <= user.Role.Power)
            return Result.Fail(AppErrors.Permissions.InsufficientPermission);

        user.EndBanDate = DateTime.UtcNow;
        user.BanReason = string.Empty;

        await _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return user;
    }
}

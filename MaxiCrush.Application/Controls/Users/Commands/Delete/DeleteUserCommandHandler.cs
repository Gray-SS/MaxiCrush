using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MediatR;

namespace MaxiCrush.Application.Controls.Users.Commands.Delete;

public class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var senderUser = await _userRepository.GetByIdAsync(request.SenderId);

        if (senderUser == null)
            return Result.Fail(AppErrors.Unexpected);

        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
            return Result.Fail(AppErrors.Users.NotFound);

        if (senderUser.Role.Power != 999 && request.UserId != request.SenderId && senderUser.Role.Power <= user.Role.Power)
            return Result.Fail(AppErrors.Permissions.InsufficientPermission);

        await _userRepository.RemoveAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
}

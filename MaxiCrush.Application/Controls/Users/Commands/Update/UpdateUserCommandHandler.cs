using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Users.Commands.Update;

public class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, Result<User>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository,
                                    IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<Result<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var senderUser = await _userRepository.GetByIdAsync(request.SenderId);

        if (senderUser == null)
            return Result.Fail(AppErrors.Unexpected);

        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
            return Result.Fail(AppErrors.Users.NotFound);

        if (senderUser.Role.Power != 999 && senderUser.Id != user.Id && senderUser.Role.Power <= user.Role.Power)
            return Result.Fail(AppErrors.Permissions.InsufficientPermission);

        user.Firstname = request.Firstname ?? user.Firstname;
        user.Lastname = request.Lastname ?? user.Lastname;
        user.Gender = request.Gender ?? user.Gender;
        user.GenderInterestedIn = request.GenderInterestedIn ?? user.GenderInterestedIn;
        user.Biography = request.Biography ?? user.Biography;

        await _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return user;
    }
}

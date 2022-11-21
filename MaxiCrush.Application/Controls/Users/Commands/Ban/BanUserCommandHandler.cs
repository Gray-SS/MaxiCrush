using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Users.Commands.Ban;

public class BanUserCommandHandler
    : IRequestHandler<BanUserCommand, Result<User>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    public BanUserCommandHandler(IUnitOfWork unitOfWork,
                                 IRoleRepository roleRepository,
                                 IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<User>> Handle(BanUserCommand request, CancellationToken cancellationToken)
    {
        var senderUser = await _userRepository.GetByIdAsync(request.SenderId);

        if (senderUser == null)
            return Result.Fail(AppErrors.Unexpected);

        var user = await _userRepository.GetByIdAsync(request.TargetId);

        if (user == null)
            return Result.Fail(AppErrors.Users.NotFound);

        if (senderUser.Role.Power != 999 && senderUser.Role.Power <= user.Role.Power)
            return Result.Fail(AppErrors.Permissions.InsufficientPermission);

        var defaultRole = await _roleRepository.GetByNameAsync("User");

        if (defaultRole == null)
            return Result.Fail(AppErrors.Unexpected);

        user.Role = defaultRole;
        user.EndBanDate = DateTime.UtcNow.Add(request.Duration);
        user.BanReason = request.Reason;

        await _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return user;
    }
}

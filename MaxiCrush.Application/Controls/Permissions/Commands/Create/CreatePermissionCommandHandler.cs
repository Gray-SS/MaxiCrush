using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Permissions.Commands.Create;

public class CreatePermissionCommandHandler
    : IRequestHandler<CreatePermissionCommand, Result<Permission>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPermissionRepository _permissionRepository;

    public CreatePermissionCommandHandler(IPermissionRepository permissionRepository,
                                          IUnitOfWork unitOfWork)
    {
        _permissionRepository = permissionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Permission>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = await _permissionRepository.GetByNameAsync(request.Name);

        if (permission != null)
            return Result.Fail(AppErrors.Permissions.DuplicateName);

        permission = new Permission
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
        };

        await _permissionRepository.AddAsync(permission);
        await _unitOfWork.SaveChangesAsync();

        return permission;
    }
}

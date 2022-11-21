using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Permissions.Commands.Update;

public class UpdatePermissionCommandHandler
    : IRequestHandler<UpdatePermissionCommand, Result<Permission>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPermissionRepository _permissionRepository;

    public UpdatePermissionCommandHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
    {
        _permissionRepository = permissionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Permission>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = await _permissionRepository.GetByIdAsync(request.Id);

        if (permission == null)
            return Result.Fail(AppErrors.Permissions.NotFound);

        permission.Name = request.Name;

        await _permissionRepository.UpdateAsync(permission);
        await _unitOfWork.SaveChangesAsync();

        return permission; 
    }
}

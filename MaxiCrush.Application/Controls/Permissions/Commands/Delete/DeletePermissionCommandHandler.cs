using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MediatR;

namespace MaxiCrush.Application.Controls.Permissions.Commands.Delete;

public class DeletePermissionCommandHandler
    : IRequestHandler<DeletePermissionCommand, Result>
{
    private IUnitOfWork _unitOfWork;
    private IPermissionRepository _permissionRepository;

    public DeletePermissionCommandHandler(IPermissionRepository permissionRepository,
                                          IUnitOfWork unitOfWork)
    {
        _permissionRepository = permissionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = await _permissionRepository.GetByIdAsync(request.Id);

        if (permission == null)
            return Result.Fail(AppErrors.Permissions.NotFound);

        await _permissionRepository.RemoveAsync(permission);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
}

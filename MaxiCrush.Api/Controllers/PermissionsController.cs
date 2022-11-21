using Mapster;
using MapsterMapper;
using MaxiCrush.Api.Common;
using MaxiCrush.Api.Extensions;
using MaxiCrush.Application.Controls.Permissions.Commands.Create;
using MaxiCrush.Application.Controls.Permissions.Commands.Delete;
using MaxiCrush.Application.Controls.Permissions.Commands.Update;
using MaxiCrush.Application.Controls.Permissions.Queries.GetPermission;
using MaxiCrush.Contracts;
using MaxiCrush.Contracts.Dto;
using MaxiCrush.Contracts.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaxiCrush.Api.Controllers;

[ApiController]
public class PermissionsController : CQRSController
{
    public PermissionsController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost(ApiRoutes.Permissions.Create)]
    [Authorize(Policy = Permissions.Permission.Commands)]
    public async Task<IActionResult> CreatePermissionAsync([FromBody] CreatePermissionRequest request)
    {
        var command = Mapper.Map<CreatePermissionCommand>(request);
        var result = await Send(command);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var dto = Mapper.Map<PermissionDto>(result.Value);
        return this.Ok(dto);
    }

    [HttpPatch(ApiRoutes.Permissions.Update)]
    [Authorize(Policy = Permissions.Permission.Commands)]
    public async Task<IActionResult> UpdatePermissionAsync([FromBody] UpdatePermissionRequest request)
    {
        var command = Mapper.Map<UpdatePermissionCommand>(request);
        var result = await Send(command);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var dto = Mapper.Map<PermissionDto>(result.Value);
        return this.Ok(dto);
    }

    [HttpDelete(ApiRoutes.Permissions.Delete)]
    [Authorize(Policy = Permissions.Permission.Commands)]
    public async Task<IActionResult> DeletePermissionAsync(Guid id)
    {
        var command = new DeletePermissionCommand(id);
        var result = await Send(command);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        return this.Ok();
    }

    [HttpGet(ApiRoutes.Permissions.GetPermission)]
    [Authorize(Policy = Permissions.Permission.Queries)]
    public async Task<IActionResult> GetPermissionAsync(Guid id)
    {
        var command = new GetPermissionQuery(id);
        var result = await Send(command);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var dto = Mapper.Map<PermissionDto>(result.Value);
        return this.Ok(dto);
    }

    [HttpGet(ApiRoutes.Permissions.GetAllPermissions)]
    [Authorize(Policy = Permissions.Permission.Queries)]
    public async Task<IActionResult> GetAllPermissionsAsync()
    {
        var command = new GetAllPermissionsQuery();
        var result = await Send(command);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var dto = result.Value.Select(x => new PermissionDto() { Id = x.Id, Name = x.Name });
        return this.Ok(dto);
    }
}

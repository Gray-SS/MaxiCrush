using MapsterMapper;
using MaxiCrush.Api.Common;
using MaxiCrush.Api.Extensions;
using MaxiCrush.Application.Controls.Roles.Commands.Create;
using MaxiCrush.Application.Controls.Roles.Commands.Delete;
using MaxiCrush.Application.Controls.Roles.Commands.Update;
using MaxiCrush.Application.Controls.Roles.Queries.GetAllRoles;
using MaxiCrush.Application.Controls.Roles.Queries.GetRole;
using MaxiCrush.Contracts;
using MaxiCrush.Contracts.Dto;
using MaxiCrush.Contracts.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaxiCrush.Api.Controllers;

[ApiController]
public class RolesController : CQRSController
{
    public RolesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost(ApiRoutes.Roles.Create)]
    [Authorize(Policy = Permissions.Roles.Commands)]
    public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleRequest request)
    {
        var getIdResult = HttpContext.User.GetUserId();

        if (getIdResult.IsFailed)
            return this.HandleProblem(getIdResult.Errors);

        var command = new CreateRoleCommand(getIdResult.Value, request.Name, request.Power);
        var result = await Send(command);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var dto = Mapper.Map<RoleDto>(result.Value);
        return this.Ok(dto);
    }

    [HttpPatch(ApiRoutes.Roles.Update)]
    [Authorize(Policy = Permissions.Roles.Commands)]
    public async Task<IActionResult> UpdateRoleAsync(Guid id, [FromBody] UpdateRoleRequest request)
    {
        var getIdResult = HttpContext.User.GetUserId();

        if (getIdResult.IsFailed)
            return this.HandleProblem(getIdResult.Errors);

        var command = new UpdateRoleCommand(getIdResult.Value, id, request.Name, request.Power, request.PermissionIds);
        var result = await Send(command);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var dto = Mapper.Map<RoleDto>(result.Value);
        return this.Ok(dto);
    }


    [HttpDelete(ApiRoutes.Roles.Delete)]
    [Authorize(Policy = Permissions.Roles.Commands)]
    public async Task<IActionResult> DeleteRoleAsync(Guid id)
    {
        var getIdResult = HttpContext.User.GetUserId();

        if (getIdResult.IsFailed)
            return this.HandleProblem(getIdResult.Errors);

        var command = new DeleteRoleCommand(getIdResult.Value, id);
        var result = await Send(command);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        return this.Ok();
    }

    [HttpGet(ApiRoutes.Roles.GetRole)]
    [Authorize(Policy = Permissions.Roles.Queries)]
    public async Task<IActionResult> GetRoleAsync(Guid id)
    {
        var query = new GetRoleQuery(id);
        var result = await Send(query);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var dto = Mapper.Map<RoleDto>(result.Value);
        return this.Ok(dto);
    }

    [HttpGet(ApiRoutes.Roles.GetAllRoles)]
    [Authorize(Policy = Permissions.Roles.Queries)]
    public async Task<IActionResult> GetAllRolesAsync()
    {
        var query = new GetAllRolesQuery();
        var result = await Send(query);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var dto = Mapper.Map<IEnumerable<RoleDto>>(result.Value);
        return this.Ok(dto);
    }
}

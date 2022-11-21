using MapsterMapper;
using MaxiCrush.Api.Common;
using MaxiCrush.Api.Extensions;
using MaxiCrush.Application.Controls.Users.Commands.Delete;
using MaxiCrush.Application.Controls.Users.Commands.SetRole;
using MaxiCrush.Application.Controls.Users.Commands.Update;
using MaxiCrush.Application.Controls.Users.Queries.GetAllUsers;
using MaxiCrush.Application.Controls.Users.Queries.GetUser;
using MaxiCrush.Contracts;
using MaxiCrush.Contracts.Dto;
using MaxiCrush.Contracts.Users;
using MaxiCrush.Contracts.Users.Me;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace MaxiCrush.Api.Controllers;

[Authorize]
[ApiController]
public class UsersController : CQRSController
{
    public UsersController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    #region Target Authorized User

    [HttpGet(ApiRoutes.Users.AuthorizedUser)]
    public async Task<IActionResult> GetMeAsync()
    {
        var getIdResult = HttpContext.User.GetUserId();

        if (getIdResult.IsFailed)
            return this.HandleProblem(getIdResult.Errors);

        var query = new GetUserQuery(getIdResult.Value);
        var result = await Send(query);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var dto = Mapper.Map<UserDto>(result.Value);
        return this.Ok(dto);
    }

    [HttpPatch(ApiRoutes.Users.AuthorizedUser)]
    public async Task<IActionResult> PatchMeAsync([FromBody] UpdateMeRequest request)
    {
        var getIdResult = HttpContext.User.GetUserId();

        if (getIdResult.IsFailed)
            return this.HandleProblem(getIdResult.Errors);

        var command = Mapper.Map<UpdateUserCommand>((request, getIdResult.Value));
        var result = await Send(command);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var dto = Mapper.Map<UserDto>(result.Value);
        return this.Ok(dto);
    }

    [HttpDelete(ApiRoutes.Users.AuthorizedUser)]
    public async Task<IActionResult> DeleteMeAsync()
    {
        var getIdResult = HttpContext.User.GetUserId();

        if (getIdResult.IsFailed)
            return this.HandleProblem(getIdResult.Errors);

        var command = new DeleteUserCommand(getIdResult.Value, getIdResult.Value);
        var result = await Send(command);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        return this.Ok();
    }

    #endregion

    [HttpPost(ApiRoutes.Users.SetRole)]
    [Authorize(Policy = Permissions.Users.SetRole)]
    public async Task<IActionResult> SetRoleAsync(Guid id, [FromBody]string roleName)
    {
        var getIdResult = HttpContext.User.GetUserId();

        if (getIdResult.IsFailed)
            return this.HandleProblem(getIdResult.Errors);

        var command = new SetUserRoleCommand(getIdResult.Value, id, roleName);
        var result = await Send(command);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var dto = Mapper.Map<UserDto>(result.Value);
        return this.Ok(dto);
    }

    [HttpGet(ApiRoutes.Users.GetUser)]
    [Authorize(Policy = Permissions.Users.Queries)]
    public async Task<IActionResult> GetUserAsync(Guid id)
    {
        var query = new GetUserQuery(id);
        var result = await Send(query);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var dto = Mapper.Map<UserDto>(result.Value);
        return this.Ok(dto);
    }

    [HttpGet(ApiRoutes.Users.GetAllUser)]
    [Authorize(Policy = Permissions.Users.Queries)]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var query = new GetAllUsersQuery();
        var result = await Send(query);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var dto = result.Value.Select(x => Mapper.Map<UserDto>(x));
        return this.Ok(dto);
    }

    [HttpPatch(ApiRoutes.Users.UpdateUser)]
    [Authorize(Policy = Permissions.Users.Commands)]
    public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody]UpdateUserRequest request)
    {
        var getIdResult = HttpContext.User.GetUserId();

        if (getIdResult.IsFailed)
            return this.HandleProblem(getIdResult.Errors);

        var query = new UpdateUserCommand(getIdResult.Value, id, request.Firstname, request.Lastname, request.Gender, request.GenderInterest);
        var result = await Send(query);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var dto = Mapper.Map<UserDto>(result.Value);
        return this.Ok(dto);
    }

    [HttpDelete(ApiRoutes.Users.DeleteUser)]
    [Authorize(Policy = Permissions.Users.Commands)]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        var getSenderIdResult = HttpContext.User.GetUserId();

        if (getSenderIdResult.IsFailed)
            return this.HandleProblem(getSenderIdResult.Errors);

        var command = new DeleteUserCommand(getSenderIdResult.Value, id);
        var result = await Send(command);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        return this.Ok();
    }
}

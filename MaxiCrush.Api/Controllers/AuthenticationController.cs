using FluentValidation;
using FluentValidation.Results;
using MapsterMapper;
using MaxiCrush.Api.Extensions;
using MaxiCrush.Application.Controls.Authentication.Commands.Register;
using MaxiCrush.Application.Controls.Authentication.Queries.Login;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Contracts;
using MaxiCrush.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MaxiCrush.Api.Common;
using MaxiCrush.Contracts.Authentication.Login;
using MaxiCrush.Contracts.Authentication.Register;

namespace MaxiCrush.Api.Controllers;

[ApiController]
public class AuthenticationController : CQRSController
{

    public AuthenticationController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost(ApiRoutes.Authentication.Register)]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        var command = new RegisterCommand(request.Firstname,
                                          request.Lastname,
                                          request.Email,
                                          request.Password,
                                          request.Biography,
                                          request.Gender,
                                          request.GenderInterest,
                                          request.Birthday,
                                          request.Code);

        var result = await Send(command);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var authResult = result.Value;

        var response = Mapper.Map<RegisterResponse>(authResult); 
        return this.Ok(response);
    }

    [HttpPost(ApiRoutes.Authentication.Login)]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        var result = await Send(query);

        if (result.IsFailed)
            return this.HandleProblem(result.Errors);

        var authResult = result.Value;

        var response = Mapper.Map<LoginResponse>(authResult);
        return this.Ok(response);
    }
}

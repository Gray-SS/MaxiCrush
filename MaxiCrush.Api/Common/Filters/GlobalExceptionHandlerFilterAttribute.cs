using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace MaxiCrush.Api.Common.Filters;

public class GlobalExceptionHandlerFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Status = (int)HttpStatusCode.InternalServerError
        };

        context.Result = new ObjectResult(problemDetails);
        context.ExceptionHandled = true;
    }
}

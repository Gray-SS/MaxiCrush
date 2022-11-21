using FluentResults;
using MaxiCrush.Application.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace MaxiCrush.Api.Extensions;

public static class ControllersExtensions
{
    public static IActionResult HandleProblem(this ControllerBase controller, List<IError> errors)
    {
        if (errors.All(x => x is ResultError resultError && resultError.StatusCode == HttpStatusCode.BadRequest))
        {
            var validationErrors = errors.ConvertAll(x => (ResultError)x);

            var modelStateDictionary = new ModelStateDictionary();
            foreach(var item in validationErrors)
            {
                modelStateDictionary.AddModelError(item.Code, item.Message);
            }

            return controller.ValidationProblem(modelStateDictionary);
        }

        var firstError = errors.First();

        if (firstError is ResultError handledServiceError)
        {
            return controller.Problem(title: handledServiceError.Message,
                                      statusCode: (int)handledServiceError.StatusCode);
        }

        return controller.Problem(title: firstError.Message,
                                  statusCode: (int)HttpStatusCode.InternalServerError);
    }
}

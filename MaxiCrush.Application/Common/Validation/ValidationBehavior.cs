using FluentResults;
using FluentValidation;
using MaxiCrush.Application.Common.Errors;
using MediatR;

namespace MaxiCrush.Application.Common.Validation;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
                                                       where TRequest : IRequest<TResponse>
                                                       where TResponse : IResultBase
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
        => _validator = validator;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
            return await next();

        var result = await _validator.ValidateAsync(request);

        if (result.IsValid)
            return await next();

        var errors = result.Errors.ConvertAll(failure => new ResultError(failure.ErrorMessage, failure.ErrorCode, System.Net.HttpStatusCode.BadRequest));
        return (dynamic)Result.Fail(errors);
    }
}

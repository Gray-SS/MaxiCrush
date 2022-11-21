using FluentResults;
using System.Net;

namespace MaxiCrush.Application.Common.Errors;

public class ResultError : IError
{
    public string Code { get; }
    public string Message { get; }
    public string? Details { get; }
    public HttpStatusCode StatusCode { get; }
    public virtual List<IError> Reasons => throw new NotImplementedException();
    public virtual Dictionary<string, object> Metadata => throw new NotImplementedException();

    public ResultError(string code, string message, HttpStatusCode statusCode, string? details = null)
    {
        Code = code;
        Message = message;
        Details = details;
        StatusCode = statusCode;
    }
}

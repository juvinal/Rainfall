using Rainfall.Core.Responses;
using System.Net;
namespace Rainfall.Core.Exceptions;

public class CustomException : Exception
{
    public List<ErrorDetail>? ErrorMessages { get; }

    public HttpStatusCode StatusCode { get; }

    public CustomException(string message, List<ErrorDetail>? errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }
}

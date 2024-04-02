using Rainfall.Core.Responses;
using System.Net;

namespace Rainfall.Core.Exceptions;

public class InternalServerException : CustomException
{
    public InternalServerException(string message, List<ErrorDetail>? errors = default)
        : base(message, errors, HttpStatusCode.InternalServerError)
    {
    }
}

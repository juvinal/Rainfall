using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Rainfall.Core.Responses;


namespace Rainfall.Api.Extensions;

public static class ResultExtensions
{
    public static ActionResult<T> ToActionResult<T>(this ControllerBase controller, Result<T> result)
    {
        return controller.ToActionResult((Ardalis.Result.IResult)result);
    }

    internal static ActionResult ToActionResult(this ControllerBase controller, Ardalis.Result.IResult result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => controller.Ok(result.GetValue()),
            ResultStatus.NotFound => NotFound(controller, result),
            ResultStatus.Invalid => BadRequest(controller, result),
            _ => throw new NotSupportedException($"Result {result.Status} conversion is not supported.")
        };
    }

    private static ActionResult BadRequest(ControllerBase controller, Ardalis.Result.IResult result)
    {
        var errorResponse = new ErrorResponse()
        {
            Message = "Invalid request"
        };

        foreach (ValidationError validationError in result.ValidationErrors)
        {
            errorResponse.Detail.Add(new ErrorDetail(validationError.Identifier, validationError.ErrorMessage));
        }

        return controller.BadRequest(errorResponse);
    }

    private static ActionResult NotFound(ControllerBase controller, Ardalis.Result.IResult result)
    {
        var errorResponse = new ErrorResponse();
        if (result.Errors.Any())
        {
            errorResponse = new ErrorResponse()
            {
                Message = result.Errors.FirstOrDefault()
            };
        }

        return controller.NotFound(errorResponse);
    }
}

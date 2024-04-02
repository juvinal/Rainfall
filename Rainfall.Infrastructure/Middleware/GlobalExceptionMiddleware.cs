using Microsoft.AspNetCore.Http;
using Rainfall.Core.Exceptions;
using Rainfall.Core.Responses;
using Serilog;
using System;
using System.Net;
using System.Text.Json;

namespace Rainfall.Infrastructure.Middleware;

internal class GlobalExceptionMiddleware : IMiddleware
{
    public GlobalExceptionMiddleware()
    {
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var errorResult = new ErrorResponse
            {
                Message = exception.Message.Trim()
            };

            if (exception is not CustomException && exception.InnerException != null)
            {
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }
            }

            if (exception is FluentValidation.ValidationException fluentException)
            {

                errorResult.Message = "One or More Validations failed.";
                foreach (var error in fluentException.Errors)
                {
                    errorResult.Detail.Add(new ErrorDetail(error.PropertyName, error.ErrorMessage));
                }
            }

            var response = context.Response;

            switch (exception)
            {
                case CustomException e:
                    response.StatusCode = (int)e.StatusCode;
                    if (e.ErrorMessages is not null)
                    {
                        errorResult.Detail = e.ErrorMessages;
                    }

                    break;

                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                break;

                case FluentValidation.ValidationException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
            }

            Log.Error($"{errorResult.Message} Request failed with Status Code {response.StatusCode}.");
            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                await response.WriteAsync(JsonSerializer.Serialize(errorResult));
            }
            else
            {
                Log.Warning("Can't write error response. Response has already started.");
            }
        }
    }
}

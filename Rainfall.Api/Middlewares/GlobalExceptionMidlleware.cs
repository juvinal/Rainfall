using Microsoft.AspNetCore.Mvc;
using Rainfall.Core.Responses;
using Serilog;
using System.Net;
using System.Text.Json;

namespace Rainfall.Api.Middlewares;

internal class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception e)
        {
            Log.Error(e, "Unhandled exception");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorString = JsonSerializer.Serialize(ErrorResponses.InternalServerError());

            await context.Response.WriteAsync(errorString);
        }
    }
}

public static class ErrorResponses
{
    public static ObjectResult InternalServerError()
    {
        var response = new ErrorResponse
        {
            Message = "Internal server error. Please contact support if this error persists."
        };

        return new ObjectResult(response)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
    }
}

using System.Text.Json;
using AuthService.Application.Common.Exceptions;
using AuthService.Application.Common.Models;
using AuthService.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

namespace AuthService.API.Middlewares;

public class ExceptionHandler() : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        await HandleException(httpContext, exception, cancellationToken);

        return true;
    }

    private Task HandleException(HttpContext context, Exception ex, CancellationToken cancellationToken)
    {
        var code = ex switch
        {
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            ForbiddenException => StatusCodes.Status403Forbidden,
            NotFoundException => StatusCodes.Status404NotFound,
            ResourceNotFoundException => StatusCodes.Status404NotFound,
            BadRequestException => StatusCodes.Status400BadRequest,
            UnprocessableRequestException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };
        var errors = ex switch
        {
            ValidationException exception => exception.Errors,
            _ => new List<string> { ex.Message }
        };

        if (code == StatusCodes.Status500InternalServerError)
        {
            Log.Error(ex.Message);
            errors = new List<string> { "An error occurred while processing your request" };
        }

        var result = JsonSerializer.Serialize(ApiResult<string>.Failure(code, errors));

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;

        return context.Response.WriteAsync(result, cancellationToken);
    }
}

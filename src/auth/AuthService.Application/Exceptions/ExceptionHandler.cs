using System.Text.Json;
using AuthService.Application.Models;
using AuthService.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AuthService.Application.Exceptions;

public class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        await HandleException(httpContext, exception, cancellationToken);

        return true;
    }

    private Task HandleException(HttpContext context, Exception ex, CancellationToken cancellationToken)
    {
        var code = StatusCodes.Status500InternalServerError;
        var errors = new List<string> { ex.Message };

        code = ex switch
        {
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            ForbiddenException => StatusCodes.Status403Forbidden,
            NotFoundException => StatusCodes.Status404NotFound,
            ResourceNotFoundException => StatusCodes.Status404NotFound,
            BadRequestException => StatusCodes.Status400BadRequest,
            UnprocessableRequestException => StatusCodes.Status422UnprocessableEntity,
            _ => code
        };

        if (code == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(ex.Message);
            errors = new List<string> { "An error occurred while processing your request" };
        }

        var result = JsonSerializer.Serialize(ApiResult<string>.Failure(code, errors));

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status200OK;

        return context.Response.WriteAsync(result, cancellationToken);
    }
}

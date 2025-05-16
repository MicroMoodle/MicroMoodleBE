using System.Text;
using System.Text.Json;
using AuthService.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthService.API.Filters;

public class ResponseWrapperFilter : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            var originalValue = objectResult.Value;
            var statusCode = context.HttpContext.Response.StatusCode;
            objectResult.Value = ApiResult<object>.Success(statusCode, originalValue);
        }
    }
}

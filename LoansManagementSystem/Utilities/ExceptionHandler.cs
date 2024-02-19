using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LoansManagementSystem.Utilities;

public class ExceptionHandler : ExceptionFilterAttribute
{
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        _logger.LogWarning($"Error: {context.Exception}");
        _logger.LogWarning($"Exception Message: {context.Exception.Message}");

        if (context.Exception.InnerException is not null)
        {
            _logger.LogWarning($" Inner exception: {context.Exception.InnerException.Message}");
        }

        if (context.Exception is BadHttpRequestException)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new JsonResult(new { StatusDescription = context.Exception.Message });
        }

        else if (context.Exception is UnauthorizedAccessException)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Result = new JsonResult(new { StatusDescription = "Unauthorized access" });
        }
        else if (context.Exception.Message == "Sequence contains no elements")
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Result = new JsonResult(new { StatusDescription = "Item does not exist" });
        }
        else
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new JsonResult(new { StatusDescription = "Error occurred, please contact the application developer" });
        }
    }
}
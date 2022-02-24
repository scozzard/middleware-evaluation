using System.Net;
using System.Text.Json;
using Evaluation.Api.Model;

namespace Evaluation.Api.Infrastructure;

/// <summary>
/// Middleware is used to catch all unhandled exceptions. It performs the following tasks
/// - Logs the exception (so we can check it out retrospectively).
/// - Returns a generic Error object back to the client with status code 500.
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log in full whatever error happens so we are able to inspect what happened after.
            _logger.LogError(ex, "Exception Occurred");

            // If we wanted, at this point we could inspect the exception and return reveal precisely what
            // information to the client, i.e., "Middleware NZ currently inaccessible" etc. But for now, let's
            // just show a generic message (keep them guessing).
            var error = JsonSerializer.Serialize(new Error("Internal Server Error", "Something went wrong."));

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(error);
        }
    }
}
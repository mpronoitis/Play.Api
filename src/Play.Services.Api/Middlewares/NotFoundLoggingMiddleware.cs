namespace Play.Services.Api.Middlewares;

/// <summary>
///     Middleware to log 404 requests via the ILogger
///     and return a 404 response.
/// </summary>
public class NotFoundLoggingMiddleware
{
    private readonly ILogger<NotFoundLoggingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public NotFoundLoggingMiddleware(RequestDelegate next, ILogger<NotFoundLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);
        if (context.Response.StatusCode == 404)
        {
            //get httpContext.Request.Path
            var path = context.Request.Path;
            //get httpContext.Request.QueryString
            var queryString = context.Request.QueryString;
            //get httpContext.Request.Method
            var method = context.Request.Method;
            //log warning
            _logger.LogWarning($"404 Not Found: {method} {path}{queryString}");
        }
    }
}
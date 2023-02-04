namespace Play.Services.Api.Configurations;

public static class SerilogConfig
{
    public static void UseSerilogSetup(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((_, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(builder.Configuration);
            //exclude all logs containing the word "heartbeat"
            loggerConfiguration.Filter.ByExcluding(c =>
                c.Properties.Any(p => p.Value.ToString().Contains("heartbeat")));
            //exclude all logs containing "CORS policy execution successful."
            loggerConfiguration.Filter.ByExcluding(c =>
                c.MessageTemplate.Text.Contains("CORS policy execution successful."));
            //exclude http://api.playsystems.io/hangfire/stats 
            loggerConfiguration.Filter.ByExcluding(c =>
                c.MessageTemplate.Text.Contains("api.playsystems.io/hangfire"));
            //exclude all kuma logs
            loggerConfiguration.Filter.ByExcluding(c =>
                c.MessageTemplate.Text.Contains("kuma"));
        });
    }

    public static void PushSerilogProperties(this WebApplication webApplication)
    {
        webApplication.Use(async (ctx, next) =>
        {
            using (LogContext.PushProperty("True-Client-IP", ctx.Request.Headers["Cf-Connecting-Ip"]))
            {
                await next(ctx);
            }

            //add CF-IPCountry
            if (ctx.Response.Headers.ContainsKey("Cf-Ipcountry"))
                using (LogContext.PushProperty("True-Client-Country", ctx.Response.Headers["Cf-Ipcountry"]))
                {
                    await next(ctx);
                }
        });
    }

    public static void UseCustomSerilogRequestLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging(loggingOptions =>
        {
            loggingOptions.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("RequestHost", httpContext.Request.Host);
                diagnosticContext.Set("RequestProtocol", httpContext.Request.Protocol);
                diagnosticContext.Set("RequestPath", httpContext.Request.Path);
                diagnosticContext.Set("RequestQueryString", httpContext.Request.QueryString);
                diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                //get response status code
                diagnosticContext.Set("ResponseStatusCode", httpContext.Response.StatusCode);
                //get response content length
                diagnosticContext.Set("ResponseContentLength", httpContext.Response.ContentLength);
                //add Cf-Connecting-Ip header
                diagnosticContext.Set("True-Client-Ip", httpContext.Request.Headers["Cf-Connecting-Ip"]);
                //get forwarded for
                diagnosticContext.Set("X-Forwarded-For", httpContext.Request.Headers["X-Forwarded-For"]);

                //exceptions

                diagnosticContext.SetException(httpContext.Features.Get<IExceptionHandlerFeature>()?.Error);
                diagnosticContext.Set("Exception", httpContext.Features.Get<IExceptionHandlerFeature>()?.Error);
                diagnosticContext.Set("ExceptionMessage",
                    httpContext.Features.Get<IExceptionHandlerFeature>()?.Error.Message);
                diagnosticContext.Set("ExceptionStackTrace",
                    httpContext.Features.Get<IExceptionHandlerFeature>()?.Error.StackTrace);
            };

            //destructuring
            loggingOptions.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                //add user email from jwt token from header
                var userEmail = httpContext.User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            };
        });
    }
}
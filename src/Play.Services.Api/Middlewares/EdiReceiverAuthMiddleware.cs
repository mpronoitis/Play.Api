using System.Net;

namespace Play.Services.Api.Middlewares;

/// <summary>
///     EdiReceiverAuthMiddleware is a middleware to perform auth based on the api key (X-API-Key) header.
///     Currently it is not being used , testing must be done to see if it is working as expected.
/// </summary>
public class EdiReceiverAuthMiddleware
{
    private static readonly string[] _allowedApiKeys =
    {
        "3C4BB929F69EB3B096EA33A7BD74E5F3",
        "401D57697F65053C1C75CBFA43C46629"
    };

    private readonly RequestDelegate _next;

    public EdiReceiverAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        //if path is edi/documents/receiver then check for api key
        if (context.Request.Path.StartsWithSegments("/edi/documents/receiver"))
        {
            //check for api key
            if (!context.Request.Headers.TryGetValue("X-API-Key", out var extractedApiKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            //check if api key is valid
            if (_allowedApiKeys.All(x => x != extractedApiKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                //error message
                await context.Response.WriteAsync("Invalid API Key - contact websupport@playsystems.io");
                return;
            }

            //if api key is valid then continue
            await _next(context);
        }

        //if path is not edi/documents/receiver then continue
        await _next(context);
    }
}

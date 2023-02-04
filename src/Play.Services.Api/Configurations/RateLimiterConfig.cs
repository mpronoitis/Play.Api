using Microsoft.AspNetCore.RateLimiting;

namespace Play.Services.Api.Configurations;

public static class RateLimiterConfig
{
    //static void method AddRateLimiterConfig with IServiceCollection and RateLimiterOptions
    public static void AddRateLimiterConfig(this IServiceCollection services)
    {
        //add rate limiter
        services.AddRateLimiter(
                options =>
                {
                    //returns 429 if limit is reached
                    options.RejectionStatusCode = 429;
                    options.AddFixedWindowLimiter("WhmcsLoginBot", options =>
                    {
                        options.AutoReplenishment = true;
                        options.PermitLimit = 100;
                        options.Window = TimeSpan.FromSeconds(30);
                    });
                });
    }
}
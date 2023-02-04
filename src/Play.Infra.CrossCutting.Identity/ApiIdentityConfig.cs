using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Play.Infra.CrossCutting.Identity.Interfaces;

namespace Play.Infra.CrossCutting.Identity;

/// <summary>
///     The AddApiIdentityConfiguration method is a helper method that adds configuration for JWT-based authentication to
///     an IServiceCollection.
///     It does this by adding an Authentication service to the collection and configuring it to use the
///     JwtBearerDefaults.AuthenticationScheme for both authentication and challenge.
///     It also adds a JwtBearer service to the collection and configures it to use the TokenValidationParameters object
///     that is defined within the method.
///     The TokenValidationParameters object specifies various options that control how the JWT tokens are validated.
///     For example, the ValidateIssuerSigningKey option indicates that the token signature should be verified using a
///     private key.
///     The IssuerSigningKey property specifies the key to be used for this validation. The ValidateIssuer, ValidIssuer,
///     ValidateAudience, and ValidAudience options specify that the token should contain specific values for the "iss" and
///     "aud" claims and that these values should be validated.
///     The ValidateLifetime option indicates that the token should be checked for expiration, and the ClockSkew option
///     allows for a small amount of clock skew when checking the expiration time.
/// </summary>
public static class ApiIdentityConfig
{
    public static void AddApiIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // The key length needs to be of sufficient length, or otherwise an error will occur.
        var tokenSecretKey = Encoding.UTF8.GetBytes(configuration["JWTSettings:Secret"] ??
                                                    throw new InvalidOperationException("JWT secret not found"));

        var tokenValidationParameters = new TokenValidationParameters
        {
            // Token signature will be verified using a private key.
            ValidateIssuerSigningKey = true,
            RequireSignedTokens = true,
            IssuerSigningKey = new SymmetricSecurityKey(tokenSecretKey),

            // Token will only be valid for "iss" claim.
            ValidateIssuer = true,
            ValidIssuer = configuration["JWTSettings:Issuer"],

            // Token will only be valid for "aud" claim.
            ValidateAudience = true,
            ValidAudience = configuration["JWTSettings:Audience"],

            // Token will only be valid if not expired yet, with 5 minutes clock skew.
            ValidateLifetime = true,
            RequireExpirationTime = true,
            ClockSkew = new TimeSpan(0, 5, 0),

            ValidateActor = false
        };

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => { options.TokenValidationParameters = tokenValidationParameters; });


        services.AddScoped<IJwtBuilder, JwtBuilder>();
    }
}
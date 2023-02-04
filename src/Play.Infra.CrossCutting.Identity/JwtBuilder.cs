using System;
using System.Collections.Generic;
using System.Text;
using Jose;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Play.Domain.Core.Models;
using Play.Infra.CrossCutting.Identity.Interfaces;

namespace Play.Infra.CrossCutting.Identity;

/// <summary>
///     The jwt builder service is responsible for creating and validating JWT tokens.
/// </summary>
public class JwtBuilder : IJwtBuilder
{
    private readonly IConfiguration _configuration;

    public JwtBuilder(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    ///     Function to generate a JWT token for a given user.
    /// </summary>
    /// <param name="user">The user for which the token is generated.</param>
    /// <returns>The generated JWT token.</returns>
    public string GenerateToken(User user)
    {
        //load JWTSettings section from app settings.json
        var jwtSettings = _configuration.GetSection("JWTSettings");
        //get the secret key from the section
        var secretKey = jwtSettings.GetSection("Secret").Value;
        //get the issuer from the section
        var issuer = jwtSettings.GetSection("Issuer").Value;
        //get the audience from the section
        var audience = jwtSettings.GetSection("Audience").Value;
        //get the expiry time from the section (in hours)
        var expiry = int.Parse(jwtSettings.GetSection("AccessTokenExpiration").Value ??
                               throw new InvalidOperationException(
                                   "Access token expiration not found in appsettings.json"));

        //based on the expiry value generate exp and iat claims (in unix time)
        var exp = DateTimeOffset.UtcNow.AddHours(expiry).ToUnixTimeSeconds();
        var iat = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        //if user role is PlayBot set expiry to 10 minutes
        if (user.Role == "PlayBot") exp = DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds();

        //build payload
        var payload = new Dictionary<string, object>
        {
            { "sub", user.Id },
            { "exp", exp },
            { "iat", iat },
            { "iss", issuer },
            { "aud", audience },
            { "email", user.Email },
            { "role", user.Role }
        };

        //convert secret key to byte array
        var key = Encoding.UTF8.GetBytes(secretKey ?? throw new InvalidOperationException("Secret key is missing"));

        var token = JWT.Encode(payload, key, JwsAlgorithm.HS256);

        return token;
    }

    /// <summary>
    ///     Function to decode/validate a JWT token.
    /// </summary>
    /// <param name="token">The token to be decoded.</param>
    /// <returns>The decoded token.</returns>
    /// <exception cref="Exception">Thrown when the token is invalid.</exception>
    public string DecodeToken(string token)
    {
        //load JWTSettings section from app settings.json
        var jwtSettings = _configuration.GetSection("JWTSettings");
        //get the secret key from the section
        var secretKey = jwtSettings.GetSection("Secret").Value;

        //convert secret key to byte array
        var key = Encoding.UTF8.GetBytes(secretKey);

        //decode token
        var decodedToken = JWT.Decode(token, key);

        return decodedToken;
    }

    /// <summary>
    ///     Refresh a JWT token.
    ///     We will allow for the token to be expired up to 15 minutes.
    /// </summary>
    /// <param name="token">The token to be refreshed.</param>
    /// <returns>The refreshed token.</returns>
    public string RefreshToken(string token)
    {
        //load JWTSettings section from app settings.json
        var jwtSettings = _configuration.GetSection("JWTSettings");
        //get the secret key from the section
        var secretKey = jwtSettings.GetSection("Secret").Value;
        //get the issuer from the section
        var issuer = jwtSettings.GetSection("Issuer").Value;
        //get the audience from the section
        var audience = jwtSettings.GetSection("Audience").Value;
        //get the expiry time from the section (in hours)
        var expiry = int.Parse(jwtSettings.GetSection("AccessTokenExpiration").Value ??
                               throw new InvalidOperationException(
                                   "Access token expiration not found in appsettings.json"));

        //convert secret key to byte array
        var key = Encoding.UTF8.GetBytes(secretKey);

        //decode token
        var decodedToken = JWT.Decode(token, key);

        //get the exp claim from the decoded token
        var exp = Convert.ToInt64(JObject.Parse(decodedToken)["exp"]);
        //create new exp claim 4 hours from now
        var newExp = DateTimeOffset.UtcNow.AddHours(expiry).ToUnixTimeSeconds();

        //create new iat for 4 from now
        var newIat = DateTimeOffset.UtcNow.AddHours(expiry).ToUnixTimeSeconds();

        //get the email claim from the decoded token
        var email = JObject.Parse(decodedToken)["email"]?.ToString();

        //get the sub claim from the decoded token
        var sub = JObject.Parse(decodedToken)["sub"]?.ToString();

        var role = JObject.Parse(decodedToken)["role"]?.ToString();

        //get the current time in unix time
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        //check if the token is expired by more than 15 minutes
        if (now > exp + 900)
            //throw exception
            throw new Exception("Token expired");

        //build payload
        var payload = new Dictionary<string, object>
        {
            { "sub", sub },
            { "exp", newExp },
            { "iat", newIat },
            { "iss", issuer },
            { "aud", audience },
            { "email", email },
            { "role", role }
        };

        //refresh token
        var refreshedToken = JWT.Encode(payload, key, JwsAlgorithm.HS256);

        return refreshedToken;
    }
}
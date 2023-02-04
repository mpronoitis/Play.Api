using System;
using Play.Domain.Core.Models;

namespace Play.Infra.CrossCutting.Identity.Interfaces;

public interface IJwtBuilder
{
    /// <summary>
    ///     Function to generate a JWT token for a given user.
    /// </summary>
    /// <param name="user">The user for which the token is generated.</param>
    /// <returns>The generated JWT token.</returns>
    string GenerateToken(User user);

    /// <summary>
    ///     Function to decode/validate a JWT token.
    /// </summary>
    /// <param name="token">The token to be decoded.</param>
    /// <returns>The decoded token.</returns>
    /// <exception cref="TokenExpiredException">Thrown when the token is expired.</exception>
    /// <exception cref="SignatureVerificationException">Thrown when the token signature is invalid.</exception>
    /// <exception cref="Exception">Thrown when the token is invalid.</exception>
    string DecodeToken(string token);

    /// <summary>
    ///     Refresh a JWT token.
    ///     We will allow for the token to be expired up to 15 minutes.
    /// </summary>
    /// <param name="token">The token to be refreshed.</param>
    /// <returns>The refreshed token.</returns>
    /// <exception cref="TokenExpiredException">Thrown when the token is expired.</exception>
    /// \
    /// <exception cref="SignatureVerificationException">Thrown when the token signature is invalid.</exception>
    string RefreshToken(string token);
}
using System;
using NetDevPack.Domain;

namespace Play.Domain.Core.Models;

/**
 * The user entity is to be used as the base for all the users of the system.
 * This entity will be used in par with authentication and authorization.
 */
public class User : Entity, IAggregateRoot
{
    /// <summary>
    ///     Constructor with params
    /// </summary>
    public User(Guid id, string email, string passwordHash, string salt, string username, string role,
        int loginAttempts, int failedLoginAttempts, DateTime? lastLogin, string otpSecret, DateTime createdAt)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        Salt = salt;
        Username = username;
        Role = role;
        LoginAttempts = loginAttempts;
        FailedLoginAttempts = failedLoginAttempts;
        LastLogin = lastLogin;
        OtpSecret = otpSecret;
        CreatedAt = createdAt;
    }

    /// <summary>
    ///     Empty constructor for ef
    /// </summary>
    public User()
    {
    }

    /// <summary>
    ///     Email of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Password hash of the user encrypted with PBKDF2.
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    ///     Salt used to encrypt the password.
    /// </summary>
    public string Salt { get; set; }

    /// <summary>
    ///     Username of the user.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    ///     The role of the user , default is Customer
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    ///     Number of successful login attempts.
    /// </summary>
    public int LoginAttempts { get; set; }

    /// <summary>
    ///     Number of failed login attempts.
    /// </summary>
    public int FailedLoginAttempts { get; set; }

    /// <summary>
    ///     Last time the user logged in.
    /// </summary>
    public DateTime? LastLogin { get; set; }

    /// <summary>
    ///     One Time Password Secret
    /// </summary>
    public string OtpSecret { get; set; }

    /// <summary>
    ///     Date of creation of the user.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
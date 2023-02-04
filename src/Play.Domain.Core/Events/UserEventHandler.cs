using System;
using System.Threading;
using System.Threading.Tasks;
using FluentEmail.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;
using Play.Domain.Core.Interfaces;
using Play.Domain.Core.Models;

namespace Play.Domain.Core.Events;

public class UserEventHandler : INotificationHandler<UserRegisteredEvent>,
    INotificationHandler<UserRemovedEvent>,
    INotificationHandler<UserUpdatedEvent>,
    INotificationHandler<UserPasswordUpdatedEvent>,
    INotificationHandler<ForgotPasswordEvent>
{
    private readonly IEmailTemplateRepository _emailTemplateRepository;
    private readonly IFluentEmail _fluentEmail;
    private readonly ILogger<UserEventHandler> _logger;
    private readonly IUserProfileRepository _userProfileRepository;


    public UserEventHandler(IUserProfileRepository userProfileRepository, IFluentEmail fluentEmail,
        ILogger<UserEventHandler> logger, IEmailTemplateRepository emailTemplateRepository)
    {
        _userProfileRepository = userProfileRepository;
        _fluentEmail = fluentEmail;
        _logger = logger;
        _emailTemplateRepository = emailTemplateRepository;
    }

    public async Task Handle(ForgotPasswordEvent notification, CancellationToken cancellationToken)
    {
        //get email template with name "Forgot Password"
        var emailTemplate = await _emailTemplateRepository.GetByNameAsync("Forgot Password");

        //if no template found, log error and return
        if (emailTemplate.Count == 0)
        {
            _logger.LogError("Email template {0} not found", "Forgot Password");
            return;
        }

        //send email to user
        var email = _fluentEmail
            .To(notification.Email)
            .Subject("Reset your password 🔓🗝")
            .UsingTemplate(emailTemplate[0].Body, new
            {
                Username = notification.Email, notification.Otp
            });
        try
        {
            await email.SendAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Failed to send reset password message");
        }
    }


    public async Task Handle(UserPasswordUpdatedEvent notification, CancellationToken cancellationToken)
    {
        //get email template with name "Playsystems - Password Changed"
        var emailTemplate = await _emailTemplateRepository.GetByNameAsync("Password Changed");

        //if no template found, log error and return
        if (emailTemplate.Count == 0)
        {
            _logger.LogError("Email template {0} not found", "Playsystems - Password Changed");
            return;
        }

        //send email
        var email = _fluentEmail
            .To(notification.Email)
            .Subject("Your password has been updated 🔑")
            .UsingTemplate(emailTemplate[0].Body, new
            {
                Username = notification.Email
            });
        // cancelationToken required so it won't dispose
        try
        {
            await email.SendAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Failed to send password update notification");
        }
    }


    public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
    {
        //check that user does not already have a profile
        var userProfile = await _userProfileRepository.GetByUserId(notification.Id);
        if (userProfile != null) return;
        //create a new profile for the user
        //from the email get the domain
        var domain = notification.Email.Split('@')[1];
        var newUserProfile =
            new UserProfile(Guid.NewGuid(), notification.Id, notification.Email, " ", DateTime.Now, domain, "en",
                "light", "0");
        _userProfileRepository.Add(newUserProfile);


        //get email template with name "Signup Email"
        var emailTemplate = await _emailTemplateRepository.GetByNameAsync("Signup Email");

        //if no template found, log error and return
        if (emailTemplate.Count == 0)
        {
            _logger.LogError("Email template {0} not found", "Signup Email");
            return;
        }

        //send email to user
        var email = _fluentEmail
            .To(notification.Email)
            .Subject("Welcome to Play 🎉")
            .UsingTemplate(emailTemplate[0].Body, new
            {
                Username = notification.Email
            });
        try
        {
            await email.SendAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Failed to send that user is registered");
        }
    }

    public async Task Handle(UserRemovedEvent notification, CancellationToken cancellationToken)
    {
        //remove user profile
        var userProfile = await _userProfileRepository.GetByUserId(notification.Id);
        if (userProfile != null) _userProfileRepository.Remove(userProfile);

        //get email template with name "User Deleted"
        var emailTemplate = await _emailTemplateRepository.GetByNameAsync("User Deleted");

        //if no template found, log error and return
        if (emailTemplate.Count == 0)
        {
            _logger.LogError("Email template {0} not found", "User Deleted");
            return;
        }

        //send email to user
        var email = _fluentEmail
            .To(notification.Email)
            .Subject("Goodbye from Play 😢")
            .UsingTemplate(emailTemplate[0].Body, new
            {
                Username = notification.Email
            });

        try
        {
            await email.SendAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Failed to send Goodbye Message");
        }
    }

    public Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public class UserRegisteredEvent : Event
{
    public UserRegisteredEvent(Guid id, string email, string passwordHash, string salt, string username,
        int loginAttempts, int failedLoginAttempts, DateTime? lastLogin, DateTime createdAt)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        Salt = salt;
        Username = username;
        LoginAttempts = loginAttempts;
        FailedLoginAttempts = failedLoginAttempts;
        LastLogin = lastLogin;
        CreatedAt = createdAt;
    }

    public Guid Id { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Salt { get; set; }


    public string Username { get; set; }


    public int LoginAttempts { get; set; }


    public int FailedLoginAttempts { get; set; }


    public DateTime? LastLogin { get; set; }

    public DateTime CreatedAt { get; set; }
}

public class UserRemovedEvent : Event
{
    public UserRemovedEvent(Guid id, string Email)
    {
        Id = id;
        this.Email = Email;
    }

    public string Email { get; set; }
    public Guid Id { get; set; }
}

public class UserUpdatedEvent : Event
{
    public UserUpdatedEvent(Guid id, string email, string passwordHash, string salt, string username, int loginAttempts,
        int failedLoginAttempts, DateTime? lastLogin, DateTime createdAt)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        Salt = salt;
        Username = username;
        LoginAttempts = loginAttempts;
        FailedLoginAttempts = failedLoginAttempts;
        LastLogin = lastLogin;
        CreatedAt = createdAt;
    }

    public Guid Id { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Salt { get; set; }

    public string Username { get; set; }

    public int LoginAttempts { get; set; }

    public int FailedLoginAttempts { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime CreatedAt { get; set; }
}

public class UserPasswordUpdatedEvent : Event
{
    public UserPasswordUpdatedEvent(Guid id, string email)
    {
        Id = id;
        Email = email;
    }

    public Guid Id { get; set; }
    public string Email { get; set; }
}

public class UpdateRoleUserEvent : Event
{
    public UpdateRoleUserEvent(Guid id, string email, string role)
    {
        Id = id;
        Email = email;
        Role = role;
    }

    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}

public class ForgotPasswordEvent : Event
{
    public ForgotPasswordEvent(Guid id, string email, string otp)
    {
        Id = id;
        Email = email;
        Otp = otp;
    }

    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Otp { get; set; }
}
using System;
using System.Diagnostics;
using NetDevPack.Messaging;
using Play.Domain.Core.Commands.Validations;

namespace Play.Domain.Core.Commands;

public class UserCommand : Command
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    public string Old_Password { get; set; }
    public string Password { get; set; }
    public string PasswordHash { get; set; }

    public string Role { get; set; }

    public string Salt { get; set; }


    public string Username { get; set; }


    public int LoginAttempts { get; set; }


    public int FailedLoginAttempts { get; set; }


    public DateTime? LastLogin { get; set; }

    public DateTime CreatedAt { get; set; }
}

public class RegisterUserCommand : UserCommand
{
    public RegisterUserCommand(string email, string password)
    {
        Debug.Print($"RegisterUserCommand: {email}");
        Email = email;
        Password = password;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterUserValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemoveUserCommand : UserCommand
{
    public RemoveUserCommand(Guid id)
    {
        Id = id;
        AggregateId = id;
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveUserValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateUserCommand : UserCommand
{
    public UpdateUserCommand(Guid id, string email, string password)
    {
        Id = id;
        Email = email;
        Password = password;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateUserValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateUserPasswordCommand : UserCommand
{
    public UpdateUserPasswordCommand(Guid id, string email, string password, string oldPassword)
    {
        Id = id;
        Email = email;
        Password = password;
        Old_Password = oldPassword;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateUserPasswordValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateUserRoleCommand : UserCommand

{
    public UpdateUserRoleCommand(Guid id, string email, string role)
    {
        Id = id;
        Email = email;
        Role = role;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateUserRoleValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class ForgotPasswordCommand : UserCommand
{
    public ForgotPasswordCommand(string email)
    {
        Email = email;
    }

    public override bool IsValid()
    {
        ValidationResult = new ForgotPasswordValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
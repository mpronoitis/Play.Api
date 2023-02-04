using System;
using System.Linq;
using FluentValidation;

namespace Play.Domain.Core.Commands.Validations;

public class UserValidation<T> : AbstractValidator<T> where T : UserCommand
{
    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty).WithMessage("Id is required")
            .NotEmpty().WithMessage("Id is required");
    }

    protected void ValidateEmail()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email")
            .MaximumLength(100).WithMessage("Email cannot be longer than 100 characters");
    }

    protected void ValidateRole()
    {
        var roles = new[] { "PlayAdmin", "Customer", "PlayBot" };
        RuleFor(c => c.Role)
            .NotEmpty().WithMessage("Role is required")
            .MaximumLength(50).WithMessage("Role cannot be longer than 50 characters")
            .Must(x => roles.Contains(x)).WithMessage("Invalid Role");
    }

    protected void ValidatePasswordHash()
    {
        RuleFor(c => c.PasswordHash)
            .NotEmpty().WithMessage("PasswordHash is required");
    }

    protected void ValidatePasswordSalt()
    {
        RuleFor(c => c.Salt)
            .NotEmpty().WithMessage("PasswordSalt is required")
            .MaximumLength(64).WithMessage("PasswordSalt cannot be longer than 64 characters");
    }

    protected void ValidateUsername()
    {
        RuleFor(c => c.Username)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(100).WithMessage("Username cannot be longer than 100 characters");
    }

    protected void ValidateLoginAttempts()
    {
        RuleFor(c => c.LoginAttempts)
            .NotEmpty().WithMessage("LoginAttempts is required")
            .GreaterThanOrEqualTo(0).WithMessage("LoginAttempts must be greater than or equal to 0");
    }

    protected void ValidateFailLoginAttempts()
    {
        RuleFor(c => c.FailedLoginAttempts)
            .NotEmpty().WithMessage("FailLoginAttempts is required")
            .GreaterThanOrEqualTo(0).WithMessage("FailLoginAttempts must be greater than or equal to 0");
    }

    protected void ValidatePassword()
    {
        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .MaximumLength(100).WithMessage("Password cannot be longer than 100 characters");
    }
}

public class RegisterUserValidation : UserValidation<RegisterUserCommand>
{
    public RegisterUserValidation()
    {
        ValidateEmail();
        ValidatePassword();
    }
}

public class UpdateUserValidation : UserValidation<UpdateUserCommand>
{
    public UpdateUserValidation()
    {
        ValidateId();
        ValidateEmail();
    }
}

public class UpdateUserPasswordValidation : UserValidation<UpdateUserPasswordCommand>
{
    public UpdateUserPasswordValidation()
    {
        ValidateId();
        ValidatePassword();
    }
}

public class RemoveUserValidation : UserValidation<RemoveUserCommand>
{
    public RemoveUserValidation()
    {
        ValidateId();
    }
}

public class ForgotPasswordValidation : UserValidation<ForgotPasswordCommand>
{
    public ForgotPasswordValidation()
    {
        ValidateEmail();
    }
}

public class UpdateUserRoleValidation : UserValidation<UpdateUserRoleCommand>
{
    public UpdateUserRoleValidation()
    {
        ValidateId();
        ValidateRole();
    }
}
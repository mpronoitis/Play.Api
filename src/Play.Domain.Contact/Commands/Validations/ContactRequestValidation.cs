using FluentValidation;

namespace Play.Domain.Contact.Commands.Validations;

public class ContactRequestValidation<T> : AbstractValidator<T> where T : ContactRequestCommand
{
    protected void ValidateEmail()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email")
            .MaximumLength(100).WithMessage("Email cannot be longer than 100 characters");
    }

    protected void ValidateSubject()
    {
        RuleFor(c => c.Subject)
            .NotEmpty().WithMessage("Subject is required")
            .MaximumLength(100).WithMessage("Subject cannot be longer than 100 characters");
    }
}

public class RegisterContactRequestValidation : ContactRequestValidation<RegisterContactRequestCommand>
{
    public RegisterContactRequestValidation()
    {
        ValidateEmail();
        ValidateSubject();
    }
}
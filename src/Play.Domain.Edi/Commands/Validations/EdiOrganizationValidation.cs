using System;
using FluentValidation;

namespace Play.Domain.Edi.Commands.Validations;

public abstract class EdiOrganizationValidation<T> : AbstractValidator<T> where T : EdiOrganizationCommand
{
    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty);
    }

    protected void ValidateName()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("The name is required")
            .Length(2, 150).WithMessage("The name must have between 2 and 150 characters");
    }

    protected void ValidateEmail()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("The email is required")
            .EmailAddress().WithMessage("The email is not valid");
    }
}

public class RegisterEdiOrganizationCommandValidation : EdiOrganizationValidation<RegisterEdiOrganizationCommand>
{
    public RegisterEdiOrganizationCommandValidation()
    {
        ValidateName();
        ValidateEmail();
    }
}

public class UpdateEdiOrganizationCommandValidation : EdiOrganizationValidation<UpdateEdiOrganizationCommand>
{
    public UpdateEdiOrganizationCommandValidation()
    {
        ValidateId();
        ValidateName();
        ValidateEmail();
    }
}

public class RemoveEdiOrganizationCommandValidation : EdiOrganizationValidation<RemoveEdiOrganizationCommand>
{
    public RemoveEdiOrganizationCommandValidation()
    {
        ValidateId();
    }
}
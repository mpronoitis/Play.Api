using System;
using FluentValidation;

namespace Play.Domain.Core.Commands.Validations;

public class EmailTemplateValidation<T> : AbstractValidator<T> where T : EmailTemplateCommand
{
    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty).WithMessage("Id is required");
    }

    protected void ValidateTemplateId()
    {
        RuleFor(c => c.EmailTemplate.Id)
            .NotEqual(Guid.Empty).WithMessage("Template Id is required");
    }

    protected void ValidateName()
    {
        RuleFor(c => c.EmailTemplate.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 100 characters");
    }

    protected void ValidateSubject()
    {
        RuleFor(c => c.EmailTemplate.Subject)
            .NotEmpty().WithMessage("Subject is required")
            .Length(2, 150).WithMessage("Subject must be between 2 and 100 characters");
    }

    protected void ValidateBody()
    {
        RuleFor(c => c.EmailTemplate.Body)
            .NotEmpty().WithMessage("Body is required");
    }
}

public class RegisterEmailTemplateValidation : EmailTemplateValidation<RegisterNewEmailTemplateCommand>
{
    public RegisterEmailTemplateValidation()
    {
        ValidateName();
        ValidateSubject();
        ValidateBody();
    }
}

public class UpdateEmailTemplateValidation : EmailTemplateValidation<UpdateEmailTemplateCommand>
{
    public UpdateEmailTemplateValidation()
    {
        ValidateTemplateId();
        ValidateName();
        ValidateSubject();
        ValidateBody();
    }
}

public class RemoveEmailTemplateValidation : EmailTemplateValidation<RemoveEmailTemplateCommand>
{
    public RemoveEmailTemplateValidation()
    {
        ValidateId();
    }
}
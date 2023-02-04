using System;
using FluentValidation;

namespace Play.Domain.Edi.Commands.Validations;

public class EdiVariableValidation<T> : AbstractValidator<T> where T : EdiVariableCommand
{
    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is required")
            .NotEqual(Guid.Empty).WithMessage("Invalid Id");
    }

    protected void ValidateTitle()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Title is required")
            .Length(0, 50).WithMessage("Title cannot have more than 50 characters");
    }

    protected void ValidateDescription()
    {
        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("Description is required")
            .Length(0, 500).WithMessage("Description cannot have more than 500 characters");
    }

    protected void ValidatePlaceholder()
    {
        RuleFor(c => c.Placeholder)
            .NotEmpty().WithMessage("Placeholder is required")
            .Length(0, 50).WithMessage("Placeholder cannot have more than 50 characters");
    }
}

public class RegisterEdiVariableValidation : EdiVariableValidation<RegisterEdiVariableCommand>
{
    public RegisterEdiVariableValidation()
    {
        ValidateTitle();
        ValidateDescription();
        ValidatePlaceholder();
    }
}

public class UpdateEdiVariableValidation : EdiVariableValidation<UpdateEdiVariableCommand>
{
    public UpdateEdiVariableValidation()
    {
        ValidateId();
        ValidateTitle();
        ValidateDescription();
        ValidatePlaceholder();
    }
}

public class RemoveEdiVariableValidation : EdiVariableValidation<RemoveEdiVariableCommand>
{
    public RemoveEdiVariableValidation()
    {
        ValidateId();
    }
}
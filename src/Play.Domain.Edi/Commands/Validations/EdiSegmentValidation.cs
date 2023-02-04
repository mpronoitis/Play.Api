using System;
using FluentValidation;

namespace Play.Domain.Edi.Commands.Validations;

public class EdiSegmentValidation<T> : AbstractValidator<T> where T : EdiSegmentCommand
{
    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is required")
            .NotEqual(Guid.Empty).WithMessage("Id is invalid");
    }

    protected void ValidateModelId()
    {
        RuleFor(c => c.Model_Id)
            .NotEmpty().WithMessage("ModelId is required")
            .NotEqual(Guid.Empty).WithMessage("ModelId is invalid");
    }

    protected void ValidateTitle()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Title is required")
            .Length(0, 50).WithMessage("Title must be between 0 and 50 characters");
    }

    protected void ValidateDescription()
    {
        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("Description is required")
            .Length(0, 500).WithMessage("Description must be between 0 and 500 characters");
    }
}

public class RegisterEdiSegmentCommandValidation : EdiSegmentValidation<RegisterEdiSegmentCommand>
{
    public RegisterEdiSegmentCommandValidation()
    {
        ValidateModelId();
        ValidateTitle();
        ValidateDescription();
    }
}

public class UpdateEdiSegmentCommandValidation : EdiSegmentValidation<UpdateEdiSegmentCommand>
{
    public UpdateEdiSegmentCommandValidation()
    {
        ValidateId();
        ValidateModelId();
        ValidateTitle();
        ValidateDescription();
    }
}

public class RemoveEdiSegmentCommandValidation : EdiSegmentValidation<RemoveEdiSegmentCommand>
{
    public RemoveEdiSegmentCommandValidation()
    {
        ValidateId();
    }
}
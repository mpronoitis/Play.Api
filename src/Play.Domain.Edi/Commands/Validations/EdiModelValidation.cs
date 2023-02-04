using System;
using FluentValidation;

namespace Play.Domain.Edi.Commands.Validations;

public class EdiModelValidation<T> : AbstractValidator<T> where T : EdiModelCommand
{
    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty);
    }

    protected void ValidateOrgId()
    {
        RuleFor(c => c.Org_id)
            .NotEqual(Guid.Empty);
    }

    protected void ValidateTitle()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Please ensure you have entered the Title")
            .Length(0, 50).WithMessage("The Title must be between 0 and 50 characters");
    }

    protected void ValidateSegmentTerminator()
    {
        //segment terminator is a char
        RuleFor(c => c.SegmentTerminator)
            .NotEmpty().WithMessage("Please ensure you have entered the Segment Terminator");
    }

    protected void ValidateSubElementSeperator()
    {
        //sub element seperator is a char
        RuleFor(c => c.SubElementSeperator)
            .NotEmpty().WithMessage("Please ensure you have entered the Sub Element Seperator");
    }

    protected void ValidateElementSeparator()
    {
        //element separator is a char
        RuleFor(c => c.ElementSeparator)
            .NotEmpty().WithMessage("Please ensure you have entered the Element Seperator");
    }

    protected void ValidateEnabled()
    {
        //enabled is a bool
        RuleFor(c => c.Enabled)
            .NotEmpty().WithMessage("Please ensure you have entered the Enabled flag");
    }
}

public class RegisterEdiModelCommandValidation : EdiModelValidation<RegisterEdiModelCommand>
{
    public RegisterEdiModelCommandValidation()
    {
        ValidateOrgId();
        ValidateTitle();
        ValidateSegmentTerminator();
        ValidateSubElementSeperator();
        ValidateElementSeparator();
        ValidateEnabled();
    }
}

public class UpdateEdiModelCommandValidation : EdiModelValidation<UpdateEdiModelCommand>
{
    public UpdateEdiModelCommandValidation()
    {
        ValidateId();
        ValidateOrgId();
        ValidateTitle();
        ValidateSegmentTerminator();
        ValidateSubElementSeperator();
        ValidateElementSeparator();
        ValidateEnabled();
    }
}

public class RemoveEdiModelCommandValidation : EdiModelValidation<RemoveEdiModelCommand>
{
    public RemoveEdiModelCommandValidation()
    {
        ValidateId();
    }
}
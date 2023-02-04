using System;
using System.Text.RegularExpressions;
using FluentValidation;

namespace Play.Domain.Edi.Commands.Validations;

public class EdiProfileValidation<T> : AbstractValidator<T> where T : EdiProfileCommand
{
    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is required")
            .NotEqual(Guid.Empty).WithMessage("Invalid Id");
    }

    protected void ValidateCustomerId()
    {
        RuleFor(c => c.Customer_Id)
            .NotEmpty().WithMessage("CustomerId is required")
            .NotEqual(Guid.Empty).WithMessage("Invalid CustomerId");
    }

    protected void ValidateModelId()
    {
        RuleFor(c => c.Model_Id)
            .NotEmpty().WithMessage("ModelId is required")
            .NotEqual(Guid.Empty).WithMessage("Invalid ModelId");
    }

    protected void ValidateTitle()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(50).WithMessage("Title must be less than 50 characters");
    }

    protected void ValidatePayload()
    {
        RuleFor(c => c.Payload)
            .NotEmpty().WithMessage("Payload is required")
            .Must(HasNoHtmlTags).WithMessage("Payload must not contain HTML tags");
    }

    protected void ValidateEnabled()
    {
        RuleFor(c => c.Enabled)
            .NotEmpty().WithMessage("Enabled is required");
    }

    //function to check that a string does not contain any html tags 
    private bool HasNoHtmlTags(string value)
    {
        //regex to check for html tags
        var regex = new Regex("<.*?>");
        //if the string contains html tags, throw an error
        return !regex.IsMatch(value);
    }
}

public class RegisterEdiProfileCommandValidation : EdiProfileValidation<RegisterEdiProfileCommand>
{
    public RegisterEdiProfileCommandValidation()
    {
        ValidateCustomerId();
        ValidateModelId();
        ValidateTitle();
        ValidatePayload();
        ValidateEnabled();
    }
}

public class UpdateEdiProfileCommandValidation : EdiProfileValidation<UpdateEdiProfileCommand>
{
    public UpdateEdiProfileCommandValidation()
    {
        ValidateId();
        ValidateCustomerId();
        ValidateModelId();
        ValidateTitle();
        ValidatePayload();
        ValidateEnabled();
    }
}

public class RemoveEdiProfileCommandValidation : EdiProfileValidation<RemoveEdiProfileCommand>
{
    public RemoveEdiProfileCommandValidation()
    {
        ValidateId();
    }
}
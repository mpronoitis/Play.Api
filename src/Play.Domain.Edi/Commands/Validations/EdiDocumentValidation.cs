using System;
using FluentValidation;

namespace Play.Domain.Edi.Commands.Validations;

/// <summary>
///     Validator for the <see cref="EdiDocumentCommandHandler" /> commands.
/// </summary>
/// <typeparam name="T"></typeparam>
public class EdiDocumentValidation<T> : AbstractValidator<T> where T : EdiDocumentCommand
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

    protected void ValidateEdiPayload()
    {
        RuleFor(c => c.EdiPayload)
            .NotEmpty().WithMessage("EdiPayload is required");
    }

    protected void ValidateDocumentPayload()
    {
        //document payload must be a valid JSON string
        RuleFor(c => c.DocumentPayload)
            .NotEmpty().WithMessage("DocumentPayload is required");
    }

    protected void ValidateHedentid()
    {
        RuleFor(c => c.Hedentid)
            .NotEmpty().WithMessage("Hedentid is required");
    }

    protected void ValidateIsProcessed()
    {
        RuleFor(c => c.IsProcessed)
            .NotEmpty().WithMessage("IsProcessed is required");
    }
}

public class RegisterEdiDocumentValidation : EdiDocumentValidation<RegisterEdiDocumentCommand>
{
    public RegisterEdiDocumentValidation()
    {
        ValidateCustomerId();
        ValidateEdiPayload();
        ValidateDocumentPayload();
        ValidateHedentid();
        ValidateIsProcessed();
    }
}

public class UpdateEdiDocumentValidation : EdiDocumentValidation<UpdateEdiDocumentCommand>
{
    public UpdateEdiDocumentValidation()
    {
        ValidateId();
        ValidateCustomerId();
        ValidateEdiPayload();
        ValidateDocumentPayload();
        ValidateHedentid();
        ValidateIsProcessed();
    }
}

public class RemoveEdiDocumentValidation : EdiDocumentValidation<RemoveEdiDocumentCommand>
{
    public RemoveEdiDocumentValidation()
    {
        ValidateId();
    }
}

public class ReceivedEdiDocumentValidation : EdiDocumentValidation<ReceivedEdiDocumentCommand>
{
    public ReceivedEdiDocumentValidation()
    {
        ValidateCustomerId();
        ValidateDocumentPayload();
        ValidateHedentid();
    }
}
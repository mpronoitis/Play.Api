using System;
using FluentValidation;

namespace Play.Domain.Edi.Commands.Validations;

public class EdiCreditValidation<T> : AbstractValidator<T> where T : EdiCreditCommand
{
    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty);
    }

    protected void ValidateAmount()
    {
        //must be number (can be negative)
        RuleFor(c => c.Credit.Amount)
            .Must(a => a.GetType().IsPrimitive)
            .WithMessage("Amount must be a number");
    }

    protected void ValidateCustomerId()
    {
        //must be a valid guid
        RuleFor(c => c.Credit.CustomerId)
            .NotEqual(Guid.Empty);
    }
}

public class RegisterNewEdiCreditCommandValidation : EdiCreditValidation<RegisterEdiCreditCommand>
{
    public RegisterNewEdiCreditCommandValidation()
    {
        ValidateAmount();
        ValidateCustomerId();
    }
}

public class UpdateEdiCreditCommandValidation : EdiCreditValidation<UpdateEdiCreditCommand>
{
    public UpdateEdiCreditCommandValidation()
    {
        ValidateId();
        ValidateAmount();
        ValidateCustomerId();
    }
}

public class RemoveEdiCreditCommandValidation : EdiCreditValidation<RemoveEdiCreditCommand>
{
    public RemoveEdiCreditCommandValidation()
    {
        ValidateId();
    }
}
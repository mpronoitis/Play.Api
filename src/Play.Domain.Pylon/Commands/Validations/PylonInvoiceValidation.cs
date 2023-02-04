using FluentValidation;

namespace Play.Domain.Pylon.Commands.Validations;

public class PylonInvoiceValidation<T> : AbstractValidator<T> where T : PylonInvoiceCommand
{
    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is required")
            .NotEqual(Guid.Empty).WithMessage("Invalid Id");
    }
}

public class RegisterPylonInvoiceCommandValidation : PylonInvoiceValidation<RegisterPylonInvoiceCommand>
{
}

public class RemovePylonInvoiceCommandValidation : PylonInvoiceValidation<RemovePylonInvoiceCommand>
{
    public RemovePylonInvoiceCommandValidation()
    {
        ValidateId();
    }
}
using FluentValidation;

namespace Play.Domain.Whmcs.Commands.Validations;

public class WhmcsOrderValidation<T> : AbstractValidator<T> where T : WhmcsAddOrderCommand
{
    protected void ValidateClientId()
    {
        RuleFor(c => c.Order.ClientId)
            .NotEmpty().WithMessage("Please ensure you have entered the client id")
            .GreaterThan(0).WithMessage("Please ensure you have entered the client id");
    }

    protected void ValidatePaymentMethod()
    {
        RuleFor(c => c.Order.PaymentMethod)
            .NotEmpty().WithMessage("Please ensure you have entered the payment method")
            .Must(BeAValidPaymentMethod).WithMessage("Please ensure you have entered a valid payment method");
    }

    private static bool BeAValidPaymentMethod(string arg)
    {
        return arg is "mailin" or "stripe";
    }
}

public class AddWhmcsOrderCommandValidation : WhmcsOrderValidation<WhmcsAddOrderCommand>
{
    public AddWhmcsOrderCommandValidation()
    {
        ValidateClientId();
        ValidatePaymentMethod();
    }
}
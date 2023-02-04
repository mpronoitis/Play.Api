using FluentValidation;

namespace Play.Domain.Whmcs.Commands.Validations;

public class WhmcsAcceptOrderValidation<T> : AbstractValidator<T> where T : WhmcsAcceptOrderCommand
{
    protected void ValidateOrderId()
    {
        RuleFor(c => c.AcceptOrder.OrderId)
            .NotEmpty().WithMessage("Please ensure you have entered the order id");
    }
}

public class AcceptWhmcsOrderCommandValidation : WhmcsAcceptOrderValidation<AcceptWhmcsOrderCommand>
{
    public AcceptWhmcsOrderCommandValidation()
    {
        ValidateOrderId();
    }
}
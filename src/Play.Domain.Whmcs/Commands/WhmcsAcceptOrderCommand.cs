using NetDevPack.Messaging;
using Play.Domain.Whmcs.Commands.Validations;
using Play.Domain.Whmcs.Models;

namespace Play.Domain.Whmcs.Commands;

public class WhmcsAcceptOrderCommand : Command
{
    public WhmcsAcceptOrder AcceptOrder { get; protected set; } = null!;
}

public class AcceptWhmcsOrderCommand : WhmcsAcceptOrderCommand
{
    public AcceptWhmcsOrderCommand(WhmcsAcceptOrder acceptOrder)
    {
        AcceptOrder = acceptOrder;
    }

    public override bool IsValid()
    {
        ValidationResult = new AcceptWhmcsOrderCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
using NetDevPack.Messaging;
using Play.Domain.Whmcs.Commands.Validations;
using Play.Domain.Whmcs.Models;

namespace Play.Domain.Whmcs.Commands;

public class WhmcsAddOrderCommand : Command
{
    public WhmcsAddOrder Order { get; protected set; } = null!;
}

public class AddWhmcsOrderCommand : WhmcsAddOrderCommand
{
    public AddWhmcsOrderCommand(WhmcsAddOrder order)
    {
        Order = order;
    }

    public override bool IsValid()
    {
        ValidationResult = new AddWhmcsOrderCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
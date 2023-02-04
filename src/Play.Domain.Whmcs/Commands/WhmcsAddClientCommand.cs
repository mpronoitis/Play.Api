using NetDevPack.Messaging;
using Play.Domain.Whmcs.Commands.Validations;
using Play.Domain.Whmcs.Models;

namespace Play.Domain.Whmcs.Commands;

public class WhmcsAddClientCommand : Command
{
    public WhmcsAddClient Client { get; protected set; } = null!;
}

public class AddWhmcsClientCommand : WhmcsAddClientCommand
{
    public AddWhmcsClientCommand(WhmcsAddClient client)
    {
        Client = client;
    }

    public override bool IsValid()
    {
        ValidationResult = new AddWhmcsClientCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
using NetDevPack.Messaging;
using Play.Domain.Epp.Commands.Validations;
using Play.Epp.Connector.Models.Contacts;

namespace Play.Domain.Epp.Commands;

public class EppContactCommand : Command
{
    public EPPContact EPPContact { get; protected set; } = null!;
}

public class RegisterEppContactCommand : EppContactCommand
{
    public RegisterEppContactCommand(EPPContact eppContact)
    {
        EPPContact = eppContact;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterEppContactCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateEppContactCommand : EppContactCommand
{
    public UpdateEppContactCommand(EPPContact eppContact)
    {
        EPPContact = eppContact;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateEppContactCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
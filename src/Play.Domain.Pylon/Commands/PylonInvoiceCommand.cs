using NetDevPack.Messaging;
using Play.Domain.Pylon.Commands.Validations;
using Play.Domain.Pylon.Models;

namespace Play.Domain.Pylon.Commands;

public class PylonInvoiceCommand : Command
{
    public Guid Id { get; protected set; }

    public PylonInvoice PylonInvoice { get; protected set; } = null!;
}

public class RegisterPylonInvoiceCommand : PylonInvoiceCommand
{
    public RegisterPylonInvoiceCommand(PylonInvoice pylonInvoice)
    {
        PylonInvoice = pylonInvoice;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterPylonInvoiceCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemovePylonInvoiceCommand : PylonInvoiceCommand
{
    public RemovePylonInvoiceCommand(Guid id)
    {
        Id = id;
    }

    public override bool IsValid()
    {
        ValidationResult = new RemovePylonInvoiceCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
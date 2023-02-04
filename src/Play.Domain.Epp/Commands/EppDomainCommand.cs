using NetDevPack.Messaging;
using Play.Domain.Epp.Commands.Validations;
using Play.Domain.Epp.Models;

namespace Play.Domain.Epp.Commands;

public class EppDomainCommand : Command
{
    public EppRegisterDomainModel RegisterDomainModel { get; protected init; } = null!;
    public EppTransferDomainModel TransferDomainModel { get; protected init; } = null!;
    public EppRenewDomainModel RenewDomainModel { get; protected init; } = null!;
}

public class RegisterEppDomainCommand : EppDomainCommand
{
    public RegisterEppDomainCommand(EppRegisterDomainModel registerDomainModel)
    {
        RegisterDomainModel = registerDomainModel;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterEppDomainCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class TransferEppDomainCommand : EppDomainCommand
{
    public TransferEppDomainCommand(EppTransferDomainModel transferDomainModel)
    {
        TransferDomainModel = transferDomainModel;
    }

    public override bool IsValid()
    {
        ValidationResult = new TransferEppDomainCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RenewEppDomainCommand : EppDomainCommand
{
    public RenewEppDomainCommand(EppRenewDomainModel renewDomainModel)
    {
        RenewDomainModel = renewDomainModel;
    }

    public override bool IsValid()
    {
        ValidationResult = new RenewEppDomainCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
using System;
using NetDevPack.Messaging;
using Play.Domain.Edi.Commands.Validations;
using Play.Domain.Edi.Models;

namespace Play.Domain.Edi.Commands;

public class EdiCreditCommand : Command
{
    public Guid Id { get; set; }
    public EdiCredit Credit { get; set; }
}

public class RegisterEdiCreditCommand : EdiCreditCommand
{
    public RegisterEdiCreditCommand(EdiCredit credit)
    {
        Credit = credit;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterNewEdiCreditCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateEdiCreditCommand : EdiCreditCommand
{
    public UpdateEdiCreditCommand(EdiCredit credit)
    {
        Credit = credit;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateEdiCreditCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemoveEdiCreditCommand : EdiCreditCommand
{
    public RemoveEdiCreditCommand(Guid id)
    {
        Id = id;
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveEdiCreditCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
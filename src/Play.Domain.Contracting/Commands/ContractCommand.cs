using NetDevPack.Messaging;
using Play.Domain.Contracting.Commands.Validation;
using Play.Domain.Contracting.Models;

namespace Play.Domain.Contracting.Commands;

public class ContractCommand : Command
{
    public Contract Contract { get; init; } = null!;
    public Guid Id { get; init; }
}

public class RegisterContractCommand : ContractCommand
{
    public RegisterContractCommand(Contract contract)
    {
        Contract = contract;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterNewContractCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateContractCommand : ContractCommand
{
    public UpdateContractCommand(Contract contract)
    {
        Contract = contract;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateContractCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemoveContractCommand : ContractCommand
{
    public RemoveContractCommand(Guid id)
    {
        Id = id;
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveContractCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
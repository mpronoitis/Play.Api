using System;
using NetDevPack.Messaging;
using Play.Domain.Edi.Commands.Validations;

namespace Play.Domain.Edi.Commands;

public class EdiOrganizationCommand : Command
{
    public Guid Id { get; protected set; }
    public string Name { get; protected set; }
    public string Email { get; protected set; }
}

public class RegisterEdiOrganizationCommand : EdiOrganizationCommand
{
    public RegisterEdiOrganizationCommand(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterEdiOrganizationCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateEdiOrganizationCommand : EdiOrganizationCommand
{
    public UpdateEdiOrganizationCommand(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateEdiOrganizationCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemoveEdiOrganizationCommand : EdiOrganizationCommand
{
    public RemoveEdiOrganizationCommand(Guid id)
    {
        Id = id;
        AggregateId = id;
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveEdiOrganizationCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
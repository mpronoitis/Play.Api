using System;
using NetDevPack.Messaging;
using Play.Domain.Edi.Commands.Validations;

namespace Play.Domain.Edi.Commands;

public class EdiVariableCommand : Command
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Placeholder { get; set; }
}

public class RegisterEdiVariableCommand : EdiVariableCommand
{
    public RegisterEdiVariableCommand(Guid Id, string Title, string Description, string Placeholder)
    {
        this.Id = Id;
        this.Title = Title;
        this.Description = Description;
        this.Placeholder = Placeholder;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterEdiVariableValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateEdiVariableCommand : EdiVariableCommand
{
    public UpdateEdiVariableCommand(Guid Id, string Title, string Description, string Placeholder)
    {
        this.Id = Id;
        this.Title = Title;
        this.Description = Description;
        this.Placeholder = Placeholder;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateEdiVariableValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemoveEdiVariableCommand : EdiVariableCommand
{
    public RemoveEdiVariableCommand(Guid Id)
    {
        this.Id = Id;
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveEdiVariableValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
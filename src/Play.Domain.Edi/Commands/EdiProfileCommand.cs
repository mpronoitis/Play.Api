using System;
using NetDevPack.Messaging;
using Play.Domain.Edi.Commands.Validations;

namespace Play.Domain.Edi.Commands;

public class EdiProfileCommand : Command
{
    public Guid Id { get; protected set; }

    //the customer id that is related to this profile
    public Guid Customer_Id { get; protected set; }

    //the model id that is related to this profile
    public Guid Model_Id { get; protected set; }

    //the title of the profile
    public string Title { get; protected set; }

    //the payload of the profile
    public string Payload { get; protected set; }

    //the enabled flag of the profile
    public bool Enabled { get; protected set; }
}

public class RegisterEdiProfileCommand : EdiProfileCommand
{
    public RegisterEdiProfileCommand(Guid customer_Id, Guid model_Id, string title, string payload, bool enabled)
    {
        Customer_Id = customer_Id;
        Model_Id = model_Id;
        Title = title;
        Payload = payload;
        Enabled = enabled;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterEdiProfileCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateEdiProfileCommand : EdiProfileCommand
{
    public UpdateEdiProfileCommand(Guid id, Guid customer_Id, Guid model_Id, string title, string payload, bool enabled)
    {
        Id = id;
        Customer_Id = customer_Id;
        Model_Id = model_Id;
        Title = title;
        Payload = payload;
        Enabled = enabled;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateEdiProfileCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemoveEdiProfileCommand : EdiProfileCommand
{
    public RemoveEdiProfileCommand(Guid id)
    {
        Id = id;
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveEdiProfileCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
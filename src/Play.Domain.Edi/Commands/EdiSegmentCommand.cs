using System;
using NetDevPack.Messaging;
using Play.Domain.Edi.Commands.Validations;

namespace Play.Domain.Edi.Commands;

public class EdiSegmentCommand : Command
{
    public Guid Id { get; set; }

    //the model id this segment belongs to
    public Guid Model_Id { get; set; }

    //the segment title
    public string Title { get; set; }

    //the segment description
    public string Description { get; set; }
}

public class RegisterEdiSegmentCommand : EdiSegmentCommand
{
    public RegisterEdiSegmentCommand(Guid id, Guid model_id, string title, string description)
    {
        Id = id;
        Model_Id = model_id;
        Title = title;
        Description = description;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterEdiSegmentCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateEdiSegmentCommand : EdiSegmentCommand
{
    public UpdateEdiSegmentCommand(Guid id, Guid model_id, string title, string description)
    {
        Id = id;
        Model_Id = model_id;
        Title = title;
        Description = description;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateEdiSegmentCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemoveEdiSegmentCommand : EdiSegmentCommand
{
    public RemoveEdiSegmentCommand(Guid id)
    {
        Id = id;
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveEdiSegmentCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
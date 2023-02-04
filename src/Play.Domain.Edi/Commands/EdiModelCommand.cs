using System;
using NetDevPack.Messaging;
using Play.Domain.Edi.Commands.Validations;

namespace Play.Domain.Edi.Commands;

public class EdiModelCommand : Command
{
    public Guid Id { get; protected set; }
    public Guid Org_id { get; protected set; }
    public string Title { get; protected set; }
    public char SegmentTerminator { get; protected set; }
    public char SubElementSeperator { get; protected set; }
    public char ElementSeparator { get; protected set; }
    public bool Enabled { get; protected set; }
}

public class RegisterEdiModelCommand : EdiModelCommand
{
    public RegisterEdiModelCommand(Guid org_id, string title, char segmentTerminator, char subElementSeperator,
        char elementSeparator, bool enabled)
    {
        Org_id = org_id;
        Title = title;
        SegmentTerminator = segmentTerminator;
        SubElementSeperator = subElementSeperator;
        ElementSeparator = elementSeparator;
        Enabled = enabled;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterEdiModelCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateEdiModelCommand : EdiModelCommand
{
    public UpdateEdiModelCommand(Guid id, Guid org_id, string title, char segmentTerminator, char subElementSeperator,
        char elementSeparator, bool enabled)
    {
        Id = id;
        Org_id = org_id;
        Title = title;
        SegmentTerminator = segmentTerminator;
        SubElementSeperator = subElementSeperator;
        ElementSeparator = elementSeparator;
        Enabled = enabled;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateEdiModelCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemoveEdiModelCommand : EdiModelCommand
{
    public RemoveEdiModelCommand(Guid id)
    {
        Id = id;
        AggregateId = id;
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveEdiModelCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
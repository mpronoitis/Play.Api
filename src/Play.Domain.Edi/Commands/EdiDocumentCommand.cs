using System;
using NetDevPack.Messaging;
using Play.Domain.Edi.Commands.Validations;

namespace Play.Domain.Edi.Commands;

public class EdiDocumentCommand : Command
{
    public Guid Id { get; set; }
    public Guid Customer_Id { get; set; }
    public string Title { get; set; }
    public string EdiPayload { get; set; }
    public string DocumentPayload { get; set; }
    public string Hedentid { get; set; }
    public bool IsProcessed { get; set; }
    public bool IsSent { get; set; }
}

public class RegisterEdiDocumentCommand : EdiDocumentCommand
{
    public RegisterEdiDocumentCommand(Guid id, Guid customer_Id, string title, string ediPayload,
        string documentPayload, string hedentid, bool isProcessed, bool isSent)
    {
        Id = id;
        Customer_Id = customer_Id;
        Title = title;
        EdiPayload = ediPayload;
        DocumentPayload = documentPayload;
        Hedentid = hedentid;
        IsProcessed = isProcessed;
        IsSent = isSent;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterEdiDocumentValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateEdiDocumentCommand : EdiDocumentCommand
{
    public UpdateEdiDocumentCommand(Guid id, Guid customer_Id, string title, string ediPayload,
        string documentPayload, string hedentid, bool isProcessed, bool isSent)
    {
        Id = id;
        Customer_Id = customer_Id;
        Title = title;
        EdiPayload = ediPayload;
        DocumentPayload = documentPayload;
        Hedentid = hedentid;
        IsProcessed = isProcessed;
        IsSent = isSent;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateEdiDocumentValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemoveEdiDocumentCommand : EdiDocumentCommand
{
    public RemoveEdiDocumentCommand(Guid id)
    {
        Id = id;
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveEdiDocumentValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class ReceivedEdiDocumentCommand : EdiDocumentCommand
{
    public ReceivedEdiDocumentCommand(Guid id, Guid customer_Id, string title, string ediPayload,
        string documentPayload, string hedentid, bool isProcessed, bool isSent)
    {
        Id = id;
        Customer_Id = customer_Id;
        Title = title;
        EdiPayload = ediPayload;
        DocumentPayload = documentPayload;
        Hedentid = hedentid;
        IsProcessed = isProcessed;
        IsSent = isSent;
    }

    public override bool IsValid()
    {
        ValidationResult = new ReceivedEdiDocumentValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
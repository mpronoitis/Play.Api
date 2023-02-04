using System;
using NetDevPack.Domain;

namespace Play.Domain.Edi.Models;

public class EdiDocument : Entity, IAggregateRoot
{
    public EdiDocument(Guid id, Guid customer_Id, string title, string ediPayload, string documentPayload,
        string hedentid, bool isProcessed, bool isSent, DateTime created_At)
    {
        Id = id;
        Customer_Id = customer_Id;
        Title = title;
        EdiPayload = ediPayload;
        DocumentPayload = documentPayload;
        Hedentid = hedentid;
        IsProcessed = isProcessed;
        IsSent = isSent;
        Created_At = created_At;
    }

    // Empty constructor for EF
    public EdiDocument()
    {
    }

    public Guid Customer_Id { get; set; }
    public string Title { get; set; }
    public string EdiPayload { get; set; }
    public string DocumentPayload { get; set; }
    public string Hedentid { get; set; }
    public bool IsProcessed { get; set; }
    public bool IsSent { get; set; }
    public DateTime Created_At { get; set; }
}
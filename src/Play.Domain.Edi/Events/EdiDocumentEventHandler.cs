using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetDevPack.Messaging;
using Play.Domain.Edi.Interfaces;

namespace Play.Domain.Edi.Events;

public class EdiDocumentEventHandler :
    INotificationHandler<EdiDocumentRegisteredEvent>,
    INotificationHandler<EdiDocumentUpdatedEvent>,
    INotificationHandler<EdiDocumentRemovedEvent>,
    INotificationHandler<EdiDocumentReceivedEvent>
{
    private readonly IEdiCreditRepository _ediCreditRepository;

    public EdiDocumentEventHandler(IEdiCreditRepository ediCreditRepository)
    {
        _ediCreditRepository = ediCreditRepository;
    }

    public async Task Handle(EdiDocumentReceivedEvent notification, CancellationToken cancellationToken)
    {
        await _ediCreditRepository.DecrementCreditAsync(notification.Customer_Id, 1);
    }

    public Task Handle(EdiDocumentRegisteredEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EdiDocumentRemovedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EdiDocumentUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public class EdiDocumentRegisteredEvent : Event
{
    public EdiDocumentRegisteredEvent(Guid id, Guid customer_Id, string title, string ediPayload,
        string documentPayload, string hedentid, bool isProcessed, DateTime created_At)
    {
        Id = id;
        Customer_Id = customer_Id;
        Title = title;
        EdiPayload = ediPayload;
        DocumentPayload = documentPayload;
        Hedentid = hedentid;
        IsProcessed = isProcessed;
        Created_At = created_At;
    }

    public Guid Id { get; set; }
    public Guid Customer_Id { get; set; }
    public string Title { get; set; }
    public string EdiPayload { get; set; }
    public string DocumentPayload { get; set; }
    public string Hedentid { get; set; }
    public bool IsProcessed { get; set; }
    public DateTime Created_At { get; set; }
}

public class EdiDocumentUpdatedEvent : Event
{
    public EdiDocumentUpdatedEvent(Guid id, Guid customer_Id, string title, string ediPayload, string documentPayload,
        string hedentid, bool isProcessed)
    {
        Id = id;
        Customer_Id = customer_Id;
        Title = title;
        EdiPayload = ediPayload;
        DocumentPayload = documentPayload;
        Hedentid = hedentid;
        IsProcessed = isProcessed;
    }

    public Guid Id { get; set; }
    public Guid Customer_Id { get; set; }
    public string Title { get; set; }
    public string EdiPayload { get; set; }
    public string DocumentPayload { get; set; }
    public string Hedentid { get; set; }
    public bool IsProcessed { get; set; }
}

public class EdiDocumentRemovedEvent : Event
{
    public EdiDocumentRemovedEvent(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class EdiDocumentReceivedEvent : Event
{
    public EdiDocumentReceivedEvent(Guid id, Guid customer_Id, string title, string ediPayload, string documentPayload,
        string hedentid, bool isProcessed, DateTime created_At)
    {
        Id = id;
        Customer_Id = customer_Id;
        Title = title;
        EdiPayload = ediPayload;
        DocumentPayload = documentPayload;
        Hedentid = hedentid;
        IsProcessed = isProcessed;
        Created_At = created_At;
    }

    public Guid Id { get; set; }
    public Guid Customer_Id { get; set; }
    public string Title { get; set; }
    public string EdiPayload { get; set; }
    public string DocumentPayload { get; set; }
    public string Hedentid { get; set; }
    public bool IsProcessed { get; set; }
    public DateTime Created_At { get; set; }
}
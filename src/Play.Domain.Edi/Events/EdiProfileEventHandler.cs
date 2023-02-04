using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetDevPack.Messaging;

namespace Play.Domain.Edi.Events;

public class EdiProfileEventHandler :
    INotificationHandler<EdiProfileRegisteredEvent>,
    INotificationHandler<EdiProfileRemovedEvent>,
    INotificationHandler<EdiProfileUpdatedEvent>
{
    public Task Handle(EdiProfileRegisteredEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EdiProfileRemovedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EdiProfileUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public class EdiProfileRegisteredEvent : Event
{
    public EdiProfileRegisteredEvent(Guid id, Guid customer_Id, Guid model_Id, string title, string payload,
        bool enabled)
    {
        Id = id;
        Customer_Id = customer_Id;
        Model_Id = model_Id;
        Title = title;
        Payload = payload;
        Enabled = enabled;
    }

    public Guid Id { get; set; }

    //the customer id that is related to this profile
    public Guid Customer_Id { get; set; }

    //the model id that is related to this profile
    public Guid Model_Id { get; set; }

    //the title of the profile
    public string Title { get; set; }

    //the payload of the profile
    public string Payload { get; set; }

    //the enabled flag of the profile
    public bool Enabled { get; set; }
}

public class EdiProfileRemovedEvent : Event
{
    public EdiProfileRemovedEvent(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class EdiProfileUpdatedEvent : Event
{
    public EdiProfileUpdatedEvent(Guid id, Guid customer_Id, Guid model_Id, string title, string payload, bool enabled)
    {
        Id = id;
        Customer_Id = customer_Id;
        Model_Id = model_Id;
        Title = title;
        Payload = payload;
        Enabled = enabled;
    }

    public Guid Id { get; set; }
    public Guid Customer_Id { get; set; }
    public Guid Model_Id { get; set; }
    public string Title { get; set; }
    public string Payload { get; set; }
    public bool Enabled { get; set; }
}
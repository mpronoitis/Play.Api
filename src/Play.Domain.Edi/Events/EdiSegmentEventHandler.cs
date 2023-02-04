using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetDevPack.Messaging;

namespace Play.Domain.Edi.Events;

public class EdiSegmentEventHandler :
    INotificationHandler<EdiSegmentRegisteredEvent>,
    INotificationHandler<EdiSegmentRemovedEvent>,
    INotificationHandler<EdiSegmentUpdatedEvent>
{
    public Task Handle(EdiSegmentRegisteredEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EdiSegmentRemovedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EdiSegmentUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public class EdiSegmentRegisteredEvent : Event
{
    public EdiSegmentRegisteredEvent(Guid id, Guid model_Id, string title, string description)
    {
        Id = id;
        Model_Id = model_Id;
        Title = title;
        Description = description;
    }

    public Guid Id { get; set; }

    //the model id this segment belongs to
    public Guid Model_Id { get; set; }

    //the segment title
    public string Title { get; set; }

    //the segment description
    public string Description { get; set; }
}

public class EdiSegmentUpdatedEvent : Event
{
    public EdiSegmentUpdatedEvent(Guid id, Guid model_Id, string title, string description)
    {
        Id = id;
        Model_Id = model_Id;
        Title = title;
        Description = description;
    }

    public Guid Id { get; set; }

    //the model id this segment belongs to
    public Guid Model_Id { get; set; }

    //the segment title
    public string Title { get; set; }

    //the segment description
    public string Description { get; set; }
}

public class EdiSegmentRemovedEvent : Event
{
    public EdiSegmentRemovedEvent(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
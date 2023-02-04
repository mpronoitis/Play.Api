using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetDevPack.Messaging;

namespace Play.Domain.Edi.Events;

public class EdiVariableEventHandler :
    INotificationHandler<EdiVariableRegisteredEvent>,
    INotificationHandler<EdiVariableUpdatedEvent>,
    INotificationHandler<EdiVariableRemovedEvent>
{
    public Task Handle(EdiVariableRegisteredEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EdiVariableRemovedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EdiVariableUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public class EdiVariableRegisteredEvent : Event
{
    public EdiVariableRegisteredEvent(Guid id, string title, string description, string placeholder)
    {
        Id = id;
        Title = title;
        Description = description;
        Placeholder = placeholder;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Placeholder { get; set; }
}

public class EdiVariableUpdatedEvent : Event
{
    public EdiVariableUpdatedEvent(Guid id, string title, string description, string placeholder)
    {
        Id = id;
        Title = title;
        Description = description;
        Placeholder = placeholder;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Placeholder { get; set; }
}

public class EdiVariableRemovedEvent : Event
{
    public EdiVariableRemovedEvent(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
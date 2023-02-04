using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetDevPack.Messaging;

namespace Play.Domain.Edi.Events;

public class EdiOrganizationEventHandler : INotificationHandler<EdiOrganizationRegisteredEvent>,
    INotificationHandler<EdiOrganizationUpdatedEvent>,
    INotificationHandler<EdiOrganizationRemovedEvent>
{
    public Task Handle(EdiOrganizationRegisteredEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EdiOrganizationRemovedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EdiOrganizationUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public class EdiOrganizationRegisteredEvent : Event
{
    /// <summary>
    ///     Event constructor
    /// </summary>
    public EdiOrganizationRegisteredEvent(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
        AggregateId = id;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class EdiOrganizationRemovedEvent : Event
{
    public EdiOrganizationRemovedEvent(Guid id)
    {
        Id = id;
        AggregateId = id;
    }

    public Guid Id { get; set; }
}

public class EdiOrganizationUpdatedEvent : Event
{
    public EdiOrganizationUpdatedEvent(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
        AggregateId = id;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Email { get; }
}
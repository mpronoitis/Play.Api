using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetDevPack.Messaging;

namespace Play.Domain.Edi.Events;

public class EdiModelEventHandler : INotificationHandler<EdiModelRegisteredEvent>,
    INotificationHandler<EdiModelUpdatedEvent>,
    INotificationHandler<EdiModelRemovedEvent>
{
    public Task Handle(EdiModelRegisteredEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EdiModelRemovedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EdiModelUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public class EdiModelRegisteredEvent : Event
{
    //event constructor
    public EdiModelRegisteredEvent(Guid id, Guid org_id, string title, char segment_terminator,
        char sub_element_separator, char element_separator, bool enabled)
    {
        Id = id;
        AggregateId = id;
        Org_Id = org_id;
        Title = title;
        SegmentTerminator = segment_terminator;
        SubElementSeparator = sub_element_separator;
        ElementSeparator = element_separator;
        Enabled = enabled;
    }

    public Guid Id { get; set; }

    /// <summary>
    ///     Id of the organization that this model belongs to
    /// </summary>
    public Guid Org_Id { get; set; }

    /// <summary>
    ///     Title of the model
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Segment terminator for given model
    /// </summary>
    public char SegmentTerminator { get; set; }

    /// <summary>
    ///     Sub element separator for given model
    /// </summary>
    public char SubElementSeparator { get; set; }

    /// <summary>
    ///     Element separator for given model
    /// </summary>
    public char ElementSeparator { get; set; }

    /// <summary>
    ///     Enabled flag
    /// </summary>
    public bool Enabled { get; set; }
}

public class EdiModelRemovedEvent : Event
{
    public EdiModelRemovedEvent(Guid id)
    {
        Id = id;
        AggregateId = id;
    }

    public Guid Id { get; set; }
}

public class EdiModelUpdatedEvent : Event
{
    public EdiModelUpdatedEvent(Guid id, Guid org_id, string title, char segment_terminator, char sub_element_separator,
        char element_separator, bool enabled)
    {
        Id = id;
        AggregateId = id;
        Org_Id = org_id;
        Title = title;
        SegmentTerminator = segment_terminator;
        SubElementSeparator = sub_element_separator;
        ElementSeparator = element_separator;
        Enabled = enabled;
    }

    public Guid Id { get; set; }
    public Guid Org_Id { get; set; }
    public string Title { get; set; }
    public char SegmentTerminator { get; set; }
    public char SubElementSeparator { get; set; }
    public char ElementSeparator { get; set; }
    public bool Enabled { get; set; }
}
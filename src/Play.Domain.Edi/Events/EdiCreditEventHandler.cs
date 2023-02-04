using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetDevPack.Messaging;

namespace Play.Domain.Edi.Events;

public class EdiCreditEventHandler : INotificationHandler<EdiCreditRegisteredEvent>,INotificationHandler<EdiCreditUpdatedEvent>,INotificationHandler<EdiCreditRemovedEvent>
{
    public Task Handle(EdiCreditRegisteredEvent notification, CancellationToken cancellationToken)
    {
        // here we want to add to database the event 
      return Task.CompletedTask;
    }

    public Task Handle(EdiCreditUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(EdiCreditRemovedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public class EdiCreditRegisteredEvent : Event
{
    public EdiCreditRegisteredEvent(Guid id, Guid customerId, int amount, DateTime createdAt,DateTime updatedAt)
    {
        Id = id;
        CustomerId = customerId;
        Amount = amount;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        AggregateId = id;

    }
    
    /// <summary>
    /// The Id of the credit
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    ///     The customer identifier for the credit
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    ///     The credit amount
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    ///     Updated at
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    ///     Created at
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
}

public class EdiCreditUpdatedEvent : Event
{
    public EdiCreditUpdatedEvent(Guid id, Guid customerId, int amount, DateTime createdAt,DateTime updatedAt)
    {
        Id = id;
        CustomerId = customerId;
        Amount = amount;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        AggregateId = id;

    }
    
    /// <summary>
    /// The Id of the credit
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    ///     The customer identifier for the credit
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    ///     The credit amount
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    ///     Updated at
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    ///     Created at
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
}

public class EdiCreditRemovedEvent : Event
{
    public EdiCreditRemovedEvent(Guid id, Guid customerId, int amount, DateTime createdAt,DateTime updatedAt)
    {
        Id = id;
        CustomerId = customerId;
        Amount = amount;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        AggregateId = id;

    }
    
    /// <summary>
    /// The Id of the credit
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    ///     The customer identifier for the credit
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    ///     The credit amount
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    ///     Updated at
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    ///     Created at
    /// </summary>
    public DateTime CreatedAt { get; set; }
 
}

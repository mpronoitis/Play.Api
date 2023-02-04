using System;
using NetDevPack.Domain;

namespace Play.Domain.Edi.Models;

public class EdiCredit : Entity, IAggregateRoot
{
    public EdiCredit(Guid id, Guid customerId, int amount, DateTime createdAt)
    {
        Id = id;
        CustomerId = customerId;
        Amount = amount;
        CreatedAt = createdAt;
        UpdatedAt = DateTime.UtcNow;
    }

    public EdiCredit()
    {
    }

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
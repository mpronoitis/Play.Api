using System;
using NetDevPack.Domain;

namespace Play.Domain.Edi.Models;

public class EdiProfile : Entity, IAggregateRoot
{
    public EdiProfile(Guid id, Guid customerId, Guid modelId, string title, string payload, bool enabled)
    {
        Id = id;
        Customer_Id = customerId;
        Model_Id = modelId;
        Title = title;
        Payload = payload;
        Enabled = enabled;
    }

    //empty constructor for EF
    public EdiProfile()
    {
    }

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
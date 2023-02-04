using System;
using NetDevPack.Domain;

namespace Play.Domain.Edi.Models;

public class EdiSegment : Entity, IAggregateRoot
{
    public EdiSegment(Guid id, Guid model_Id, string title, string description)
    {
        Id = id;
        Model_Id = model_Id;
        Title = title;
        Description = description;
    }

    //empty constructor for EF
    public EdiSegment()
    {
    }

    //the model id this segment belongs to
    public Guid Model_Id { get; set; }

    //the segment title
    public string Title { get; set; }

    //the segment description
    public string Description { get; set; }
}
using NetDevPack.Domain;
using NetDevPack.Messaging;

namespace Play.Domain.EventSourcing.Models;

public class StoredEvent : Event
{
    public StoredEvent(Event theEvent,string data)
    {
        Id = Guid.NewGuid();
        AggregateId = theEvent.AggregateId;
        Data = data;
        MessageType = theEvent.MessageType;
        Timestamp = theEvent.Timestamp;
    }
    
    //empty constructor for EF
    public StoredEvent() { }
    
    public Guid Id { get; set; }
    //serialized data of the event
    public string Data { get;  set; }
    
    //aggregate id of the event
    public Guid AggregateId { get;  set; }
    
    //type of the event
    public string MessageType { get;  set; }
    
    //timestamp of the event
    public DateTime Timestamp { get;  set; }
    
    
}
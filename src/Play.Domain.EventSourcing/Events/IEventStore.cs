using NetDevPack.Messaging;

namespace Play.Domain.EventSourcing.Events;

public interface IEventStore
{
    //save Event 
    void Save<T>(T theEvent) where T : Event;
}
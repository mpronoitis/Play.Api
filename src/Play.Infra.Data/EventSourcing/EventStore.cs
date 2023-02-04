using Newtonsoft.Json;
using Play.Domain.Edi.Events;
using Play.Domain.EventSourcing.Events;
using Play.Domain.EventSourcing.Interfaces;
using Play.Domain.EventSourcing.Models;

namespace Play.Infra.Data.EventSourcing;

public class EventStore : IEventStore
{
    protected readonly IEventStoreRepository _eventStoreRepository;
    
    public EventStore(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }
    public void Save<T>(T theEvent) where T : Event
    {
        
        if (theEvent.GetType() == typeof(EdiDocumentReceivedEvent))
        {
            var ediDocumentReceivedEvent = theEvent as EdiDocumentReceivedEvent;
            ediDocumentReceivedEvent.DocumentPayload = string.Empty;
            ediDocumentReceivedEvent.EdiPayload = string.Empty;
            
        }
        
        var serializedData = JsonConvert.SerializeObject(theEvent);
        
        var storedEvent = new StoredEvent(theEvent,serializedData);
        _eventStoreRepository.Store(storedEvent);

    }
}
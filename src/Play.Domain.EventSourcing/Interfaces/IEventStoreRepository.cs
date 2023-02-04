using Play.Domain.EventSourcing.Models;

namespace Play.Domain.EventSourcing.Interfaces;

public interface IEventStoreRepository
{
    //method to add to database the event
    void Store(StoredEvent theEvent);
    //method to get all events from database with pagination
   Task<IEnumerable<StoredEvent>> GetAllAsync(int page = 1, int pageSize = 10);
   
   //method to get events with specific messageType from database with pagination
    Task<IEnumerable<StoredEvent>> GetByMessageTypeAsync(string messageType, int page = 1, int pageSize = 10);
    
    //method to get events By CustomerId from database with pagination
    Task<IEnumerable<StoredEvent>> GetByCustomerIdAsync(Guid customerId, int page = 1, int pageSize = 10);
    
    //get total count of events
    Task<int> GetCountAsync();
    
    //get total count of events with specific messageType
    
    Task<int> GetCountByMessageTypeAsync(string messageType);
    
    //get total count of events with specific CustomerId
    
    Task<int> GetCountByCustomerIdAsync(Guid customerId);
    
    
   
}
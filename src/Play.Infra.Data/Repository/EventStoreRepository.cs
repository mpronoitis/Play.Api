using Play.Domain.EventSourcing.Interfaces;
using Play.Domain.EventSourcing.Models;

namespace Play.Infra.Data.Repository;

public class EventStoreRepository : IEventStoreRepository
{
    protected readonly PlayEventStoreContext Db;
    protected readonly DbSet<StoredEvent> DbSet;
    
    public EventStoreRepository(PlayEventStoreContext context)
    {
        Db = context;
        DbSet = Db.Set<StoredEvent>();
        
    }
    public void Store(StoredEvent theEvent)
    {
        Db.StoredEvent.Add(theEvent);
        //context save changes
        Db.SaveChanges();

    }
    
    //Get all Events with pagination
    public async Task<IEnumerable<StoredEvent>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        var events = await DbSet.AsNoTracking().OrderByDescending(c => c.Timestamp).Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
        
        return events;
    }
    
    //Get Events with specific MessageType
    public async Task<IEnumerable<StoredEvent>> GetByMessageTypeAsync(string messageType, int page = 1, int pageSize = 10)
    {
        var events = await DbSet.AsNoTracking().Where(c => c.MessageType == messageType).OrderByDescending(c => c.Timestamp).Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
        
        return events;
    }
    
    //Get Events By CustomerId that is inside the serialized data with pagination
    public async Task<IEnumerable<StoredEvent>> GetByCustomerIdAsync(Guid customerId, int page = 1, int pageSize = 10)
    {
        var events = await DbSet.AsNoTracking().Where(c => c.Data.Contains(customerId.ToString())).OrderByDescending(c => c.Timestamp).Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
        
        return events;
    }
    
    //Get Total Count Of Events
    public async Task<int> GetCountAsync()
    {
        var count = await DbSet.AsNoTracking().CountAsync();
        
        return count;
    }
    
    //Get Total Count by customerId
    public async Task<int> GetCountByCustomerIdAsync(Guid customerId)
    {
        var count = await DbSet.AsNoTracking().Where(c => c.Data.Contains(customerId.ToString())).CountAsync();
        
        return count;
    }
    
    //Get Total Count by MessageType
    public async Task<int> GetCountByMessageTypeAsync(string messageType)
    {
        var count = await DbSet.AsNoTracking().Where(c => c.MessageType == messageType).CountAsync();
        
        return count;
    }
}
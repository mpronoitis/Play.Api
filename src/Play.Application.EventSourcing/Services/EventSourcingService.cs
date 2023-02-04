using NetDevPack.Mediator;
using Play.Application.EventSourcing.Interfaces;
using Play.Domain.EventSourcing.Interfaces;
using Play.Domain.EventSourcing.Models;

namespace Play.Application.EventSourcing.Services;

public class EventSourcingService : IEventSourcingService
{
    private readonly IMediatorHandler _mediator;
    private readonly IEventStoreRepository _eventStoreRepository;
    
    public EventSourcingService(IMediatorHandler mediator, IEventStoreRepository eventStoreRepository)
    {
        _mediator = mediator;
        _eventStoreRepository = eventStoreRepository;
    }
    
    /// <summary>
    ///     Get All Events from Database with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    
    public async Task<IEnumerable<StoredEvent>> GetAllAsync(int page, int pageSize)
    {
        return await _eventStoreRepository.GetAllAsync(page, pageSize);
    }
    
    /// <summary>
    ///   Get Events with specific messageType from Database with pagination
    /// </summary>
    /// <param name="messageType">Message Type</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>

    public async Task<IEnumerable<StoredEvent>> GetByTypeAsync(string messageType, int page, int pageSize)
    {
       return await _eventStoreRepository.GetByMessageTypeAsync(messageType, page, pageSize);
    }

    public async Task<IEnumerable<StoredEvent>> GetByCustomerIdAsync(Guid customerId, int page, int pageSize)
    {
       return await _eventStoreRepository.GetByCustomerIdAsync(customerId, page, pageSize);
    }

    public Task<int> GetTotalCountAsync()
    {
        return _eventStoreRepository.GetCountAsync();
    }

    public Task<int> GetTotalCountByTypeAsync(string messageType)
    {
        return _eventStoreRepository.GetCountByMessageTypeAsync(messageType);
    }

    public Task<int> GetTotalCountByCustomerIdAsync(Guid customerId)
    {
        return _eventStoreRepository.GetCountByCustomerIdAsync(customerId);
    }
}
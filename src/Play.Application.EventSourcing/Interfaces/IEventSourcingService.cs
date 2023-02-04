using Play.Domain.EventSourcing.Models;

namespace Play.Application.EventSourcing.Interfaces;

public interface IEventSourcingService
{
    /// <summary>
    ///     Get All Events from Database with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    Task<IEnumerable<StoredEvent>> GetAllAsync(int page, int pageSize);
    
    /// <summary>
    ///   Get Events with specific messageType from Database with pagination
    /// </summary>
    /// <param name="messageType">Message Type</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    
    Task<IEnumerable<StoredEvent>> GetByTypeAsync(string messageType, int page, int pageSize);
    
    /// <summary>
    ///   Get Events By Customer Id from Database with pagination
    /// </summary>
    /// <param name="customerId">Customer Id</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    
    Task<IEnumerable<StoredEvent>> GetByCustomerIdAsync(Guid customerId, int page, int pageSize);
    
    /// <summary>
    ///   Get Total Events Count
    /// </summary>
    /// <returns></returns>
    Task<int> GetTotalCountAsync();
    
    /// <summary>
    ///   Get Total Events Count by Message Type
    /// </summary>
    /// <param name="messageType">Message Type</param>
    /// <returns></returns>
    
    Task<int> GetTotalCountByTypeAsync(string messageType);
    
    /// <summary>
    ///   Get Total Events Count by Customer Id
    /// </summary>
    /// <param name="customerId">Customer Id</param>
    /// <returns></returns>
    
    Task<int> GetTotalCountByCustomerIdAsync(Guid customerId);
    
    
}
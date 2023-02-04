using Play.Application.EventSourcing.Interfaces;

namespace Play.Services.Api.Controllers.EventSourcing;

[Route("events")]

public class EventSourcingController : ApiController
{
    private readonly IEventSourcingService _eventSourcingService;
    
    public EventSourcingController(IEventSourcingService eventSourcingService)
    {
        _eventSourcingService = eventSourcingService;
    }
    
    /// <summary>
    ///     Get All Events from Database with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    
    [HttpGet("get-all/{page:int}/{pageSize:int}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetAll(int page, int pageSize)
    {
        var res = await _eventSourcingService.GetAllAsync(page, pageSize);
        return CustomResponse(res);
    }
    
    /// <summary>
    /// Get Events with specific messageType
    /// </summary>
    /// <param name="messageType">Message Type</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    
    [HttpGet("get-all/{messageType}/{page:int}/{pageSize:int}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetAllByMessageType(string messageType, int page, int pageSize)
    {
        var res = await _eventSourcingService.GetByTypeAsync(messageType, page, pageSize);
        return CustomResponse(res);
    }
    
    [HttpGet("get-all-by-customer/{customerId}/{page:int}/{pageSize:int}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetAllByCustomerId(Guid customerId, int page, int pageSize)
    {
        var res = await _eventSourcingService.GetByCustomerIdAsync(customerId, page, pageSize);
        return CustomResponse(res);
    }
    
    /// <summary>
    ///    Get Count of Events
    /// </summary>
    /// <returns></returns>
    
    [HttpGet("count")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetCount()
    {
        var res = await _eventSourcingService.GetTotalCountAsync();
        return CustomResponse(res);
    }
    
    /// <summary>
    ///    Get Count of Events with specific messageType
    /// </summary>
    /// <param name="messageType">Message Type</param>
    /// <returns></returns>
    
    [HttpGet("count-by-type/{messageType}")]
    [Authorize(Roles = "PlayAdmin")]
    
    public async Task<IActionResult> GetCountByMessageType(string messageType)
    {
        var res = await _eventSourcingService.GetTotalCountByTypeAsync(messageType);
        return CustomResponse(res);
    }
    
    /// <summary>
    ///   Get Count of Events with specific customerId
    /// </summary>
    /// <param name="customerId">Customer Id</param>
    /// <returns></returns>
    
    [HttpGet("count-by-customer/{customerId}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetCountByCustomerId(Guid customerId)
    {
        var res = await _eventSourcingService.GetTotalCountByCustomerIdAsync(customerId);
        return CustomResponse(res);
    }

    
}
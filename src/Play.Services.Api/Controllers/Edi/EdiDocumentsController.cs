namespace Play.Services.Api.Controllers.Edi;

[Route("edi/documents")]
public class EdiDocumentsController : ApiController
{
    private readonly IEdiDocumentService _ediDocumentService;

    public EdiDocumentsController(IEdiDocumentService ediDocumentService)
    {
        _ediDocumentService = ediDocumentService;
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("{id:guid}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<EdiDocumentViewModel> GetById(Guid id)
    {
        return await _ediDocumentService.GetByIdAsync(id);
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("all/customerid/{customerId}/{page}/{pageSize}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "customerId,page,pageSize")]
    public async Task<IEnumerable<EdiDocumentViewModel>> GetAll(Guid customerId, int page, int pageSize)
    {
        var docs = await _ediDocumentService.GetAllWithPaginationByCustomerIdAsync(customerId, page, pageSize);
        //replace any edi document payloads and edi payloads with "content too long" if more than 10.000 characters
        foreach (var doc in docs)
        {
            if (doc.EdiPayload.Length > 10000)
            {
                doc.EdiPayload = "Content too long";
            }
            if (doc.DocumentPayload.Length > 10000)
            {
                doc.DocumentPayload = "Content too long";
            }
        }
        
        return docs;
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpGet("all/{page}/{pageSize}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "page,pageSize")]
    public async Task<IEnumerable<EdiDocumentViewModel>> GetAll(int page, int pageSize)
    {
        return await _ediDocumentService.GetAllWithPaginationAsync(page, pageSize);
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("count/customerid/{customerId}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "customerId")]
    public async Task<int> GetCount(Guid customerId)
    {
        return await _ediDocumentService.GetTotalCountByCustomerIdAsync(customerId);
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpPost]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "ediDocument")]
    public async Task<IActionResult> Create([FromBody] EdiDocumentViewModel ediDocument)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _ediDocumentService.Register(ediDocument));
    }

    [HttpPost("receiver")]
    public async Task<IActionResult> CreateReceiver([FromBody] EdiDocumentReceiverViewModel ediDocument)
    {
        if (!ModelState.IsValid) CustomResponse(ModelState);
        var res = await _ediDocumentService.Receive(ediDocument);
        if (res.IsValid) return CustomResponse(new { message = "Document received successfully" });
        return CustomResponse(res);
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpPut]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "ediDocument")]
    public async Task<IActionResult> Update([FromBody] EdiDocumentViewModel ediDocument)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _ediDocumentService.Update(ediDocument));
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpDelete("id:guid")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<IActionResult> Remove(Guid id)
    {
        return CustomResponse(await _ediDocumentService.Remove(id));
    }

    /// <summary>
    ///     Get total count by customer id and date range
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    [Authorize(Roles = "PlayAdmin")]
    [HttpGet("count/customerid/{customerId}/date/{startDate}/{endDate}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "customerId,startDate,endDate")]
    public async Task<IActionResult> GetCountByCustomerIdAndDateRange(Guid customerId, DateTime startDate,
        DateTime endDate)
    {
        return CustomResponse(new
        {
            count = await _ediDocumentService.GetTotalCountByCustomerIdAndDateRangeAsync(customerId, startDate, endDate)
        });
    }

    /// <summary>
    ///     Get total count of documents
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "PlayAdmin")]
    [HttpGet("count")]
    public async Task<IActionResult> GetCount()
    {
        return CustomResponse(new { count = await _ediDocumentService.GetTotalCountAsync() });
    }
    
    //get all with no payloads and pagination and customer id
    [HttpGet("all/customerid/{customerId}/{page}/{pageSize}/nopayload")]
    [Authorize(Roles = "Customer,PlayAdmin")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "customerId,page,pageSize")]
    public async Task<IEnumerable<EdiDocumentViewModel>> GetAllNoPayload(Guid customerId, int page, int pageSize)
    {
        return await _ediDocumentService.GetAllWithNoPayloadsAndPaginationByCustomerIdAsync(customerId, page, pageSize);
    }
    
    //get all with no payloads and pagination
    [HttpGet("all/{page}/{pageSize}/nopayload")]
    [Authorize(Roles = "PlayAdmin")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "page,pageSize")]
    public async Task<IEnumerable<EdiDocumentViewModel>> GetAllNoPayload(int page, int pageSize)
    {
        return await _ediDocumentService.GetAllWithNoPayloadsAndPaginationAsync(page, pageSize);
    }
}
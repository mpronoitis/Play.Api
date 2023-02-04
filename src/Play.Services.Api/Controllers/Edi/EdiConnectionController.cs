namespace Play.Services.Api.Controllers.Edi;

[Route("edi/connections")]
public class EdiConnectionController : ApiController
{
    private readonly IEdiConnectionService _ediConnectionService;

    public EdiConnectionController(IEdiConnectionService ediConnectionService)
    {
        _ediConnectionService = ediConnectionService;
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("id:guid")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<EdiConnectionViewModel> Get(Guid id)
    {
        return await _ediConnectionService.GetById(id);
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("ByCustomerId/id:guid")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<IEnumerable<EdiConnectionViewModel>> GetByCustomerId(Guid id)
    {
        return await _ediConnectionService.GetByCustomerId(id);
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpGet("all/{page:int}/{pageSize:int}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "page,pageSize")]
    public async Task<IEnumerable<EdiConnectionViewModel>> GetAll(int page, int pageSize)
    {
        return await _ediConnectionService.GetAll(page, pageSize);
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("all/byCustomer/{customerId:guid}/{page:int}/{pageSize:int}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "customerId,page,pageSize")]
    public async Task<IEnumerable<EdiConnectionViewModel>> GetAllByCustomer(Guid customerId, int page, int pageSize)
    {
        return await _ediConnectionService.GetAllByCustomerId(customerId, page, pageSize);
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpPost]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "ediConnection")]
    public async Task<IActionResult> Create([FromBody] EdiConnectionViewModel ediConnection)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _ediConnectionService.Add(ediConnection));
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpPut]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "ediConnection")]
    public async Task<IActionResult> Update([FromBody] EdiConnectionViewModel ediConnection)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _ediConnectionService.Update(ediConnection));
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpDelete("{id:guid}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CustomResponse(await _ediConnectionService.Remove(id));
    }

    /// <summary>
    ///     Get total number of EdiConnections
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "PlayAdmin")]
    [HttpGet("count")]
    [RateLimit(PeriodInSec = 10, Limit = 10)]
    public async Task<IActionResult> Count()
    {
        try
        {
            var count = await _ediConnectionService.GetTotalCount();
            return CustomResponse(new { count });
        }
        catch (Exception e)
        {
            AddError(e.Message);
            return CustomResponse();
        }
    }
}
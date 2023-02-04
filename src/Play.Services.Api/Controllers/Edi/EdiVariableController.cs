namespace Play.Services.Api.Controllers.Edi;

[Route("edi/variables")]
public class EdiVariableController : ApiController
{
    private readonly IEdiVariableService _ediVariableService;

    public EdiVariableController(IEdiVariableService ediVariableService)
    {
        _ediVariableService = ediVariableService;
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("id:guid")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<EdiVariableViewModel> Get(Guid id)
    {
        return await _ediVariableService.GetByIdAsync(id);
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("all/{page:int}/{pageSize:int}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "page,pageSize")]
    public async Task<IEnumerable<EdiVariableViewModel>> GetAll(int page, int pageSize)
    {
        return await _ediVariableService.GetAllAsync(page, pageSize);
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpPost]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "ediVariableViewModel")]
    public async Task<IActionResult> Create([FromBody] EdiVariableViewModel ediVariableViewModel)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _ediVariableService.RegisterAsync(ediVariableViewModel));
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpPut]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "ediVariableViewModel")]
    public async Task<IActionResult> Update([FromBody] EdiVariableViewModel ediVariableViewModel)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _ediVariableService.UpdateAsync(ediVariableViewModel));
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpDelete("{id:guid}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<IActionResult> Remove(Guid id)
    {
        return CustomResponse(await _ediVariableService.RemoveAsync(id));
    }

    /// <summary>
    ///     Get total count of Edi Variables
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "PlayAdmin")]
    [HttpGet("count")]
    [RateLimit(PeriodInSec = 10, Limit = 10)]
    public async Task<IActionResult> Count()
    {
        return CustomResponse(new { count = await _ediVariableService.GetTotalCount() });
    }
}
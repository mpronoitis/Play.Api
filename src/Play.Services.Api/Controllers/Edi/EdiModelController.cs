namespace Play.Services.Api.Controllers.Edi;

[Route("edi/models")]
public class EdiModelController : ApiController
{
    private readonly IEdiModelService _ediModelService;

    public EdiModelController(IEdiModelService ediModelService)
    {
        _ediModelService = ediModelService;
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("id:guid")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<EdiModelViewModel> Get(Guid id)
    {
        return await _ediModelService.GetById(id);
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("all/{page:int}/{pageSize:int}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "page,pageSize")]
    public async Task<IEnumerable<EdiModelViewModel>> GetAll(int page, int pageSize)
    {
        return await _ediModelService.GetAll(page, pageSize);
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpPost]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "ediModel")]
    public async Task<IActionResult> Create([FromBody] EdiModelViewModel ediModel)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _ediModelService.Register(ediModel));
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpPut]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "ediModel")]
    public async Task<IActionResult> Update([FromBody] EdiModelViewModel ediModel)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _ediModelService.Update(ediModel));
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpDelete("{id:guid}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<IActionResult> Remove(Guid id)
    {
        return CustomResponse(await _ediModelService.Remove(id));
    }

    /// <summary>
    ///     Get total count of EdiModels
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "PlayAdmin")]
    [HttpGet("count")]
    [RateLimit(PeriodInSec = 10, Limit = 10)]
    public async Task<IActionResult> Count()
    {
        return CustomResponse(new { count = await _ediModelService.GetCount() });
    }
}
namespace Play.Services.Api.Controllers.Edi;

[Route("edi/profiles")]
public class EdiProfileController : ApiController
{
    private readonly IEdiProfileService _ediProfileService;

    public EdiProfileController(IEdiProfileService ediProfileService)
    {
        _ediProfileService = ediProfileService;
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("id:guid")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<EdiProfileViewModel> Get(Guid id)
    {
        return await _ediProfileService.GetById(id);
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("ByModelId/{modelId}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "modelId")]
    public async Task<IEnumerable<EdiProfileViewModel>> GetByModelId(Guid modelId)
    {
        return await _ediProfileService.GetByModelId(modelId);
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpGet("all/{page:int}/{pageSize:int}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "page,pageSize")]
    public async Task<IEnumerable<EdiProfileViewModel>> GetAll(int page, int pageSize)
    {
        return await _ediProfileService.GetAll(page, pageSize);
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("all/byCustomer/{customer_id:guid}/{page:int}/{pageSize:int}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "customer_id,page,pageSize")]
    public async Task<IEnumerable<EdiProfileViewModel>> GetAllByCustomer(Guid customer_id, int page, int pageSize)
    {
        return await _ediProfileService.GetAllByCustomerId(customer_id, page, pageSize);
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpPost]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "ediProfile")]
    public async Task<IActionResult> Create([FromBody] EdiProfileViewModel ediProfile)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _ediProfileService.Register(ediProfile));
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpPut]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "ediProfile")]
    public async Task<IActionResult> Update([FromBody] EdiProfileViewModel ediProfile)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _ediProfileService.Update(ediProfile));
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpDelete("{id:guid}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CustomResponse(await _ediProfileService.Remove(id));
    }

    /// <summary>
    ///     Get total count of Edi Profiles
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "PlayAdmin")]
    [HttpGet("count")]
    [RateLimit(PeriodInSec = 10, Limit = 10)]
    public async Task<IActionResult> Count()
    {
        return CustomResponse(new { count = await _ediProfileService.GetTotalCount() });
    }
}
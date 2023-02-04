namespace Play.Services.Api.Controllers.Edi;

[Route("edi/organization")]
public class EdiOrganizationController : ApiController
{
    private readonly IEdiOrganizationService _ediOrganizationService;

    public EdiOrganizationController(IEdiOrganizationService ediOrganizationService)
    {
        _ediOrganizationService = ediOrganizationService;
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("{id:guid}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<EdiOrganizationViewModel> Get(Guid id)
    {
        return await _ediOrganizationService.GetById(id);
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("all/{page:int}/{pageSize:int}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "page,pageSize")]
    public async Task<IEnumerable<EdiOrganizationViewModel>> GetAll(int page, int pageSize)
    {
        return await _ediOrganizationService.GetAll(page, pageSize);
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpPost("")]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "ediOrganization")]
    public async Task<IActionResult> Create([FromBody] EdiOrganizationViewModel ediOrganization)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _ediOrganizationService.Register(ediOrganization));
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpPut("")]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "ediOrganization")]
    public async Task<IActionResult> Update([FromBody] EdiOrganizationViewModel ediOrganization)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _ediOrganizationService.Update(ediOrganization));
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpDelete("{id:guid}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CustomResponse(await _ediOrganizationService.Remove(id));
    }
}
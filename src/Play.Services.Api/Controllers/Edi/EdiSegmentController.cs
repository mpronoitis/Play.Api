namespace Play.Services.Api.Controllers.Edi;

[Route("edi/segments")]
public class EdiSegmentController : ApiController
{
    private readonly IEdiSegmentService _ediSegmentService;

    public EdiSegmentController(IEdiSegmentService ediSegmentService)
    {
        _ediSegmentService = ediSegmentService;
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("id:guid")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<EdiSegmentViewModel> Get(Guid id)
    {
        return await _ediSegmentService.GetById(id);
    }

    [Authorize(Roles = "Customer,PlayAdmin")]
    [HttpGet("all/{page:int}/{pageSize:int}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "page,pageSize")]
    public async Task<IEnumerable<EdiSegmentViewModel>> GetAll(int page, int pageSize)
    {
        return await _ediSegmentService.GetAllAsync(page, pageSize);
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpPost]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "ediSegment")]
    public async Task<IActionResult> Create([FromBody] EdiSegmentViewModel ediSegment)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _ediSegmentService.Register(ediSegment));
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpPut]
    [RateLimit(PeriodInSec = 10, Limit = 10, BodyParams = "ediSegment")]
    public async Task<IActionResult> Update([FromBody] EdiSegmentViewModel ediSegment)
    {
        return !ModelState.IsValid
            ? CustomResponse(ModelState)
            : CustomResponse(await _ediSegmentService.Update(ediSegment));
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpDelete("{id:guid}")]
    [RateLimit(PeriodInSec = 10, Limit = 10, RouteParams = "id")]
    public async Task<IActionResult> Remove(Guid id)
    {
        return CustomResponse(await _ediSegmentService.Remove(id));
    }

    /// <summary>
    ///     Get total number of segments
    /// </summary>
    [Authorize(Roles = "PlayAdmin")]
    [HttpGet("count")]
    [RateLimit(PeriodInSec = 10, Limit = 10)]
    public async Task<IActionResult> Count()
    {
        return CustomResponse(new { count = await _ediSegmentService.GetTotalCount() });
    }
}
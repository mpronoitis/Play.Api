namespace Play.Services.Api.Controllers.Contact;

[Route("contact")]
public class ContactRequestController : ApiController
{
    private readonly IContactRequestService _contactRequestService;

    public ContactRequestController(IContactRequestService contactRequestService)
    {
        _contactRequestService = contactRequestService;
    }

    [HttpPost]
    [AllowAnonymous]
    [RateLimit(PeriodInSec = 60, Limit = 2, BodyParams = "contactRequestDto")]
    public async Task<IActionResult> Create([FromBody] ContactRequestViewModel contactRequestDto)
    {
        var result = await _contactRequestService.SendContactRequest(contactRequestDto);
        return CustomResponse(result);
    }
}
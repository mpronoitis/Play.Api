namespace Play.Services.Api.Controllers.Pylon;

[Route("pylon/sessions")]
public class PylonSessionController : ApiController
{
    private readonly IPylonSessionService _pylonSessionService;

    public PylonSessionController(IPylonSessionService pylonSessionService)
    {
        _pylonSessionService = pylonSessionService;
    }


    [Authorize(Roles = "PlayAdmin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllSessions()
    {
        try
        {
            var sys = await _pylonSessionService.GetAllSessionsAsync();
            return CustomResponse(sys);
        }
        catch (Exception e)
        {
            AddError(e.Message);
            return CustomResponse();
        }
    }
}
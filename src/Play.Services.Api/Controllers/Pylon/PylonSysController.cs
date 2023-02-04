namespace Play.Services.Api.Controllers.Pylon;

[Route("pylon/sys")]
public class PylonSysController : ApiController
{
    private readonly IPylonSysService _pylonSysService;

    public PylonSysController(IPylonSysService pylonSysService)
    {
        _pylonSysService = pylonSysService;
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpGet("{pokey}")]
    public async Task<IActionResult> GetSysByKey(string pokey)
    {
        try
        {
            var sys = await _pylonSysService.GetByKey(pokey);
            return CustomResponse(new { value = sys });
        }
        catch (Exception e)
        {
            AddError(e.Message);
            return CustomResponse();
        }
    }
}
namespace Play.Services.Api.Controllers.Pylon;

[Route("pylon/ldap")]
public class PylonLdifController : ApiController
{
    private readonly IPylonLdifService _pylonLdifService;

    public PylonLdifController(IPylonLdifService pylonLdifService)
    {
        _pylonLdifService = pylonLdifService;
    }

    [Authorize(Roles = "PlayAdmin")]
    [HttpGet("ldif")]
    public async Task<IActionResult> GetLdifFile()
    {
        try
        {
            var ldif = await _pylonLdifService.ExportContactsLdif();
            return ldif;
        }
        catch (Exception e)
        {
            AddError(e.Message);
            return CustomResponse();
        }
    }
}
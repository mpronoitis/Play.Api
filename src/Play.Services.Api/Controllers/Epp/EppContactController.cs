using Play.Epp.Connector.Models.Contacts;

namespace Play.Services.Api.Controllers.Epp;

[Route("epp/contact")]
public class EppContactController : ApiController
{
    private readonly IEppContactService _eppContactService;

    public EppContactController(IEppContactService eppContactService)
    {
        _eppContactService = eppContactService;
    }

    /// <summary>
    ///     Create a new contact
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [Authorize(Roles = "PlayAdmin,PlayBot")]
    public async Task<IActionResult> Create([FromBody] RegisterEppContactViewModel contact)
    {
        return CustomResponse(await _eppContactService.CreateContactAsync(contact));
    }

    /// <summary>
    ///     Update a contact
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    [HttpPost("update")]
    [Authorize(Roles = "PlayAdmin,PlayBot")]
    public async Task<IActionResult> Update([FromBody] EPPContact contact)
    {
        return CustomResponse(await _eppContactService.UpdateContactAsync(contact));
    }

    /// <summary>
    ///     Check if a contact is available for registration
    /// </summary>
    /// <param name="contactId"></param>
    /// <returns></returns>
    [HttpGet("check/{contactId}")]
    [Authorize(Roles = "PlayAdmin,PlayBot")]
    public async Task<IActionResult> Check(string contactId)
    {
        try
        {
            return CustomResponse(new
                { available = await _eppContactService.CheckContactAvailabilityAsync(contactId) });
        }
        catch (Exception e)
        {
            AddError(e.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Suggest an available contact id
    /// </summary>
    /// <returns></returns>
    [HttpGet("suggest")]
    [Authorize(Roles = "PlayAdmin,PlayBot")]
    public async Task<IActionResult> Suggest()
    {
        try
        {
            return CustomResponse(new { contactId = await _eppContactService.SuggestContactIdAsync() });
        }
        catch (Exception e)
        {
            AddError(e.Message);
            return CustomResponse();
        }
    }
}
namespace Play.Services.Api.Controllers.Pylon;

[Route("pylon/contacts")]
public class PylonContactController : ApiController
{
    private readonly IPylonContactService _pylonContactService;
    private readonly IPylonHeContactService _pylonHeContactService;

    public PylonContactController(IPylonHeContactService pylonHeContactService,
        IPylonContactService pylonContactService)
    {
        _pylonHeContactService = pylonHeContactService;
        _pylonContactService = pylonContactService;
    }

    /// <summary>
    ///     get all hecontacts with pagination
    /// </summary>
    /// <param name="page">The page number</param>
    /// <param name="pageSize">The page size</param>
    /// <returns>The hecontacts</returns>
    [HttpGet("all/{page}/{pageSize}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetAll(int page, int pageSize)
    {
        var result = await _pylonHeContactService.GetPylonContactsAsync(page, pageSize);
        return CustomResponse(result);
    }

    /// <summary>
    ///     Get contacts by phone number , no pagination , descending order by creation date
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns>List of contacts</returns>
    [HttpGet("phone/{phoneNumber}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetByPhoneNumber(string phoneNumber)
    {
        var result = await _pylonHeContactService.GetPylonContactsByPhoneNumberAsync(phoneNumber);
        return CustomResponse(result);
    }

    /// <summary>
    ///     Get contacts by email , no pagination , descending order by creation date
    /// </summary>
    /// <param name="email"></param>
    /// <returns>List of contacts</returns>
    [HttpGet("email/{email}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await _pylonHeContactService.GetPylonContactsByEmailAsync(email);
        return CustomResponse(result);
    }

    /// <summary>
    ///     Get contacts by name , no pagination , descending order by creation date
    /// </summary>
    /// <param name="name"></param>
    /// <returns>List of contacts</returns>
    [HttpGet("name/{name}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetByName(string name)
    {
        var result = await _pylonHeContactService.GetPylonContactsByNameAsync(name);
        return CustomResponse(result);
    }

    /// <summary>
    ///     Get all PylonContacts with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    [HttpGet("temp/all/{page}/{pageSize}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetAllTemp(int page, int pageSize)
    {
        var result = await _pylonContactService.GetPylonContactsAsync(page, pageSize);
        return CustomResponse(result);
    }

    /// <summary>
    ///     Search for PylonContacts
    /// </summary>
    /// <param name="query">Query to search</param>
    /// <param name="name">If we want to search by name</param>
    /// <param name="phone">If we want to search by phone</param>
    /// <param name="email">If we want to search by email</param>
    /// <param name="address">If we want to search by address</param>
    /// <returns></returns>
    [HttpGet("temp/search/{query}/{name}/{phone}/{email}/{address}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> SearchTemp(string query, bool name, bool phone, bool email, bool address)
    {
        var result = await _pylonContactService.SearchPylonContactsAsync(query, name, phone, email, address);
        return CustomResponse(result);
    }

    /// <summary>
    ///     Get count of contacts created in a given date range
    /// </summary>
    /// <param name="from">From date</param>
    /// <param name="to">To date</param>
    /// <returns></returns>
    [HttpGet("count/{from}/{to}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetCount(DateTime from, DateTime to)
    {
        var result = await _pylonHeContactService.GetPylonContactsCountByDateRangeAsync(from, to);
        return CustomResponse(new { count = result });
    }
}
namespace Play.Services.Api.Controllers.Whmcs;

[Route("whmcs/clients")]
public class WhmcsClientController : ApiController
{
    private readonly ILogger<WhmcsClientController> _logger;
    private readonly IWhmcsClientService _whmcsClientService;

    public WhmcsClientController(IWhmcsClientService whmcsClientService, ILogger<WhmcsClientController> logger)
    {
        _whmcsClientService = whmcsClientService;
        _logger = logger;
    }

    /// <summary>
    ///     GetClients
    /// </summary>
    /// <param name="limitstart">The offset for the returned client data (default: 0).Optional</param>
    /// <param name="limitnum">The number of records to return (default: 25).Optional</param>
    /// <param name="sorting">The direction to sort the results. ASC or DESC. Default: ASC.Optional</param>
    /// <param name="status">Optional desired Client Status. ‘Active’, ‘Inactive’, or ‘Closed’.Optional</param>
    /// <param name="search">
    ///     The search term to look for at the start of email, firstname, lastname, fullname or
    ///     companyname.Optional
    /// </param>
    /// <param name="orderby">
    ///     The column to order by. id, firstname, lastname, companyname, email, groupid, datecreated,
    ///     status.Optional
    /// </param>
    /// <returns>json</returns>
    [HttpGet("getclients/{limitstart?}/{limitnum?}/{sorting?}/{status?}/{search?}/{orderby?}")]
    public async Task<IActionResult> GetClients(int limitstart = 0, int limitnum = 25, string sorting = "ASC",
        string status = "", string search = "", string orderby = "")
    {
        try
        {
            var result = await _whmcsClientService.GetClients(limitstart, limitnum, sorting, status, search, orderby);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetClients");
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     AddClient
    /// </summary>
    /// <param name="client">
    ///     <see cref="WhmcsAddClient" />
    /// </param>
    /// <returns> ActionResult </returns>
    [HttpPost("addclient")]
    public async Task<IActionResult> AddClient([FromBody] WhmcsAddClient client)
    {
        try
        {
            var result = await _whmcsClientService.AddClient(client);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AddClient");
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     CloseClient
    /// </summary>
    /// <param name="clientid">The ID of the client to close.Required</param>
    /// return json string with result with status and clientid with id of closed client
    [HttpPost("closeclient")]
    public async Task<IActionResult> CloseClient(int clientid)
    {
        try
        {
            var result = await _whmcsClientService.CloseClient(clientid);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CloseClient");
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     GetClientsDomains
    ///     Obtain a list of Client Purchased Domains matching the provided criteria
    /// </summary>
    /// <param name="limitstart">The offset for the returned log data (default: 0).Optional</param>
    /// <param name="limitnum">The number of records to return (default: 25).Optional</param>
    /// <param name="clientid">The client id to obtain the details for. Optional</param>
    /// <param name="domainid">The specific domain id to obtain the details for. Optional</param>
    /// <param name="domain">The specific domain name to obtain the details for. Optional</param>
    /// <docs>https://developers.whmcs.com/api-reference/getclientsdomains/</docs>
    /// <returns>
    ///     json string with result -> The result of the operation: success or error, clientid -> The specific client id
    ///     searched for, domainid -> The specific domain id searched for, totalresults -> The total number of results
    ///     available, startnumber -> The starting number for the returned results, numreturned -> The number of records
    ///     returned, domains -> The domains that match the criteria passed
    /// </returns>
    [HttpGet("getclientsdomains/{limitstart?}/{limitnum?}/{clientid?}/{domainid?}/{domain?}")]
    public async Task<IActionResult> GetClientsDomains(int limitstart = 0, int limitnum = 25, int clientid = 0,
        int domainid = 0, string domain = "")
    {
        try
        {
            var result = await _whmcsClientService.GetClientsDomains(limitstart, limitnum, clientid, domainid, domain);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetClientsDomains");
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     GetClientsProducts
    ///     Obtain a list of Client Purchased Products matching the provided criteria
    /// </summary>
    /// <param name="limitstart">The offset for the returned log data (default: 0).Optional</param>
    /// <param name="limitnum">The number of records to return (default: 25).Optional</param>
    /// <param name="clientid">The client id to obtain the details for. Optional</param>
    /// <param name="serviceid">The specific service id to obtain the details for. Optional</param>
    /// <param name="pid">The specific product id to obtain the details for. Optional</param>
    /// <param name="domain">The specific domain to obtain the service details for. Optional</param>
    /// <param name="username2">The specific username to obtain the service details for. Optional</param>
    /// <docs>https://developers.whmcs.com/api-reference/getclientsproducts/</docs>
    /// <returns>
    ///     json string result	string	The result of the operation: success or error ,clientid	int	The specific client id
    ///     searched for, serviceid	int	The specific service id searched for, pid	int	The specific product id searched for,
    ///     domain	string	The specific domain searched for, totalresults	int	The total number of results available, startnumber
    ///     int	The starting number for the returned results, numreturned int	The total number of results returned, products
    ///     array	The products returned matching the criteria passed
    /// </returns>
    [HttpGet("getclientsproducts/{limitstart?}/{limitnum?}/{clientid?}/{serviceid?}/{pid?}/{domain?}/{username2?}")]
    public async Task<IActionResult> GetClientsProducts(int limitstart = 0, int limitnum = 25, int clientid = 0,
        int serviceid = 0, int pid = 0, string domain = "", string username2 = "")
    {
        try
        {
            var result = await _whmcsClientService.GetClientsProducts(limitstart, limitnum, clientid, serviceid, pid,
                domain,
                username2);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetClientsProducts");
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     GetClientsDetails
    ///     Obtain the Clients Details for a specific client
    /// </summary>
    /// <param name="clientid">The client id to obtain the details for. $clientid or $email is required</param>
    /// <param name="email">The email address of the client to obtain the details for. $clientid or $email is required</param>
    /// <param name="status">Also return additional client statistics. Optional,Default: false</param>
    /// <docs>https://developers.whmcs.com/api-reference/getclientsdetails/</docs>
    /// <returns>
    ///     json string with result -> The result of the operation: success or error, client -> The client
    ///     details(array),stats -> The client statistics(array)
    /// </returns>
    [HttpGet("getclientsdetails/{clientid?}/{email?}/{status?}")]
    public async Task<IActionResult> GetClientsDetails(int clientid = 0, string email = "", bool status = false)
    {
        try
        {
            var result = await _whmcsClientService.GetClientsDetails(clientid, email, status);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetClientsDetails");
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Get Clients that have demo packages that have exceeded the demo period (1 month)
    /// </summary>
    /// <returns>Array of clients</returns>
    [HttpGet("getclientswithexpiredpackages")]
    public async Task<IActionResult> GetClientsWithExpiredPackages()
    {
        try
        {
            var result = await _whmcsClientService.GetClientsWithExpiredDemoPackages();
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetClientsWithExpiredPackages");
            AddError(ex.Message);
            return CustomResponse();
        }
    }
}
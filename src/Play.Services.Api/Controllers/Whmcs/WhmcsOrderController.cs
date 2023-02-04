namespace Play.Services.Api.Controllers.Whmcs;

[Route("whmcs/orders")]
public class WhmcsOrderController : ApiController
{
    private readonly ILogger<WhmcsOrderController> _logger;
    private readonly IWhmcsOrderService _whmcsOrderService;

    public WhmcsOrderController(IWhmcsOrderService whmcsOrderService, ILogger<WhmcsOrderController> logger)
    {
        _whmcsOrderService = whmcsOrderService;
        _logger = logger;
    }

    /// <summary>
    ///     GetOrders
    ///     Obtain orders matching the passed criteria
    /// </summary>
    /// <param name="limitstart">The offset for the returned order data (default: 0). Optional</param>
    /// <param name="limitnum">The number of records to return (default: 25). Optional</param>
    /// <param name="id">Find orders for a specific id. Optional</param>
    /// <param name="userid">Find orders for a specific userid. Optional</param>
    /// <param name="requestorId">Find orders for a specific requestor_id. Optional</param>
    /// <param name="status">Find orders for a specific status. Optional</param>
    [HttpGet("getorders/{limitstart?}/{limitnum?}/{id?}/{userid?}/{requestorId?}/{status?}")]
    public async Task<IActionResult> GetOrders(int limitstart = 0, int limitnum = 25, int id = 0, int userid = 0,
        int requestorId = 0, string status = "")
    {
        try
        {
            var result = await _whmcsOrderService.GetOrders(limitstart, limitnum, id, userid, requestorId, status);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetOrders");
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     GetProducts
    ///     Retrieve configured products matching provided criteria
    /// </summary>
    /// <param name="pid">
    ///     The product ID to retrieve. Obtain a specific product id configuration. Can be a list of ids comma
    ///     separated Optional
    /// </param>
    /// <param name="gid">Retrieve products in a specific group id. Optional</param>
    /// <param name="module">Retrieve products utilising a specific module. Optional</param>
    /// <docs>https://developers.whmcs.com/api-reference/getproducts/</docs>
    /// <returns>
    ///     json string with result of operation,totalresults(int) The total number of results available,startnumber(int)
    ///     The starting number for the returned results,numreturned(int) The number of results returned,products(array) An
    ///     array of products matching the criteria passed
    /// </returns>
    [HttpGet("getproducts/{pid?}/{gid?}/{module?}")]
    public async Task<IActionResult> GetProducts(int pid = 0, int gid = 0, string module = "")
    {
        try
        {
            var result = await _whmcsOrderService.GetProducts(pid, gid, module);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetProducts");
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Add Order
    ///     Add a new order
    /// </summary>
    /// <param name="order">Order <see cref="WhmcsAddOrder" /></param>
    [HttpPost("addorder")]
    public async Task<IActionResult> AddOrder([FromBody] WhmcsAddOrder order)
    {
        try
        {
            var result = await _whmcsOrderService.AddOrder(order);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AddOrder");
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Accept order
    ///     Accept an order
    /// </summary>
    /// <param name="acceptOrderModel">The order to accept <see cref="WhmcsAcceptOrder" /></param>
    [HttpPost("acceptorder")]
    public async Task<IActionResult> AcceptOrder([FromBody] WhmcsAcceptOrder acceptOrderModel)
    {
        try
        {
            var result = await _whmcsOrderService.AcceptOrder(acceptOrderModel);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AcceptOrder");
            AddError(ex.Message);
            return CustomResponse();
        }
    }
}
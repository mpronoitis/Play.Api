namespace Play.Services.Api.Controllers.Edi;

[Route("edi/credit")]
public class EdiCreditController : ApiController
{
    private readonly IEdiCreditService _ediCreditService;

    public EdiCreditController(IEdiCreditService ediCreditService)
    {
        _ediCreditService = ediCreditService;
    }

    /// <summary>
    ///     Get credit by id
    /// </summary>
    /// <param name="id">Credit id</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetCreditByIdAsync([FromRoute] Guid id)
    {
        var credit = await _ediCreditService.GetEdiCreditById(id);
        return CustomResponse(credit);
    }

    /// <summary>
    ///     Get by customer id
    /// </summary>
    /// <param name="customerId">Customer id</param>
    /// <returns></returns>
    [HttpGet("customer/{customerId}")]
    [Authorize(Roles = "PlayAdmin,Customer")]
    public async Task<IActionResult> GetCreditByCustomerIdAsync([FromRoute] Guid customerId)
    {
        var credit = await _ediCreditService.GetEdiCreditByCustomerId(customerId);
        return CustomResponse(credit);
    }

    /// <summary>
    ///     Get all with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    [HttpGet("all/{page}/{pageSize}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> GetAllAsync([FromRoute] int page, [FromRoute] int pageSize)
    {
        var credits = await _ediCreditService.GetAllEdiCreditWithPaging(page, pageSize);
        return CustomResponse(credits);
    }

    /// <summary>
    ///     Register credit
    /// </summary>
    /// <param name="credit">Register Credit Viewmodel</param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> RegisterCreditAsync([FromBody] RegisterEdiCreditViewModel credit)
    {
        var result = await _ediCreditService.RegisterEdiCredit(credit);
        return CustomResponse(result);
    }

    /// <summary>
    ///     Update credit
    /// </summary>
    /// <param name="credit">Update Credit Viewmodel</param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> UpdateCreditAsync([FromBody] UpdateEdiCreditViewModel credit)
    {
        var result = await _ediCreditService.UpdateEdiCredit(credit);
        return CustomResponse(result);
    }

    /// <summary>
    ///     Delete credit
    /// </summary>
    /// <param name="id">Credit id</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "PlayAdmin")]
    public async Task<IActionResult> DeleteCreditAsync([FromRoute] Guid id)
    {
        var result = await _ediCreditService.RemoveEdiCredit(id);
        return CustomResponse(result);
    }
}
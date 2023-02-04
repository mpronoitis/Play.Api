namespace Play.Services.Api.Controllers.Edi;

[Route("edi/reports")]
public class EdiReportController : ApiController
{
    private readonly IEdiReportRepository _ediReportRepository;

    public EdiReportController(IEdiReportRepository ediReportRepository)
    {
        _ediReportRepository = ediReportRepository;
    }

    /// <summary>
    ///     Function to get number of documents in the EDI system for a given date range
    /// </summary>
    /// <param name="startDate">Start date of the range</param>
    /// <param name="endDate">End date of the range</param>
    /// <param name="customerId">Customer Id</param>
    /// <param name="period">The period to format the report , daily , weekly, monthly</param>
    /// <returns> object list </returns>
    [HttpGet("count/{startDate}/{endDate}/{customerId}/{period}")]
    public async Task<IActionResult> GetEdiDocumentCount(DateTime startDate, DateTime endDate, Guid customerId,
        string period)
    {
        try
        {
            var result = await _ediReportRepository.GetDocumentCountByCustomer(startDate, endDate, customerId, period);
            return Ok(result);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return CustomResponse();
        }
    }

    /// <summary>
    ///     Function to get number of documents in the EDI system for a given date range for all customers
    /// </summary>
    /// <param name="startDate">Start date of the range</param>
    /// <param name="endDate">End date of the range</param>
    /// <param name="period">The period to format the report , daily , weekly, monthly</param>
    /// <returns> object </returns>
    [HttpGet("total-count/{startDate}/{endDate}/{period}")]
    public async Task<IActionResult> GetEdiDocumentCount(DateTime startDate, DateTime endDate, string period)
    {
        try
        {
            var result = await _ediReportRepository.GetDocumentCount(startDate, endDate, period);
            return Ok(result);
        }
        catch (Exception ex)
        {
            AddError(ex.Message);
            return CustomResponse();
        }
    }
}
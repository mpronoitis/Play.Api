using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Play.Domain.Edi.Interfaces;

public interface IEdiReportRepository
{
    /// <summary>
    ///     Function to get number of documents in the EDI system for a given date range
    /// </summary>
    /// <param name="startDate">Start date of the range</param>
    /// <param name="endDate">End date of the range</param>
    /// <param name="customerId">Customer Id</param>
    /// <param name="period">The period to format the report , daily , weekly, monthly</param>
    /// <returns> object list </returns>
    Task<List<object>> GetDocumentCountByCustomer(DateTime startDate, DateTime endDate, Guid customerId, string period);

    /// <summary>
    ///     Function to get number of documents in the EDI system for a given date range for all customers
    /// </summary>
    /// <param name="startDate">Start date of the range</param>
    /// <param name="endDate">End date of the range</param>
    /// <param name="period">The period to format the report , daily , weekly, monthly</param>
    /// <returns> object </returns>
    Task<List<object>> GetDocumentCount(DateTime startDate, DateTime endDate, string period);
}
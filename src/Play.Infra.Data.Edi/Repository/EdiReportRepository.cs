namespace Play.Infra.Data.Edi.Repository;

/// <summary>
///     Class containing methods for receiving reports for the EDI system.
/// </summary>
public class EdiReportRepository : IEdiReportRepository
{
    private readonly IEdiDocumentRepository _ediDocumentRepository;

    public EdiReportRepository(IEdiDocumentRepository ediDocumentRepository)
    {
        _ediDocumentRepository = ediDocumentRepository;
    }

    /// <summary>
    ///     Function to get number of documents in the EDI system for a given date range
    /// </summary>
    /// <param name="startDate">Start date of the range</param>
    /// <param name="endDate">End date of the range</param>
    /// <param name="customerId">Customer Id</param>
    /// <param name="period">The period to format the report , daily , weekly, monthly</param>
    /// <returns> object </returns>
    public async Task<List<object>> GetDocumentCountByCustomer(DateTime startDate, DateTime endDate, Guid customerId,
        string period)
    {
        //check that period is valid otherwise throw exception
        if (period != "daily" && period != "weekly" && period != "monthly") throw new Exception("Invalid period");
        //get documents for the given date range and customer
        //var documents =
        //    await _ediDocumentRepository.GetAllWithDateRangeAndCustomerIdAsync(startDate, endDate, customerId);
        var documents =
            await _ediDocumentRepository.GetAllWithNoPayloadsAndDateRangeAndCustomerIdAsync(startDate, endDate, customerId);
        //create a list of objects to hold the report data
        var reportData = new List<object>();
        //switch statement to format the report based on the period
        switch (period)
        {
            case "daily":
                //group the documents by date
                var dailyGroup = documents.GroupBy(x => x.Created_At.Date);
                //loop through the grouped documents
                foreach (var group in dailyGroup)
                    //add the date and the number of documents to the report data
                    reportData.Add(new
                    {
                        Date = group.Key.ToString("dd/MM/yyyy"),
                        Count = group.Count()
                    });
                break;
            case "weekly":
                //group the documents by week
                var weeklyGroup = documents.GroupBy(x =>
                    CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(x.Created_At, CalendarWeekRule.FirstDay,
                        DayOfWeek.Monday));
                //loop through the grouped documents
                foreach (var group in weeklyGroup)
                    //add the week and the number of documents to the report data
                    reportData.Add(new
                    {
                        Week = group.Key,
                        Count = group.Count()
                    });
                break;
            case "monthly":
                //group the documents by month
                var monthlyGroup = documents.GroupBy(x => x.Created_At.Month);
                //loop through the grouped documents
                foreach (var group in monthlyGroup)
                    //add the month and the number of documents to the report data
                    reportData.Add(new
                    {
                        //get month in english
                        Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key),
                        Count = group.Count()
                    });
                
                //add missing months with value 0
                for (var i = 1; i <= 12; i++)
                    if (monthlyGroup.All(x => x.Key != i))
                        reportData.Add(new
                        {
                            Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i),
                            Count = 0
                        });
                break;
        }

        return reportData;
    }

    /// <summary>
    ///     Function to get number of documents in the EDI system for a given date range for all customers
    /// </summary>
    /// <param name="startDate">Start date of the range</param>
    /// <param name="endDate">End date of the range</param>
    /// <param name="period">The period to format the report , daily , weekly, monthly</param>
    /// <returns> object </returns>
    public async Task<List<object>> GetDocumentCount(DateTime startDate, DateTime endDate, string period)
    {
        //check that period is valid otherwise throw exception
        if (period != "daily" && period != "weekly" && period != "monthly") throw new Exception("Invalid period");
        //get documents for the given date range
        var documents = await _ediDocumentRepository.GetAllWithNoPayloadsAndDateRangeAsync(startDate, endDate);
        //create a list of objects to hold the report data
        var reportData = new List<object>();
        //switch statement to format the report based on the period
        switch (period)
        {
            case "daily":
                //group the documents by date
                var dailyGroup = documents.GroupBy(x => x.Created_At.Date);
                //loop through the grouped documents
                foreach (var group in dailyGroup)
                    //add the date and the number of documents to the report data
                    reportData.Add(new
                    {
                        Date = group.Key.ToString("dd/MM/yyyy"),
                        Count = group.Count()
                    });
                break;
            case "weekly":
                //group the documents by week
                var weeklyGroup = documents.GroupBy(x =>
                    CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(x.Created_At, CalendarWeekRule.FirstDay,
                        DayOfWeek.Monday));
                //loop through the grouped documents
                foreach (var group in weeklyGroup)
                    //add the week and the number of documents to the report data
                    reportData.Add(new
                    {
                        Week = group.Key,
                        Count = group.Count()
                    });
                break;
            case "monthly":
                //group the documents by month
                var monthlyGroup = documents.GroupBy(x => x.Created_At.Month);
                //loop through the grouped documents
                foreach (var group in monthlyGroup)
                    //add the month and the number of documents to the report data
                    reportData.Add(new
                    {
                        //get month in english
                        Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key),
                        Count = group.Count()
                    });
                break;
        }

        return reportData;
    }
}
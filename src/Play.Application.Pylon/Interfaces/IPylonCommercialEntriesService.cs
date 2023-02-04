namespace Play.Application.Pylon.Interfaces;

public interface IPylonCommercialEntriesService
{
    /// <summary>
    ///     Get total income for a given time range
    /// </summary>
    /// <param name="from">Start date</param>
    /// <param name="to">End date</param>
    /// <returns>Total income</returns>
    Task<decimal> GetTotalIncome(DateTime from, DateTime to);
}

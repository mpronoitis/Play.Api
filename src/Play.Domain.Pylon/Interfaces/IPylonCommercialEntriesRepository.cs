using PylonDatabaseHandler.models.pylon;

namespace Play.Domain.Pylon.Interfaces;

public interface IPylonCommercialEntriesRepository
{
    /// <summary>
    ///     Function to get a CommercialEntry based on its HEID (primary key) GUID
    /// </summary>
    /// <param name="heid">The HEID GUID</param>
    /// <returns>A CommercialEntry object</returns>
    /// <exception cref="ArgumentNullException">Thrown when the HEID is null</exception>
    /// <exception cref="ArgumentException">Thrown when the HEID is empty</exception>
    Task<Hecommercialentries?> GetCommercialEntryByHeidAsync(Guid heid);

    /// <summary>
    ///     Function to get all CommercialEntries that match a given TIN
    /// </summary>
    /// <param name="tin">The TIN to search for</param>
    /// <returns>A list of CommercialEntry objects</returns>
    /// <exception cref="ArgumentNullException">Thrown when the TIN is null</exception>
    /// <exception cref="ArgumentException">Thrown when the TIN is empty</exception>
    Task<List<Hecommercialentries>> GetCommercialEntriesByTinAsync(string tin);

    /// <summary>
    ///     Function to get all CommercialEntries that match a given TIN , with pagination
    /// </summary>
    /// <param name="tin">The TIN to search for</param>
    /// <param name="pageNumber">The page number to return</param>
    /// <param name="pageSize">The number of items per page</param>
    /// <returns>A list of CommercialEntry objects</returns>
    /// <exception cref="ArgumentNullException">Thrown when the TIN is null</exception>
    /// <exception cref="ArgumentException">Thrown when the TIN is empty</exception>
    Task<List<Hecommercialentries>> GetCommercialEntriesByTinAsync(string tin, int pageNumber, int pageSize);

    /// <summary>
    ///     Get the total income for a given date range
    /// </summary>
    /// <param name="startDate">The start date of the range</param>
    /// <param name="endDate">The end date of the range</param>
    /// <returns>The total income</returns>
    Task<decimal> GetTotalIncomeAsync(DateTime startDate, DateTime endDate);
}
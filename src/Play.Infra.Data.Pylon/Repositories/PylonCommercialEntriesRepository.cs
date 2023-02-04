using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Play.Domain.Pylon.Interfaces;
using PylonDatabaseHandler;
using PylonDatabaseHandler.contexts.pylon;
using PylonDatabaseHandler.models.pylon;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonCommercialEntriesRepository : IPylonCommercialEntriesRepository
{
    private readonly PylonCommercialContext _context;

    public PylonCommercialEntriesRepository(IConfiguration configuration, IPylonDatabase database)
    {
        var connectionString = configuration.GetConnectionString("PylonDatabase");
        _context = database
            .InitializeContext<PylonCommercialContext>(connectionString ??
                                                       throw new InvalidOperationException(
                                                           "Connection string not found")).Result;
    }

    /// <summary>
    ///     Function to get a CommercialEntry based on its HEID (primary key) GUID
    /// </summary>
    /// <param name="heid">The HEID GUID</param>
    /// <returns>A CommercialEntry object</returns>
    /// <exception cref="ArgumentNullException">Thrown when the HEID is null</exception>
    /// <exception cref="ArgumentException">Thrown when the HEID is empty</exception>
    public async Task<Hecommercialentries?> GetCommercialEntryByHeidAsync(Guid heid)
    {
        if (heid == Guid.Empty)
            throw new ArgumentException("The HEID cannot be empty", nameof(heid));

        return await _context.Hecommercialentries.FindAsync(heid);
    }

    /// <summary>
    ///     Function to get all CommercialEntries that match a given TIN
    /// </summary>
    /// <param name="tin">The TIN to search for</param>
    /// <returns>A list of CommercialEntry objects</returns>
    /// <exception cref="ArgumentNullException">Thrown when the TIN is null</exception>
    /// <exception cref="ArgumentException">Thrown when the TIN is empty</exception>
    public async Task<List<Hecommercialentries>> GetCommercialEntriesByTinAsync(string tin)
    {
        if (tin == null)
            throw new ArgumentNullException(nameof(tin), "The TIN cannot be null");
        if (tin == string.Empty)
            throw new ArgumentException("The TIN cannot be empty", nameof(tin));

        return await _context.Hecommercialentries.Where(ce => ce.Hetin == tin).AsNoTracking()
            .OrderByDescending(x => x.Heexecutiondate)
            .ToListAsync();
    }

    /// <summary>
    ///     Function to get all CommercialEntries that match a given TIN , with pagination
    /// </summary>
    /// <param name="tin">The TIN to search for</param>
    /// <param name="pageNumber">The page number to return</param>
    /// <param name="pageSize">The number of items per page</param>
    /// <returns>A list of CommercialEntry objects</returns>
    /// <exception cref="ArgumentNullException">Thrown when the TIN is null</exception>
    /// <exception cref="ArgumentException">Thrown when the TIN is empty</exception>
    public async Task<List<Hecommercialentries>> GetCommercialEntriesByTinAsync(string tin, int pageNumber,
        int pageSize)
    {
        if (tin == null)
            throw new ArgumentNullException(nameof(tin), "The TIN cannot be null");
        if (tin == string.Empty)
            throw new ArgumentException("The TIN cannot be empty", nameof(tin));

        return await _context.Hecommercialentries.Where(ce => ce.Hetin == tin).AsNoTracking()
            .OrderByDescending(x => x.Heexecutiondate)
            .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    /// <summary>
    ///     Get the total income for a given date range
    /// </summary>
    /// <param name="startDate">The start date of the range</param>
    /// <param name="endDate">The end date of the range</param>
    /// <returns>The total income</returns>
    public async Task<decimal> GetTotalIncomeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Hecommercialentries
            .Where(ce => ce.Heexecutiondate >= startDate && ce.Heexecutiondate <= endDate)
            .AsNoTracking().SumAsync(ce => ce.Hettotalval);
    }
}
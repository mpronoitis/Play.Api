using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Play.Domain.Pylon.Interfaces;
using PylonDatabaseHandler;
using PylonDatabaseHandler.contexts.pylon;
using PylonDatabaseHandler.models.pylon;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonTraderRepository : IPylonTraderRepository
{
    private readonly PylonCrmContext _context;


    public PylonTraderRepository(IConfiguration configuration, IPylonDatabase database)
    {
        var connectionString = configuration.GetConnectionString("PylonDatabase");
        _context = database.InitializeContext<PylonCrmContext>(connectionString ??
                                                               throw new InvalidOperationException(
                                                                   "PylonDatabase connection string is null")).Result;
    }

    /// <summary>
    ///     Function to get a trader by his heid (primary key) GUID
    /// </summary>
    /// <param name="heid">The GUID of the trader</param>
    /// <returns>The trader</returns>
    /// <exception cref="ArgumentNullException">Thrown when the heid is null</exception>
    /// <exception cref="ArgumentException">Thrown when the heid is empty</exception>
    public async Task<Hetraders?> GetTraderByHeidAsync(Guid heid)
    {
        if (heid == Guid.Empty)
            throw new ArgumentException("The heid cannot be empty", nameof(heid));

        return await _context.Hetraders.FirstOrDefaultAsync(t => t.Heid == heid);
    }

    /// <summary>
    ///     Function to get a trader by HECntcID (foreign key) GUID
    /// </summary>
    /// <param name="heCntcId">The Contact Id of the trader</param>
    /// <returns>The trader</returns>
    /// <exception cref="ArgumentNullException">Thrown when the heCntcId is null</exception>
    /// <exception cref="ArgumentException">Thrown when the heCntcId is empty</exception>
    public async Task<Hetraders?> GetTraderByHeCntcIdAsync(Guid heCntcId)
    {
        if (heCntcId == Guid.Empty)
            throw new ArgumentException("The heCntcId cannot be empty", nameof(heCntcId));

        return await _context.Hetraders.FirstOrDefaultAsync(t => t.Hecntcid == heCntcId);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Play.Domain.Pylon.Interfaces;
using PylonDatabaseHandler;
using PylonDatabaseHandler.contexts.pylon;
using PylonDatabaseHandler.models.pylon;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonCentlineRepository : IPylonCentlineRepository
{
    private readonly PylonCommercialContext _context;

    public PylonCentlineRepository(IConfiguration configuration, IPylonDatabase database)
    {
        var connectionString = configuration.GetConnectionString("PylonDatabase");
        _context = database
            .InitializeContext<PylonCommercialContext>(connectionString ??
                                                       throw new InvalidOperationException(
                                                           "Pylon Connection string not found")).Result;
    }

    /// <summary>
    ///     Function to get a Centline by its HEID (primary key) GUID
    /// </summary>
    /// <param name="heid">The HEID GUID</param>
    /// <returns>The Centline</returns>
    /// <exception cref="ArgumentNullException">Thrown when the HEID is null</exception>
    /// <exception cref="ArgumentException">Thrown when the HEID is empty</exception>
    public async Task<Hecentlines?> GetCentlineByHeidAsync(Guid heid)
    {
        if (heid == Guid.Empty)
            throw new ArgumentException("The HEID cannot be empty", nameof(heid));

        return await _context.Hecentlines.FindAsync(heid);
    }

    /// <summary>
    ///     Function to get all centlines based on a Hedentid
    /// </summary>
    /// <param name="hedentid">The Hedentid</param>
    /// <returns>A list of centlines</returns>
    /// <exception cref="ArgumentNullException">Thrown when the Hedentid is null</exception>
    /// <exception cref="ArgumentException">Thrown when the Hedentid is empty</exception>
    public async Task<List<Hecentlines>> GetCentlinesByHedentidAsync(Guid hedentid)
    {
        if (hedentid == Guid.Empty)
            throw new ArgumentException("The Hedentid cannot be empty", nameof(hedentid));

        return await _context.Hecentlines.Where(x => x.Hedentid == hedentid).AsNoTracking().ToListAsync();
    }
}
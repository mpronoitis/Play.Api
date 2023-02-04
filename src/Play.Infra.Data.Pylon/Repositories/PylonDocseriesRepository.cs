using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Play.Domain.Pylon.Interfaces;
using PylonDatabaseHandler;
using PylonDatabaseHandler.contexts.pylon;
using PylonDatabaseHandler.models.pylon;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonDocseriesRepository : IPylonDocseriesRepository
{
    private readonly PylonInfraContext _context;

    public PylonDocseriesRepository(IConfiguration configuration, IPylonDatabase db)
    {
        var connectionString = configuration.GetConnectionString("PylonDatabase");
        _context = db
            .InitializeContext<PylonInfraContext>(connectionString ??
                                                  throw new InvalidOperationException("Connection string not found"))
            .Result;
    }

    /// <summary>
    ///     Get a single document series by its HEID (primary key) GUID
    /// </summary>
    /// <param name="heid">The HEID GUID</param>
    /// <returns>The document series</returns>
    public async Task<Hedocseries?> GetDocseriesByHeidAsync(Guid heid)
    {
        return await _context.Hedocseries.FindAsync(heid);
    }

    /// <summary>
    ///     Get a single document series by its hename
    /// </summary>
    /// <param name="hename">The hename</param>
    /// <returns>The document series</returns>
    public async Task<Hedocseries?> GetDocseriesByHenameAsync(string hename)
    {
        return await _context.Hedocseries.AsNoTracking().FirstOrDefaultAsync(x => x.Hename == hename);
    }
}
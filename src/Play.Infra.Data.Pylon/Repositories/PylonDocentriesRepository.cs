using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Play.Domain.Pylon.Interfaces;
using PylonDatabaseHandler;
using PylonDatabaseHandler.contexts.pylon;
using PylonDatabaseHandler.models.pylon;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonDocentriesRepository : IPylonDocentriesRepository
{
    private readonly PylonInfraContext _context;

    public PylonDocentriesRepository(IConfiguration configuration, IPylonDatabase database)
    {
        var connectionString = configuration.GetConnectionString("PylonDatabase");
        _context = database
            .InitializeContext<PylonInfraContext>(connectionString ??
                                                  throw new InvalidOperationException("Connection string not found"))
            .Result;
    }

    /// <summary>
    ///     Get a docentry based on its heid (GUID)
    /// </summary>
    /// <param name="heid">The heid of the docentry</param>
    /// <returns>The docentry</returns>
    /// <exception cref="ArgumentNullException">Thrown when the heid is null</exception>
    /// <exception cref="ArgumentException">Thrown when the heid is empty</exception>
    public async Task<Hedocentries?> GetDocentryByHeidAsync(Guid heid)
    {
        if (heid == Guid.Empty)
            throw new ArgumentException("The heid cannot be empty", nameof(heid));

        return await _context.Hedocentries.FindAsync(heid);
    }

    /// <summary>
    ///     Get docentries count based on a given time range
    /// </summary>
    /// <param name="from">The start of the time range</param>
    /// <param name="to">The end of the time range</param>
    /// <returns>The number of docentries</returns>
    public async Task<int> GetDocentriesCountAsync(DateTime from, DateTime to)
    {
        return await _context.Hedocentries.CountAsync(x => x.Hecreationdate >= from && x.Hecreationdate <= to);
    }
}
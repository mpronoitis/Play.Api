using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Play.Domain.Pylon.Interfaces;
using PylonDatabaseHandler;
using PylonDatabaseHandler.contexts.pylon;
using PylonDatabaseHandler.models.pylon;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonSessionRepository : IPylonSessionRepository
{
    public readonly PylonInfraContext _context;

    public PylonSessionRepository(IConfiguration configuration, IPylonDatabase database)
    {
        var connectionString = configuration.GetConnectionString("PylonDatabase");
        _context = database
            .InitializeContext<PylonInfraContext>(connectionString ??
                                                  throw new InvalidOperationException("Connection string not found"))
            .Result;
    }

    public async Task<IEnumerable<Posessions>> GetAllSessionsAsync()
    {
        return await _context.Posessions.ToListAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Play.Domain.Pylon.Interfaces;
using PylonDatabaseHandler;
using PylonDatabaseHandler.contexts.pylon;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonSysRepository : IPylonSysRepository
{
    public readonly PylonInfraContext _context;

    public PylonSysRepository(IConfiguration configuration, IPylonDatabase database)
    {
        var connectionString = configuration.GetConnectionString("PylonDatabase");
        _context = database
            .InitializeContext<PylonInfraContext>(connectionString ??
                                                  throw new InvalidOperationException("Connection string not found"))
            .Result;
    }

    public async Task<string> GetByKey(string pokey)
    {
        if (pokey == null) throw new ArgumentNullException(nameof(pokey));
        var result = await _context.Posys.FirstOrDefaultAsync(t => t.Pokey == pokey);
        return result?.Povalstr ?? string.Empty;
    }
}
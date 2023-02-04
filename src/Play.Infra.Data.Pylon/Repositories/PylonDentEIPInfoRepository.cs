using Microsoft.Extensions.Configuration;
using Play.Domain.Pylon.Interfaces;
using PylonDatabaseHandler;
using PylonDatabaseHandler.contexts.pylon;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonDentEipInfoRepository : IPylonDentEipInfoRepository
{
    private readonly PylonInfraContext _context;

    public PylonDentEipInfoRepository(IPylonDatabase database, IConfiguration configuration)
    {
        _context = database.InitializeContext<PylonInfraContext>(configuration.GetConnectionString("PylonDatabase") ??
                                                                 throw new InvalidOperationException(
                                                                     "PylonDatabase connection string not found"))
            .Result;
    }

    /// <summary>
    ///     Function to get the Heqrcode for a given heid (primary key) GUID
    /// </summary>
    /// <param name="dentid">The heid GUID</param>
    /// <returns>The heqrcode</returns>
    public string? GetHeqrcodeByDentid(Guid dentid)
    {
        var dentEipInfo = _context.Hedenteipinfo
            .Where(h => h.Hedentid == dentid);

        return dentEipInfo.Any() ? dentEipInfo.First().Heqrcode : string.Empty;
    }
}
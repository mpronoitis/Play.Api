using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Play.Domain.Pylon.Interfaces;
using PylonDatabaseHandler;
using PylonDatabaseHandler.contexts.pylon;
using PylonDatabaseHandler.models.pylon;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonHeItemRepository : IPylonHeItemRepository
{
    private readonly PylonCommercialContext _context;

    public PylonHeItemRepository(IConfiguration configuration, IPylonDatabase database)
    {
        var connectionString = configuration.GetConnectionString("PylonDatabase");
        _context = database
            .InitializeContext<PylonCommercialContext>(connectionString ??
                                                       throw new InvalidOperationException("Connection string is null"))
            .Result;
    }

    /// <summary>
    ///     Function to get a heitem based on its Heid (primary key) GUID
    /// </summary>
    /// <param name="heid">The GUID of the heitem</param>
    /// <returns>The heitem</returns>
    public async Task<Heitems?> GetHeitemByHeid(Guid heid)
    {
        return await _context.Heitems.FindAsync(heid);
    }

    /// <summary>
    ///     Find items based on its name (hename)
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <returns>A list of items</returns>
    public async Task<List<Heitems>> FindHeitemsByName(string name)
    {
        return await _context.Heitems.Where(x => x.Hename.Contains(name)).ToListAsync();
    }

    /// <summary>
    ///     Find items based on its code (hecode)
    /// </summary>
    /// <param name="code">The code of the item</param>
    /// <returns>A list of items</returns>
    public async Task<List<Heitems>> FindHeitemsByCode(string code)
    {
        return await _context.Heitems.Where(x => x.Hecode.Contains(code)).ToListAsync();
    }

    /// <summary>
    ///     Get All items
    /// </summary>
    /// <returns>A list of items</returns>
    public async Task<List<Heitems>> GetAllHeitems()
    {
        return await _context.Heitems.ToListAsync();
    }

    /// <summary>
    ///     Get count of items created in a given period
    /// </summary>
    /// <param name="from">The start date of the period</param>
    /// <param name="to">The end date of the period</param>
    /// <returns>The count of items</returns>
    public async Task<int> GetHeitemsCountByPeriod(DateTime from, DateTime to)
    {
        return await _context.Heitems.CountAsync(x => x.Hecreationdate >= from && x.Hecreationdate <= to);
    }
}
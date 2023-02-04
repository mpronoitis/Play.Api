using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using Play.Domain.Pylon.Interfaces;
using Play.Domain.Pylon.Models;
using Play.Infra.Data.Context;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonItemRepository : IPylonItemRepository
{
    protected readonly PlayPylonContext Db;
    protected readonly DbSet<PylonItem> DbSet;

    public PylonItemRepository(PlayPylonContext context)
    {
        Db = context;
        DbSet = Db.Set<PylonItem>();
    }

    public IUnitOfWork UnitOfWork => Db;

    public async Task<PylonItem?> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    /// <summary>
    ///     Get Item by code
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public async Task<PylonItem?> GetByCode(string code)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Code == code) ?? null;
    }

    /// <summary>
    ///     Get All Items with paging
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<IEnumerable<PylonItem>> GetAll(int page, int pageSize)
    {
        return await DbSet.AsNoTracking().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    /// <summary>
    ///     Get Items by Name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<IEnumerable<PylonItem>> GetByName(string name)
    {
        return await DbSet.AsNoTracking().Where(p => p.Name.ToUpper().Contains(name.ToUpper())).ToListAsync();
    }

    /// <summary>
    ///     Get total count of Items
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetCount()
    {
        return await DbSet.AsNoTracking().CountAsync();
    }

    /// <summary>
    ///     Add Item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public void Add(PylonItem item)
    {
        DbSet.Add(item);
    }

    /// <summary>
    ///     Add list of Items
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public void AddRange(IEnumerable<PylonItem> items)
    {
        DbSet.AddRange(items);
    }

    /// <summary>
    ///     Remove all Items
    /// </summary>
    /// <returns></returns>
    public void RemoveAll()
    {
        DbSet.RemoveRange(DbSet);
    }

    /// <summary>
    ///     Remove Item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public void Remove(PylonItem item)
    {
        DbSet.Remove(item);
    }

    /// <summary>
    ///     Remove range
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public void RemoveRange(IEnumerable<PylonItem> items)
    {
        DbSet.RemoveRange(items);
    }

    public void Dispose()
    {
        Db?.Dispose();
    }
}
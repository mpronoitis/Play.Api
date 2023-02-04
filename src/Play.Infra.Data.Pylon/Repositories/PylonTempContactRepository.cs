using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using Play.Domain.Pylon.Interfaces;
using Play.Domain.Pylon.Models;
using Play.Infra.Data.Context;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonTempContactRepository : IPylonTempContactRepository
{
    protected readonly PlayPylonContext Db;
    protected readonly DbSet<PylonContact> DbSet;

    public PylonTempContactRepository(PlayPylonContext context)
    {
        Db = context;
        DbSet = Db.Set<PylonContact>();
    }

    public IUnitOfWork UnitOfWork => Db;

    public async Task<PylonContact?> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    /// <summary>
    ///     Get all PylonContacts with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    public async Task<IEnumerable<PylonContact>> GetAll(int page, int pageSize)
    {
        return await DbSet.AsNoTracking().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    /// <summary>
    ///     Search for PylonContacts
    /// </summary>
    /// <param name="query">Query to search</param>
    /// <param name="name">If we want to search by name</param>
    /// <param name="phone">If we want to search by phone</param>
    /// <param name="email">If we want to search by email</param>
    /// <param name="address">If we want to search by address</param>
    /// <returns></returns>
    public async Task<IEnumerable<PylonContact>> Search(string query, bool name, bool phone, bool email, bool address)
    {
        //convert to uppercase
        query = query.ToUpper();
        //search based on the parameters
        var result = await DbSet.AsNoTracking().Where(x => (name && x.Name.ToUpper().Contains(query)) ||
                                                           (phone && x.Phones.ToUpper().Contains(query)) ||
                                                           (email && x.Emails.ToUpper().Contains(query)) ||
                                                           (address && x.Address.ToUpper().Contains(query)))
            .ToListAsync();

        return result;
    }

    /// <summary>
    ///     Add a new PylonContact
    /// </summary>
    /// <param name="pylonContact">PylonContact object</param>
    public async Task Add(PylonContact pylonContact)
    {
        await DbSet.AddAsync(pylonContact);
    }

    /// <summary>
    ///     Add a range of PylonContacts
    /// </summary>
    /// <param name="pylonContacts">PylonContact objects</param>
    /// <returns></returns>
    public async Task AddRange(IEnumerable<PylonContact> pylonContacts)
    {
        await DbSet.AddRangeAsync(pylonContacts);
    }

    /// <summary>
    ///     Update a PylonContact
    /// </summary>
    /// <param name="pylonContact">PylonContact object</param>
    public void Update(PylonContact pylonContact)
    {
        DbSet.Update(pylonContact);
    }

    /// <summary>
    ///     Remove a PylonContact
    /// </summary>
    /// <param name="pylonContact">PylonContact object</param>
    public void Remove(PylonContact pylonContact)
    {
        DbSet.Remove(pylonContact);
    }

    public void RemoveRange(IEnumerable<PylonContact> pylonContacts)
    {
        DbSet.RemoveRange(pylonContacts);
    }

    /// <summary>
    ///     Remove all PylonContacts
    /// </summary>
    public void RemoveAll()
    {
        //execute truncate raw query
        Db.Database.ExecuteSqlRaw("TRUNCATE TABLE PylonContacts");
    }
}
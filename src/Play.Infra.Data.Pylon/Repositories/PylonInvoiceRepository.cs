using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using Play.Domain.Pylon.Interfaces;
using Play.Domain.Pylon.Models;
using Play.Infra.Data.Context;

namespace Play.Infra.Data.Pylon.Repositories;

public class PylonInvoiceRepository : IPylonInvoiceRepository
{
    protected readonly PlayPylonContext Db;
    protected readonly DbSet<PylonInvoice> DbSet;

    public PylonInvoiceRepository(PlayPylonContext context)
    {
        Db = context;
        DbSet = Db.Set<PylonInvoice>();
    }

    public IUnitOfWork UnitOfWork => Db;

    public async Task<PylonInvoice?> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<IEnumerable<PylonInvoice>> GetByCustomerTin(string customerTin)
    {
        return await DbSet.AsNoTracking().Where(x => x.CustomerTin == customerTin).ToListAsync();
    }

    /// <summary>
    ///     Get by customer tin with paging
    /// </summary>
    /// <param name="customerTin"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<IEnumerable<PylonInvoice>> GetByCustomerTin(string customerTin, int pageNumber, int pageSize)
    {
        return await DbSet.AsNoTracking().Where(x => x.CustomerTin == customerTin).Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToListAsync();
    }

    /// <summary>
    ///     Get all with paging
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<IEnumerable<PylonInvoice>> GetAll(int pageNumber, int pageSize)
    {
        return await DbSet.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    //add
    public void Add(PylonInvoice pylonInvoice)
    {
        DbSet.Add(pylonInvoice);
    }

    //add range
    public void AddRange(IEnumerable<PylonInvoice> pylonInvoices)
    {
        DbSet.AddRange(pylonInvoices);
    }

    //update
    public void Update(PylonInvoice pylonInvoice)
    {
        DbSet.Update(pylonInvoice);
    }

    //delete
    public void Remove(PylonInvoice invoice)
    {
        DbSet.Remove(invoice);
    }

    //delete range
    public void RemoveRange(IEnumerable<PylonInvoice> pylonInvoices)
    {
        DbSet.RemoveRange(pylonInvoices);
    }

    public void Dispose()
    {
        Db?.Dispose();
    }
}
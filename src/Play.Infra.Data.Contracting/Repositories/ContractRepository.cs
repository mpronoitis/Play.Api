using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using Play.Domain.Contracting.Interfaces;
using Play.Domain.Contracting.Models;
using Play.Infra.Data.Context;

namespace Play.Infra.Data.Contracting.Repositories;

public class ContractRepository : IContractRepository
{
    protected readonly DbSet<Contract> _dbSet;
    protected readonly PlayContext Db;

    public ContractRepository(PlayContext context)
    {
        Db = context;
        _dbSet = Db.Set<Contract>();
    }

    public IUnitOfWork UnitOfWork => Db;

    public async Task<Contract?> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<Contract>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    /// <summary>
    ///     Get all with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    public async Task<IEnumerable<Contract>> GetAll(int page, int pageSize)
    {
        return await _dbSet.Skip((page - 1) * pageSize).Take(pageSize).OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    /// <summary>
    ///     Get all with given customer name
    /// </summary>
    /// <param name="customerName">Customer name</param>
    /// <returns></returns>
    public async Task<IEnumerable<Contract>> GetAll(string customerName)
    {
        return await _dbSet.Where(c => c.ClientName == customerName).ToListAsync();
    }

    /// <summary>
    ///     Get all with given item name
    /// </summary>
    /// <param name="itemName">Item name</param>
    /// <returns></returns>
    public async Task<IEnumerable<Contract>> GetAllByItem(string itemName)
    {
        return await _dbSet.Where(c => c.ItemName == itemName).ToListAsync();
    }


    public void Add(Contract contract)
    {
        _dbSet.Add(contract);
    }

    public void Update(Contract contract)
    {
        _dbSet.Update(contract);
    }

    public void Remove(Contract contract)
    {
        _dbSet.Remove(contract);
    }
}
using NetDevPack.Data;
using Play.Domain.Contracting.Models;

namespace Play.Domain.Contracting.Interfaces;

public interface IContractRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<Contract?> GetById(Guid id);
    Task<IEnumerable<Contract>> GetAll();

    /// <summary>
    ///     Get all with pagination
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    Task<IEnumerable<Contract>> GetAll(int page, int pageSize);

    /// <summary>
    ///     Get all with given customer name
    /// </summary>
    /// <param name="customerName">Customer name</param>
    /// <returns></returns>
    Task<IEnumerable<Contract>> GetAll(string customerName);

    /// <summary>
    ///     Get all with given item name
    /// </summary>
    /// <param name="itemName">Item name</param>
    /// <returns></returns>
    Task<IEnumerable<Contract>> GetAllByItem(string itemName);

    void Add(Contract contract);
    void Update(Contract contract);
    void Remove(Contract contract);
}
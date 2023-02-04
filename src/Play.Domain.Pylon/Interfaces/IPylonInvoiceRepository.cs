using NetDevPack.Data;
using Play.Domain.Pylon.Models;

namespace Play.Domain.Pylon.Interfaces;

public interface IPylonInvoiceRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<PylonInvoice?> GetById(Guid id);
    Task<IEnumerable<PylonInvoice>> GetByCustomerTin(string customerTin);

    /// <summary>
    ///     Get by customer tin with paging
    /// </summary>
    /// <param name="customerTin"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<IEnumerable<PylonInvoice>> GetByCustomerTin(string customerTin, int pageNumber, int pageSize);

    /// <summary>
    ///     Get all with paging
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<IEnumerable<PylonInvoice>> GetAll(int pageNumber, int pageSize);

    void Add(PylonInvoice pylonInvoice);
    void AddRange(IEnumerable<PylonInvoice> pylonInvoices);
    void Update(PylonInvoice pylonInvoice);
    void Remove(PylonInvoice invoice);
    void RemoveRange(IEnumerable<PylonInvoice> pylonInvoices);
    void Dispose();
}
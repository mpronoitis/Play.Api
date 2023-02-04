using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetDevPack.Data;
using Play.Domain.Edi.Models;

namespace Play.Domain.Edi.Interfaces;

public interface IEdiCreditRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<EdiCredit> GetByCreditIdAsync(Guid creditId);
    Task<EdiCredit> GetByCustomerIdAsync(Guid customerId);
    Task<IEnumerable<EdiCredit>> GetAllAsync();
    Task<IEnumerable<EdiCredit>> GetAllWithPagingAsync(int page = 0, int pagesize = 10);
    Task DecrementCreditAsync(Guid CustomerId, int amount);
    void Add(EdiCredit credit);
    void Update(EdiCredit credit);
    void Remove(EdiCredit credit);
}
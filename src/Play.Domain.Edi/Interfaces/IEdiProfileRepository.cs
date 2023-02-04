using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetDevPack.Data;
using Play.Domain.Edi.Models;

namespace Play.Domain.Edi.Interfaces;

public interface IEdiProfileRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<EdiProfile> GetByIdAsync(Guid id);
    Task<IEnumerable<EdiProfile>> GetAllAsync(int page = 1, int pageSize = 10);
    Task<IEnumerable<EdiProfile>> GetAllByCustomerIdAsync(Guid customerId, int page = 1, int pageSize = 10);
    Task<EdiProfile> GetByModelIdAsync(Guid modelId);
    Task<IEnumerable<EdiProfile>> GetByUserIdAsync(Guid userId);

    /// <summary>
    ///     Get total count of records
    /// </summary>
    /// <returns> Total count of records </returns>
    Task<int> GetCountAsync();

    void Add(EdiProfile ediProfile);
    void Update(EdiProfile ediProfile);
    void Remove(EdiProfile ediProfile);
    void Dispose();
    void Flush();

    void RemoveAll();
}
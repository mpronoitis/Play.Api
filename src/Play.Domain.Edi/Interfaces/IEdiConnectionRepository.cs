using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetDevPack.Data;
using Play.Domain.Edi.Models;

namespace Play.Domain.Edi.Interfaces;

public interface IEdiConnectionRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<EdiConnection> GetByIdAsync(Guid id);
    Task<IEnumerable<EdiConnection>> GetByModelIdAsync(Guid modelId);
    Task<IEnumerable<EdiConnection>> GetByOrganizationIdAsync(Guid organizationId);
    Task<IEnumerable<EdiConnection>> GetByProfileIdAsync(Guid profileId);
    Task<IEnumerable<EdiConnection>> GetByCustomerIdAsync(Guid customerId);
    Task<IEnumerable<EdiConnection>> GetAllAsync(int page = 1, int pageSize = 10);
    Task<IEnumerable<EdiConnection>> GetAllByCustomerIdAsync(Guid customerId, int page = 1, int pageSize = 10);
    void Add(EdiConnection ediConnection);
    void Update(EdiConnection ediConnection);
    void Remove(EdiConnection ediConnection);

    /// <summary>
    ///     Get total count
    /// </summary>
    Task<int> GetCountAsync();

    void Dispose();
}
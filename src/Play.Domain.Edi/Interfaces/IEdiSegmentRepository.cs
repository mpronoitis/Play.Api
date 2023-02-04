using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetDevPack.Data;
using Play.Domain.Edi.Models;

namespace Play.Domain.Edi.Interfaces;

public interface IEdiSegmentRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<EdiSegment> GetByIdAsync(Guid id);
    Task<EdiSegment> GetByModelIdAsync(Guid modelId);
    Task<IEnumerable<EdiSegment>> GetAllAsync(int page = 1, int pageSize = 10);

    /// <summary>
    ///     Get total count of records
    /// </summary>
    /// <returns> Total count of records </returns>
    Task<int> GetCountAsync();

    void Add(EdiSegment model);
    void AddRange(IEnumerable<EdiSegment> models);
    void Update(EdiSegment model);
    void Delete(EdiSegment model);
    void DeleteRange(IEnumerable<EdiSegment> models);
    void Dispose();
    void Flush();

    void RemoveAll();
}
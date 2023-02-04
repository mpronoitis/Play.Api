using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetDevPack.Data;
using Play.Domain.Edi.Models;

namespace Play.Domain.Edi.Interfaces;

public interface IEdiModelRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<EdiModel> GetByIdAsync(Guid id);
    Task<IEnumerable<EdiModel>> GetAllAsync(int page = 1, int pageSize = 10);
    Task<EdiModel> GetByTitleAsync(string title);

    /// <summary>
    ///     Get total count of records
    /// </summary>
    /// <returns> Total count of records </returns>
    Task<int> GetTotalCountAsync();

    void Add(EdiModel ediModel);
    void Update(EdiModel ediModel);
    void Remove(EdiModel model);

    void Dispose();

    //flush tracked changes
    void Flush();
    void RemoveAll();
}
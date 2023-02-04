using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetDevPack.Data;
using Play.Domain.Edi.Models;

namespace Play.Domain.Edi.Interfaces;

public interface IEdiVariableRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<EdiVariable> GetByIdAsync(Guid id);
    Task<IEnumerable<EdiVariable>> GetByPlaceholderAsync(string placeholder);
    Task<IEnumerable<EdiVariable>> GetByTitleAsync(string title);
    Task<IEnumerable<EdiVariable>> GetAllAsync(int page = 1, int pageSize = 10);

    /// <summary>
    ///     Get count of all records
    /// </summary>
    /// <returns> Count of all records </returns>
    Task<int> CountAllAsync();

    void Register(EdiVariable EdiVariable);
    void Update(EdiVariable EdiVariable);
    void Remove(EdiVariable EdiVariable);
    void Dispose();
    void Flush();
    void RemoveAll();
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetDevPack.Data;
using Play.Domain.Edi.Models;

namespace Play.Domain.Edi.Interfaces;

public interface IEdiOrganizationRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<EdiOrganization> GetByIdAsync(Guid id);
    Task<EdiOrganization> GetByEmailAsync(string email);

    /// <summary>
    ///     GetAll function , allows for pagination
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<IEnumerable<EdiOrganization>> GetAllAsync(int page = 1, int pageSize = 10);

    void Add(EdiOrganization ediOrganization);
    void Update(EdiOrganization ediOrganization);
    void Remove(EdiOrganization ediOrganization);
    void Dispose();
    void Flush();
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetDevPack.Data;
using Play.Domain.Edi.Models;

namespace Play.Domain.Edi.Interfaces;

public interface IEdiDocumentRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<EdiDocument> GetByIdAsync(Guid id);
    Task<IEnumerable<EdiDocument>> GetByTitleAsync(string title);
    Task<IEnumerable<EdiDocument>> GetByHedentidAsync(string hedentid);
    Task<IEnumerable<EdiDocument>> GetByIsProcessedAsync(bool isProcessed);
    Task<IEnumerable<EdiDocument>> GetByIsProcessedAndCustomerAsync(bool isProcessed, Guid customer);
    Task<IEnumerable<EdiDocument>> GetByIsSentAsync(bool isSent);
    Task<IEnumerable<EdiDocument>> GetByIsSentAndCustomerIdAsync(bool isSent, Guid customerId);
    Task<IEnumerable<EdiDocument>> GetByCustomerIdAsync(Guid customerId);


    Task<IEnumerable<EdiDocument>> GetAllWithPaginationByCustomerIdAsync(Guid customerId, int page = 1,
        int pageSize = 10);

    Task<IEnumerable<EdiDocument>> GetAllWithPaginationAsync(int page = 1, int pageSize = 10);
    Task<IEnumerable<EdiDocument>> GetAllWithDateRangeAsync(DateTime startDate, DateTime endDate);

    Task<IEnumerable<EdiDocument>> GetAllWithDateRangeAndCustomerIdAsync(DateTime startDate, DateTime endDate,
        Guid customerId);
    
    //get all with no payloads and pagination
     Task<IEnumerable<EdiDocument>> GetAllWithNoPayloadsAsync(int page = 1, int pageSize = 10);

     Task<IEnumerable<EdiDocument>> GetAllWithNoPayloadsAndCustomerIdAsync(Guid customerId, int page = 1,
         int pageSize = 10);


    Task<int> GetTotalCountByCustomerIdAsync(Guid customerId);

    /// <summary>
    ///     Get total count by customer id and date range
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    Task<int> GetTotalCountByCustomerIdAndDateRangeAsync(Guid customerId, DateTime startDate,
        DateTime endDate);

    /// <summary>
    ///  Get all with no payload and date range
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    Task<IEnumerable<EdiDocument>> GetAllWithNoPayloadsAndDateRangeAsync(DateTime startDate, DateTime endDate);

    Task<IEnumerable<EdiDocument>> GetAllWithNoPayloadsAndDateRangeAndCustomerIdAsync(DateTime startDate,
        DateTime endDate, Guid customerId);
    //get total count
    Task<int> GetTotalCountAsync();
    void Register(EdiDocument ediDocument);
    void Update(EdiDocument ediDocument);
    void UpdateMultiple(IEnumerable<EdiDocument> ediDocuments);
    void Remove(EdiDocument ediDocument);

    void Flush();
}
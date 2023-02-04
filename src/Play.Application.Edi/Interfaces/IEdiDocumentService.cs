using FluentValidation.Results;
using Play.Application.Edi.ViewModels;

namespace Play.Application.Edi.Interfaces;

public interface IEdiDocumentService
{
    Task<EdiDocumentViewModel> GetByIdAsync(Guid id);

    Task<IEnumerable<EdiDocumentViewModel>> GetAllWithPaginationByCustomerIdAsync(Guid customerId,
        int page = 1, int pageSize = 10);

    Task<IEnumerable<EdiDocumentViewModel>> GetAllWithPaginationAsync(int page = 1, int pageSize = 10);
    Task<int> GetTotalCountByCustomerIdAsync(Guid customerId);
    Task<ValidationResult> Receive(EdiDocumentReceiverViewModel ediDocumentViewModel);
    Task<ValidationResult> Register(EdiDocumentViewModel ediDocumentViewModel);
    Task<ValidationResult> Update(EdiDocumentViewModel ediDocumentViewModel);
    Task<ValidationResult> Remove(Guid id);

   Task<IEnumerable<EdiDocumentViewModel>> GetAllWithNoPayloadsAndPaginationAsync(int page = 1,
        int pageSize = 10);

   Task<IEnumerable<EdiDocumentViewModel>> GetAllWithNoPayloadsAndPaginationByCustomerIdAsync(
       Guid customerId, int page = 1, int pageSize = 10);

   /// <summary>
    ///     Get total count
    /// </summary>
    Task<int> GetTotalCountAsync();

    /// <summary>
    ///     Get total count by customer id and date range
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    Task<int> GetTotalCountByCustomerIdAndDateRangeAsync(Guid customerId, DateTime startDate,
        DateTime endDate);

    void Dispose();
}
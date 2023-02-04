using FluentValidation.Results;
using Play.Application.Edi.ViewModels;

namespace Play.Application.Edi.Interfaces;

public interface IEdiSegmentService
{
    Task<EdiSegmentViewModel> GetById(Guid id);
    Task<IEnumerable<EdiSegmentViewModel>> GetAllAsync(int page = 1, int pageSize = 10);
    Task<ValidationResult> Register(EdiSegmentViewModel ediSegmentViewModel);
    Task<ValidationResult> Update(EdiSegmentViewModel ediSegmentViewModel);
    Task<ValidationResult> Remove(Guid id);

    /// <summary>
    ///     Get total count
    /// </summary>
    Task<int> GetTotalCount();

    void Dispose();
}
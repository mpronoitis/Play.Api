using FluentValidation.Results;
using Play.Application.Edi.ViewModels;

namespace Play.Application.Edi.Interfaces;

public interface IEdiConnectionService
{
    Task<EdiConnectionViewModel> GetById(Guid id);
    Task<IEnumerable<EdiConnectionViewModel>> GetByCustomerId(Guid customerId);
    Task<IEnumerable<EdiConnectionViewModel>> GetAll(int page = 1, int pageSize = 10);
    Task<IEnumerable<EdiConnectionViewModel>> GetAllByCustomerId(Guid customerId, int page = 1, int pageSize = 10);
    Task<ValidationResult> Add(EdiConnectionViewModel ediConnectionViewModel);
    Task<ValidationResult> Update(EdiConnectionViewModel ediConnectionViewModel);
    Task<ValidationResult> Remove(Guid id);

    /// <summary>
    ///     Get total count
    /// </summary>
    Task<int> GetTotalCount();

    void Dispose();
}
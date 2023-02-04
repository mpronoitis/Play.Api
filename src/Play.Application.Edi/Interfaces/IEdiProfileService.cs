using FluentValidation.Results;
using Play.Application.Edi.ViewModels;

namespace Play.Application.Edi.Interfaces;

public interface IEdiProfileService
{
    Task<EdiProfileViewModel> GetById(Guid id);
    Task<IEnumerable<EdiProfileViewModel>> GetAll(int page = 1, int pageSize = 10);
    Task<IEnumerable<EdiProfileViewModel>> GetAllByCustomerId(Guid customerId, int page = 1, int pageSize = 10);
    Task<IEnumerable<EdiProfileViewModel>> GetByModelId(Guid modelId);

    /// <summary>
    ///     Get total count
    /// </summary>
    Task<int> GetTotalCount();

    Task<ValidationResult> Register(EdiProfileViewModel ediProfile);
    Task<ValidationResult> Update(EdiProfileViewModel ediProfile);
    Task<ValidationResult> Remove(Guid id);
    void Dispose();
}
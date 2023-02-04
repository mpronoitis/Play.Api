using FluentValidation.Results;
using Play.Application.Edi.ViewModels;

namespace Play.Application.Edi.Interfaces;

public interface IEdiModelService
{
    Task<EdiModelViewModel> GetById(Guid id);
    Task<EdiModelViewModel> GetByTitle(string Title);
    Task<IEnumerable<EdiModelViewModel>> GetAll(int page = 1, int pageSize = 10);
    Task<ValidationResult> Register(EdiModelViewModel ediModel);
    Task<ValidationResult> Update(EdiModelViewModel ediModel);
    Task<ValidationResult> Remove(Guid id);

    /// <summary>
    ///     Get total count of EdiModels
    /// </summary>
    Task<int> GetCount();

    void Dispose();
}
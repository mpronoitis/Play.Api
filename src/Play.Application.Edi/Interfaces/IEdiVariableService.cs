using FluentValidation.Results;
using Play.Application.Edi.ViewModels;

namespace Play.Application.Edi.Interfaces;

public interface IEdiVariableService
{
    Task<EdiVariableViewModel> GetByIdAsync(Guid id);
    Task<IEnumerable<EdiVariableViewModel>> GetAllAsync(int page = 1, int pageSize = 10);
    Task<ValidationResult> RegisterAsync(EdiVariableViewModel ediVariableViewModel);
    Task<ValidationResult> UpdateAsync(EdiVariableViewModel ediVariableViewModel);
    Task<ValidationResult> RemoveAsync(Guid id);

    /// <summary>
    ///     Get total count
    /// </summary>
    Task<int> GetTotalCount();

    void Dispose();
}
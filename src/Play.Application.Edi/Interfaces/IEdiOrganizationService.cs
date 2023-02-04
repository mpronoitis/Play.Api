using FluentValidation.Results;
using Play.Application.Edi.ViewModels;

namespace Play.Application.Edi.Interfaces;

public interface IEdiOrganizationService
{
    Task<EdiOrganizationViewModel> GetById(Guid id);
    Task<EdiOrganizationViewModel> GetByEmail(string email);
    Task<IEnumerable<EdiOrganizationViewModel>> GetAll(int page = 1, int pageSize = 10);
    Task<ValidationResult> Register(EdiOrganizationViewModel ediOrganizationViewModel);
    Task<ValidationResult> Update(EdiOrganizationViewModel ediOrganizationViewModel);
    Task<ValidationResult> Remove(Guid id);
    void Dispose();
}
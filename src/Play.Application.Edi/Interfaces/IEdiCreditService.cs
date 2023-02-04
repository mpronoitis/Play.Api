using FluentValidation.Results;
using Play.Application.Edi.ViewModels;
using Play.Domain.Edi.Models;

namespace Play.Application.Edi.Interfaces;

public interface IEdiCreditService
{
    Task<EdiCredit> GetEdiCreditById(Guid id);
    Task<EdiCredit> GetEdiCreditByCustomerId(Guid id);
    Task<IEnumerable<EdiCredit>> GetAllEdiCreditWithPaging(int page, int pageSize);
    Task<ValidationResult> RegisterEdiCredit(RegisterEdiCreditViewModel ediCredit);
    Task<ValidationResult> UpdateEdiCredit(UpdateEdiCreditViewModel ediCredit);
    Task<ValidationResult> RemoveEdiCredit(Guid id);
}
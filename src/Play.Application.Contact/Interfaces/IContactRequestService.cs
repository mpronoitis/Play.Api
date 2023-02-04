using FluentValidation.Results;
using Play.Application.Contact.ViewModels;

namespace Play.Application.Contact.Interfaces;

public interface IContactRequestService
{
    /// <summary>
    ///     Send Contact Request
    /// </summary>
    /// <param name="contactRequest">
    ///     <see cref="ContactRequestViewModel
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// </param>
    /// <returns></returns>
    Task<ValidationResult> SendContactRequest(ContactRequestViewModel contactRequest);
}
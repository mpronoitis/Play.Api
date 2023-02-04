using FluentValidation.Results;
using Play.Application.Epp.ViewModels;
using Play.Epp.Connector.Models.Contacts;

namespace Play.Application.Epp.Interfaces;

public interface IEppContactService
{
    /// <summary>
    ///     Create a new contact
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    Task<ValidationResult> CreateContactAsync(RegisterEppContactViewModel contact);

    /// <summary>
    ///     Update an existing contact
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    Task<ValidationResult> UpdateContactAsync(EPPContact contact);

    /// <summary>
    ///     Check if a contact is available for registration
    /// </summary>
    /// <param name="contactId"></param>
    /// <returns></returns>
    Task<bool> CheckContactAvailabilityAsync(string contactId);

    /// <summary>
    ///     Suggest an available contact id
    /// </summary>
    /// <returns></returns>
    Task<string> SuggestContactIdAsync();
}
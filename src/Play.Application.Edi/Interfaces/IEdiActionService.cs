using FluentValidation.Results;

namespace Play.Application.Edi.Interfaces;

public interface IEdiActionService
{
    /// <summary>
    ///     Function to build all un built edi documents for a given customer id
    /// </summary>
    /// <param name="customerId">Customer Id</param>
    Task<ValidationResult> BuildEdiDocuments(Guid customerId);

    /// <summary>
    ///     Function to send all un sent edi documents for a given customer id
    /// </summary>
    /// <param name="customerId">Customer Id</param>
    Task<ValidationResult> SendEdiDocuments(Guid customerId);
}
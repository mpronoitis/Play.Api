using FluentValidation.Results;
using Play.Application.Contracting.ViewModels;
using Play.Domain.Contracting.Models;

namespace Play.Application.Contracting.Interfaces;

public interface IContractService
{
    /// <summary>
    ///     Get all contracts with paging
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    Task<IEnumerable<Contract>> GetContractsAsync(int page, int pageSize);

    /// <summary>
    ///     Create a new contract
    /// </summary>
    /// <param name="contractViewModel">ContractViewModel to be created</param>
    /// <returns></returns>
    Task<ValidationResult> CreateContractAsync(ContractViewModel contractViewModel);

    /// <summary>
    ///     Update a contract
    /// </summary>
    /// <param name="contractViewModel">ContractViewModel to be updated</param>
    /// <returns></returns>
    Task<ValidationResult> UpdateContractAsync(UpdateContractViewModel contractViewModel);

    /// <summary>
    ///     Delete a contract
    /// </summary>
    /// <param name="id">Contract id</param>
    /// <returns></returns>
    Task<ValidationResult> DeleteContractAsync(Guid id);
}
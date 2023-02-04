using AutoMapper;
using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Contracting.Interfaces;
using Play.Application.Contracting.ViewModels;
using Play.Domain.Contracting.Commands;
using Play.Domain.Contracting.Interfaces;
using Play.Domain.Contracting.Models;

namespace Play.Application.Contracting.Services;

public class ContractService : IContractService
{
    private readonly IContractRepository _contractRepository;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;

    public ContractService(IMapper mapper, IMediatorHandler mediatorHandler, IContractRepository contractRepository)
    {
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
        _contractRepository = contractRepository;
    }

    /// <summary>
    ///     Get all contracts with paging
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    public async Task<IEnumerable<Contract>> GetContractsAsync(int page, int pageSize)
    {
        return await _contractRepository.GetAll(page, pageSize);
    }

    /// <summary>
    ///     Create a new contract
    /// </summary>
    /// <param name="contractViewModel">ContractViewModel to be created</param>
    /// <returns></returns>
    public async Task<ValidationResult> CreateContractAsync(ContractViewModel contractViewModel)
    {
        var contract = _mapper.Map<RegisterContractCommand>(contractViewModel);
        return await _mediatorHandler.SendCommand(contract);
    }

    /// <summary>
    ///     Update a contract
    /// </summary>
    /// <param name="contractViewModel">ContractViewModel to be updated</param>
    /// <returns></returns>
    public async Task<ValidationResult> UpdateContractAsync(UpdateContractViewModel contractViewModel)
    {
        var contract = _mapper.Map<UpdateContractCommand>(contractViewModel);
        return await _mediatorHandler.SendCommand(contract);
    }

    /// <summary>
    ///     Delete a contract
    /// </summary>
    /// <param name="id">Contract id</param>
    /// <returns></returns>
    public async Task<ValidationResult> DeleteContractAsync(Guid id)
    {
        var contract = new RemoveContractCommand(id);
        return await _mediatorHandler.SendCommand(contract);
    }
}
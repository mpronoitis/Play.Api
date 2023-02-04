using AutoMapper;
using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Edi.Interfaces;
using Play.Application.Edi.ViewModels;
using Play.Domain.Edi.Commands;
using Play.Domain.Edi.Interfaces;

namespace Play.Application.Edi.Services;

public class EdiVariableService : IEdiVariableService
{
    private readonly IEdiVariableRepository _ediVariableRepository;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;

    public EdiVariableService(IEdiVariableRepository ediVariableRepository, IMapper mapper,
        IMediatorHandler mediatorHandler)
    {
        _ediVariableRepository = ediVariableRepository;
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<EdiVariableViewModel> GetByIdAsync(Guid id)
    {
        var ediVariable = await _ediVariableRepository.GetByIdAsync(id);
        return _mapper.Map<EdiVariableViewModel>(ediVariable);
    }

    //get all with paging
    public async Task<IEnumerable<EdiVariableViewModel>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        var ediVariables = await _ediVariableRepository.GetAllAsync(page, pageSize);
        return _mapper.Map<IEnumerable<EdiVariableViewModel>>(ediVariables);
    }

    //commands
    public async Task<ValidationResult> RegisterAsync(EdiVariableViewModel ediVariableViewModel)
    {
        var registerCommand = _mapper.Map<RegisterEdiVariableCommand>(ediVariableViewModel);
        return await _mediatorHandler.SendCommand(registerCommand);
    }

    public async Task<ValidationResult> UpdateAsync(EdiVariableViewModel ediVariableViewModel)
    {
        var updateCommand = _mapper.Map<UpdateEdiVariableCommand>(ediVariableViewModel);
        return await _mediatorHandler.SendCommand(updateCommand);
    }

    public async Task<ValidationResult> RemoveAsync(Guid id)
    {
        var removeCommand = new RemoveEdiVariableCommand(id);
        return await _mediatorHandler.SendCommand(removeCommand);
    }

    /// <summary>
    ///     Get total count
    /// </summary>
    public async Task<int> GetTotalCount()
    {
        return await _ediVariableRepository.CountAllAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
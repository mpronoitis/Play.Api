using AutoMapper;
using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Edi.Interfaces;
using Play.Application.Edi.ViewModels;
using Play.Domain.Edi.Commands;
using Play.Domain.Edi.Interfaces;

namespace Play.Application.Edi.Services;

public class EdiConnectionService : IEdiConnectionService
{
    private readonly IEdiConnectionRepository _ediConnectionRepository;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;

    public EdiConnectionService(IEdiConnectionRepository ediConnectionRepository, IMapper mapper,
        IMediatorHandler mediatorHandler)
    {
        _ediConnectionRepository = ediConnectionRepository;
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
    }

    //get by id
    public async Task<EdiConnectionViewModel> GetById(Guid id)
    {
        var ediConnection = await _ediConnectionRepository.GetByIdAsync(id);
        return _mapper.Map<EdiConnectionViewModel>(ediConnection);
    }

    //get by customer id
    public async Task<IEnumerable<EdiConnectionViewModel>> GetByCustomerId(Guid customerId)
    {
        var ediConnections = await _ediConnectionRepository.GetByCustomerIdAsync(customerId);
        return _mapper.Map<IEnumerable<EdiConnectionViewModel>>(ediConnections);
    }

    //get all with pagination
    public async Task<IEnumerable<EdiConnectionViewModel>> GetAll(int page = 1, int pageSize = 10)
    {
        var ediConnections = await _ediConnectionRepository.GetAllAsync(page, pageSize);
        return _mapper.Map<IEnumerable<EdiConnectionViewModel>>(ediConnections);
    }

    //get all by customer id with pagination
    public async Task<IEnumerable<EdiConnectionViewModel>> GetAllByCustomerId(Guid customerId, int page = 1,
        int pageSize = 10)
    {
        var ediConnections = await _ediConnectionRepository.GetAllByCustomerIdAsync(customerId, page, pageSize);
        return _mapper.Map<IEnumerable<EdiConnectionViewModel>>(ediConnections);
    }

    //commands
    public async Task<ValidationResult> Add(EdiConnectionViewModel ediConnectionViewModel)
    {
        var registerCommand = _mapper.Map<RegisterEdiConnectionCommand>(ediConnectionViewModel);
        return await _mediatorHandler.SendCommand(registerCommand);
    }

    public async Task<ValidationResult> Update(EdiConnectionViewModel ediConnectionViewModel)
    {
        var updateCommand = _mapper.Map<UpdateEdiConnectionCommand>(ediConnectionViewModel);
        return await _mediatorHandler.SendCommand(updateCommand);
    }

    public async Task<ValidationResult> Remove(Guid id)
    {
        var removeCommand = new RemoveEdiConnectionCommand(id);
        return await _mediatorHandler.SendCommand(removeCommand);
    }

    /// <summary>
    ///     Get total count
    /// </summary>
    public async Task<int> GetTotalCount()
    {
        return await _ediConnectionRepository.GetCountAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
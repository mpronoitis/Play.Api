using AutoMapper;
using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Edi.Interfaces;
using Play.Application.Edi.ViewModels;
using Play.Domain.Edi.Commands;
using Play.Domain.Edi.Interfaces;

namespace Play.Application.Edi.Services;

public class EdiModelService : IEdiModelService
{
    private readonly IEdiModelRepository _ediModelRepository;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediator;

    public EdiModelService(IEdiModelRepository ediModelRepository, IMapper mapper, IMediatorHandler mediator)
    {
        _ediModelRepository = ediModelRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<EdiModelViewModel> GetById(Guid id)
    {
        var ediModel = await _ediModelRepository.GetByIdAsync(id);
        return _mapper.Map<EdiModelViewModel>(ediModel);
    }

    public async Task<EdiModelViewModel> GetByTitle(string Title)
    {
        var ediModel = await _ediModelRepository.GetByTitleAsync(Title);
        return _mapper.Map<EdiModelViewModel>(ediModel);
    }

    public async Task<IEnumerable<EdiModelViewModel>> GetAll(int page = 1, int pageSize = 10)
    {
        var ediModels = await _ediModelRepository.GetAllAsync(page, pageSize);
        return _mapper.Map<IEnumerable<EdiModelViewModel>>(ediModels);
    }

    //commands
    public async Task<ValidationResult> Register(EdiModelViewModel ediModel)
    {
        var registerCommand = _mapper.Map<RegisterEdiModelCommand>(ediModel);
        return await _mediator.SendCommand(registerCommand);
    }

    public async Task<ValidationResult> Update(EdiModelViewModel ediModel)
    {
        var updateCommand = _mapper.Map<UpdateEdiModelCommand>(ediModel);
        return await _mediator.SendCommand(updateCommand);
    }

    public async Task<ValidationResult> Remove(Guid id)
    {
        var removeCommand = new RemoveEdiModelCommand(id);
        return await _mediator.SendCommand(removeCommand);
    }

    /// <summary>
    ///     Get total count of EdiModels
    /// </summary>
    public async Task<int> GetCount()
    {
        return await _ediModelRepository.GetTotalCountAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
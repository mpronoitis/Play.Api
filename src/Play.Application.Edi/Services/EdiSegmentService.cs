using AutoMapper;
using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Edi.Interfaces;
using Play.Application.Edi.ViewModels;
using Play.Domain.Edi.Commands;
using Play.Domain.Edi.Interfaces;

namespace Play.Application.Edi.Services;

public class EdiSegmentService : IEdiSegmentService
{
    private readonly IEdiSegmentRepository _ediSegmentRepository;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;

    public EdiSegmentService(IEdiSegmentRepository ediSegmentRepository, IMapper mapper,
        IMediatorHandler mediatorHandler)
    {
        _ediSegmentRepository = ediSegmentRepository;
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<EdiSegmentViewModel> GetById(Guid id)
    {
        var ediSegment = await _ediSegmentRepository.GetByIdAsync(id);
        return _mapper.Map<EdiSegmentViewModel>(ediSegment);
    }

    //get all async with paging
    public async Task<IEnumerable<EdiSegmentViewModel>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        var ediSegments = await _ediSegmentRepository.GetAllAsync(page, pageSize);
        return _mapper.Map<IEnumerable<EdiSegmentViewModel>>(ediSegments);
    }

    //commands
    public async Task<ValidationResult> Register(EdiSegmentViewModel ediSegmentViewModel)
    {
        var registerCommand = _mapper.Map<RegisterEdiSegmentCommand>(ediSegmentViewModel);
        return await _mediatorHandler.SendCommand(registerCommand);
    }

    public async Task<ValidationResult> Update(EdiSegmentViewModel ediSegmentViewModel)
    {
        var updateCommand = _mapper.Map<UpdateEdiSegmentCommand>(ediSegmentViewModel);
        return await _mediatorHandler.SendCommand(updateCommand);
    }

    public async Task<ValidationResult> Remove(Guid id)
    {
        var removeCommand = new RemoveEdiSegmentCommand(id);
        return await _mediatorHandler.SendCommand(removeCommand);
    }

    /// <summary>
    ///     Get total count
    /// </summary>
    public async Task<int> GetTotalCount()
    {
        return await _ediSegmentRepository.GetCountAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
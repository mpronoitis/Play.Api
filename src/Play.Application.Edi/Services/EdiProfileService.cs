using AutoMapper;
using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Edi.Interfaces;
using Play.Application.Edi.ViewModels;
using Play.Domain.Edi.Commands;
using Play.Domain.Edi.Interfaces;

namespace Play.Application.Edi.Services;

public class EdiProfileService : IEdiProfileService
{
    private readonly IEdiProfileRepository _ediProfileRepository;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;

    public EdiProfileService(IEdiProfileRepository ediProfileRepository, IMapper mapper,
        IMediatorHandler mediatorHandler)
    {
        _ediProfileRepository = ediProfileRepository;
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
    }

    //get by id
    public async Task<EdiProfileViewModel> GetById(Guid id)
    {
        var ediProfile = await _ediProfileRepository.GetByIdAsync(id);
        return _mapper.Map<EdiProfileViewModel>(ediProfile);
    }

    //get all with pagination
    public async Task<IEnumerable<EdiProfileViewModel>> GetAll(int page = 1, int pageSize = 10)
    {
        var ediProfiles = await _ediProfileRepository.GetAllAsync(page, pageSize);
        return _mapper.Map<IEnumerable<EdiProfileViewModel>>(ediProfiles);
    }

    //get all with pagination by customer id
    public async Task<IEnumerable<EdiProfileViewModel>> GetAllByCustomerId(Guid customerId, int page = 1,
        int pageSize = 10)
    {
        var ediProfiles = await _ediProfileRepository.GetAllByCustomerIdAsync(customerId, page, pageSize);
        return _mapper.Map<IEnumerable<EdiProfileViewModel>>(ediProfiles);
    }

    //get by model id
    public async Task<IEnumerable<EdiProfileViewModel>> GetByModelId(Guid modelId)
    {
        var ediProfiles = await _ediProfileRepository.GetByModelIdAsync(modelId);
        return _mapper.Map<IEnumerable<EdiProfileViewModel>>(ediProfiles);
    }

    //commands
    public async Task<ValidationResult> Register(EdiProfileViewModel ediProfile)
    {
        var registerCommand = _mapper.Map<RegisterEdiProfileCommand>(ediProfile);
        return await _mediatorHandler.SendCommand(registerCommand);
    }

    public async Task<ValidationResult> Update(EdiProfileViewModel ediProfile)
    {
        var updateCommand = _mapper.Map<UpdateEdiProfileCommand>(ediProfile);
        return await _mediatorHandler.SendCommand(updateCommand);
    }

    public async Task<ValidationResult> Remove(Guid id)
    {
        var removeCommand = new RemoveEdiProfileCommand(id);
        return await _mediatorHandler.SendCommand(removeCommand);
    }

    /// <summary>
    ///     Get total count
    /// </summary>
    public async Task<int> GetTotalCount()
    {
        return await _ediProfileRepository.GetCountAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
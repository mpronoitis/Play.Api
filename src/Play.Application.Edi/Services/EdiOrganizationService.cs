using AutoMapper;
using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Edi.Interfaces;
using Play.Application.Edi.ViewModels;
using Play.Domain.Edi.Commands;
using Play.Domain.Edi.Interfaces;

namespace Play.Application.Edi.Services;

public class EdiOrganizationService : IEdiOrganizationService
{
    private readonly IEdiOrganizationRepository _ediOrganizationRepository;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediator;

    public EdiOrganizationService(IMapper mapper, IEdiOrganizationRepository ediOrganizationRepository,
        IMediatorHandler mediator)
    {
        _mapper = mapper;
        _ediOrganizationRepository = ediOrganizationRepository;
        _mediator = mediator;
    }

    public async Task<EdiOrganizationViewModel> GetById(Guid id)
    {
        var ediOrganization = await _ediOrganizationRepository.GetByIdAsync(id);
        return _mapper.Map<EdiOrganizationViewModel>(ediOrganization);
    }

    public async Task<EdiOrganizationViewModel> GetByEmail(string email)
    {
        var ediOrganization = await _ediOrganizationRepository.GetByEmailAsync(email);
        return _mapper.Map<EdiOrganizationViewModel>(ediOrganization);
    }

    public async Task<IEnumerable<EdiOrganizationViewModel>> GetAll(int page = 1, int pageSize = 10)
    {
        var ediOrganizations = await _ediOrganizationRepository.GetAllAsync(page, pageSize);
        return _mapper.Map<IEnumerable<EdiOrganizationViewModel>>(ediOrganizations);
    }

    // COMMANDS

    public async Task<ValidationResult> Register(EdiOrganizationViewModel ediOrganizationViewModel)
    {
        var registerCommand = _mapper.Map<RegisterEdiOrganizationCommand>(ediOrganizationViewModel);
        return await _mediator.SendCommand(registerCommand);
    }

    public async Task<ValidationResult> Update(EdiOrganizationViewModel ediOrganizationViewModel)
    {
        var updateCommand = _mapper.Map<UpdateEdiOrganizationCommand>(ediOrganizationViewModel);
        return await _mediator.SendCommand(updateCommand);
    }

    public async Task<ValidationResult> Remove(Guid id)
    {
        var removeCommand = new RemoveEdiOrganizationCommand(id);
        return await _mediator.SendCommand(removeCommand);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
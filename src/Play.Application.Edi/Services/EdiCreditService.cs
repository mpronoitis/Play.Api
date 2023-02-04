using AutoMapper;
using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Edi.Interfaces;
using Play.Application.Edi.ViewModels;
using Play.Domain.Edi.Commands;
using Play.Domain.Edi.Interfaces;
using Play.Domain.Edi.Models;

namespace Play.Application.Edi.Services;

public class EdiCreditService : IEdiCreditService
{
    private readonly IEdiCreditRepository _ediCreditRepository;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediator;

    public EdiCreditService(IEdiCreditRepository ediCreditRepository, IMapper mapper, IMediatorHandler mediator)
    {
        _ediCreditRepository = ediCreditRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<EdiCredit> GetEdiCreditById(Guid id)
    {
        return await _ediCreditRepository.GetByCreditIdAsync(id);
    }

    public async Task<EdiCredit> GetEdiCreditByCustomerId(Guid id)
    {
        return await _ediCreditRepository.GetByCustomerIdAsync(id);
    }

    public async Task<IEnumerable<EdiCredit>> GetAllEdiCreditWithPaging(int page, int pageSize)
    {
        return await _ediCreditRepository.GetAllWithPagingAsync(page, pageSize);
    }

    public async Task<ValidationResult> RegisterEdiCredit(RegisterEdiCreditViewModel ediCredit)
    {
        var registerCommand = _mapper.Map<RegisterEdiCreditCommand>(ediCredit);
        return await _mediator.SendCommand(registerCommand);
    }

    public async Task<ValidationResult> UpdateEdiCredit(UpdateEdiCreditViewModel ediCredit)
    {
        var updateCommand = _mapper.Map<UpdateEdiCreditCommand>(ediCredit);
        return await _mediator.SendCommand(updateCommand);
    }

    public async Task<ValidationResult> RemoveEdiCredit(Guid id)
    {
        var removeCommand = new RemoveEdiCreditCommand(id);
        return await _mediator.SendCommand(removeCommand);
    }
}
using AutoMapper;
using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Contact.Interfaces;
using Play.Application.Contact.ViewModels;
using Play.Domain.Contact.Commands;

namespace Play.Application.Contact.Services;

public class ContactRequestService : IContactRequestService
{
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;

    public ContactRequestService(IMediatorHandler mediatorHandler, IMapper mapper)
    {
        _mediatorHandler = mediatorHandler;
        _mapper = mapper;
    }

    /// <summary>
    ///     Send Contact Request
    /// </summary>
    /// <param name="contactRequest">
    ///     <see cref="ContactRequestViewModel
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// </param>
    /// <returns></returns>
    public async Task<ValidationResult> SendContactRequest(ContactRequestViewModel contactRequest)
    {
        var command = _mapper.Map<RegisterContactRequestCommand>(contactRequest);
        return await _mediatorHandler.SendCommand(command);
    }
}
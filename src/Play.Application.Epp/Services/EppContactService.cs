using AutoMapper;
using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Epp.Interfaces;
using Play.Application.Epp.ViewModels;
using Play.Domain.Epp.Commands;
using Play.Epp.Connector.Interfaces;
using Play.Epp.Connector.Models.Contacts;

namespace Play.Application.Epp.Services;

public class EppContactService : IEppContactService
{
    private readonly IEppConnector _eppConnector;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;

    public EppContactService(IMediatorHandler mediatorHandler, IMapper mapper, IEppConnector eppConnector)
    {
        _mediatorHandler = mediatorHandler;
        _mapper = mapper;
        _eppConnector = eppConnector;
    }

    /// <summary>
    ///     Create a new contact
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    public async Task<ValidationResult> CreateContactAsync(RegisterEppContactViewModel contact)
    {
        var command = _mapper.Map<RegisterEppContactCommand>(contact);
        return await _mediatorHandler.SendCommand(command);
    }

    /// <summary>
    ///     Update an existing contact
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    public async Task<ValidationResult> UpdateContactAsync(EPPContact contact)
    {
        var command = new UpdateEppContactCommand(contact);
        return await _mediatorHandler.SendCommand(command);
    }

    /// <summary>
    ///     Check if a contact is available for registration
    /// </summary>
    /// <param name="contactId"></param>
    /// <returns></returns>
    public async Task<bool> CheckContactAvailabilityAsync(string contactId)
    {
        await _eppConnector.Login();
        return await _eppConnector.CheckContact(contactId);
    }

    /// <summary>
    ///     Suggest an available contact id
    /// </summary>
    /// <returns></returns>
    public async Task<string> SuggestContactIdAsync()
    {
        await _eppConnector.Login();
        //a valid contact_id can have a maximum of 16 characters must start with b68_ and can use chars from [a-z],[0-9],[A-Z]
        var contactId = $"b68_{Guid.NewGuid().ToString()[..8]}";
        while (!await _eppConnector.CheckContact(contactId)) contactId = $"b68_{Guid.NewGuid().ToString()[..8]}";
        return contactId;
    }
}
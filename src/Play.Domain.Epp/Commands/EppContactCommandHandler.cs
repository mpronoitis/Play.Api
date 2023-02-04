using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;
using Play.Epp.Connector.Interfaces;

namespace Play.Domain.Epp.Commands;

public class EppContactCommandHandler : CommandHandler, IRequestHandler<RegisterEppContactCommand, ValidationResult>,
    IRequestHandler<UpdateEppContactCommand, ValidationResult>
{
    private readonly IEppConnector _connector;
    private readonly ILogger<EppContactCommandHandler> _logger;

    public EppContactCommandHandler(IEppConnector connector, ILogger<EppContactCommandHandler> logger)
    {
        _connector = connector;
        _logger = logger;
    }


    public async Task<ValidationResult> Handle(RegisterEppContactCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            await _connector.Login();
            var res = await _connector.CreateContact(request.EPPContact);
            if (!res) AddError("Error creating contact");
            return ValidationResult;
        }
        catch (Exception ex)
        {
            //log error
            _logger.LogError(ex,"Error while executing command for request {request}",request);
            AddError(ex.Message);
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(UpdateEppContactCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            await _connector.Login();
            await _connector.UpdateContact(request.EPPContact);
            return request.ValidationResult;
        }
        catch (Exception ex)
        {
            //log error
            _logger.LogError(ex,"Error while executing command for request {request}",request);
            AddError(ex.Message);
            return ValidationResult;
        }
    }
}
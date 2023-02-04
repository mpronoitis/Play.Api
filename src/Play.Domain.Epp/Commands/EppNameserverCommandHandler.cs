using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;
using Play.Epp.Connector.Interfaces;

namespace Play.Domain.Epp.Commands;

public class EppNameserverCommandHandler : CommandHandler,
    IRequestHandler<RegisterEppNameserverCommand, ValidationResult>,
    IRequestHandler<RemoveAllEppNameserversCommand, ValidationResult>,
    IRequestHandler<RegisterListEppNameserversCommand, ValidationResult>
{
    private readonly IEppConnector _connector;
    private readonly ILogger<EppNameserverCommandHandler> _logger;

    public EppNameserverCommandHandler(IEppConnector connector, ILogger<EppNameserverCommandHandler> logger)
    {
        _connector = connector;
        _logger = logger;
    }


    public async Task<ValidationResult> Handle(RegisterEppNameserverCommand request,
        CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            await _connector.Login();
            //check if nameserver exists in epp registry
            var ns = await _connector.CheckHost(request.Nameserver);
            //if it doesn't exist we need to register it so it can be added to a domain
            if (ns) await _connector.CreateHost(request.Nameserver);
            //add the nameserver to the domain
            //get domain info check if the nameserver is already added
            var domain = await _connector.GetDomainInfo(request.DomainName);
            if (domain.Nameservers.Any(x => x == request.Nameserver)) return ValidationResult;
            await _connector.AddNameserverToDomain(request.DomainName, request.Nameserver);

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

    public async Task<ValidationResult> Handle(RegisterListEppNameserversCommand request,
        CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            //clear all nameservers from domain
            await _connector.Login();
            var domainInfo = await _connector.GetDomainInfo(request.DomainName);
            if (domainInfo.Nameservers.Length > 0)
                foreach (var nameserver in domainInfo.Nameservers)
                    await _connector.RemoveNameserverFromDomain(request.DomainName, nameserver);

            //loop through the list of nameservers and register them
            foreach (var nameserver in request.Nameservers)
            {
                //check if nameserver exists in epp registry
                var ns = await _connector.CheckHost(nameserver);
                //if it doesn't exist we need to register it so it can be added to a domain
                if (ns) await _connector.CreateHost(nameserver);
                //add the nameserver to the domain
                await _connector.AddNameserverToDomain(request.DomainName, nameserver);
            }

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

    public async Task<ValidationResult> Handle(RemoveAllEppNameserversCommand request,
        CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            await _connector.Login();
            //get domain info
            var domainInfo = await _connector.GetDomainInfo(request.DomainName);

            //if nameservers array is empty return
            if (domainInfo.Nameservers.Length == 0) return ValidationResult;

            //remove all nameservers from domain
            foreach (var nameserver in domainInfo.Nameservers)
                await _connector.RemoveNameserverFromDomain(request.DomainName, nameserver);

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
}
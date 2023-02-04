using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using NetDevPack.Messaging;
using Play.Epp.Connector.Interfaces;

namespace Play.Domain.Epp.Commands;

public class EppDomainCommandHandler : CommandHandler, IRequestHandler<RegisterEppDomainCommand, ValidationResult>,
    IRequestHandler<TransferEppDomainCommand, ValidationResult>,
    IRequestHandler<RenewEppDomainCommand, ValidationResult>
{
    private readonly IEppConnector _connector;
    private readonly ILogger<EppDomainCommandHandler> _logger;

    public EppDomainCommandHandler(IEppConnector connector,ILogger<EppDomainCommandHandler> logger)
    {
        _connector = connector;
        _logger = logger;

    }

    public async Task<ValidationResult> Handle(RegisterEppDomainCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            await _connector.Login();
            await _connector.CreateDomain(request.RegisterDomainModel.DomainName,
                request.RegisterDomainModel.Registrant, request.RegisterDomainModel.Admin,
                request.RegisterDomainModel.Tech, request.RegisterDomainModel.Billing,
                request.RegisterDomainModel.Password, request.RegisterDomainModel.Period);
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

    public async Task<ValidationResult> Handle(RenewEppDomainCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        try
        {
            await _connector.Login();
            //get domain info
            var domainInfo = await _connector.GetDomainInfo(request.RenewDomainModel.DomainName);
            //if expiration date is empty or the contact does not start with b68_ then return error
            if (string.IsNullOrEmpty(domainInfo.ExDate) || !domainInfo.Registrant.StartsWith("b68_"))
            {
                AddError("Domain is not registered or not registered with this registrar");
                return ValidationResult;
            }

            //convert ExDate from ISO to yyyy-MM-dd minus a day , cause I DONT KNOW WHY
            var exDate = DateTime.Parse(domainInfo.ExDate).AddDays(-1).ToString("yyyy-MM-dd");
            //renew domain
            await _connector.RenewDomain(request.RenewDomainModel.DomainName, exDate,
                request.RenewDomainModel.Years);

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

    public async Task<ValidationResult> Handle(TransferEppDomainCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;
        try
        {
            await _connector.Login();
             await _connector.TransferDomain(request.TransferDomainModel.DomainName,
                request.TransferDomainModel.Password, request.TransferDomainModel.ContactId,
                request.TransferDomainModel.NewPassword);
            
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
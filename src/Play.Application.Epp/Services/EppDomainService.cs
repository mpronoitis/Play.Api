using AutoMapper;
using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Epp.Interfaces;
using Play.Application.Epp.ViewModels;
using Play.Domain.Epp.Commands;
using Play.Epp.Connector.Interfaces;
using Play.Epp.Connector.Models.Domains;

namespace Play.Application.Epp.Services;

public class EppDomainService : IEppDomainService
{
    private readonly IEppConnector _eppConnector;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;

    public EppDomainService(IEppConnector eppConnector, IMapper mapper, IMediatorHandler mediatorHandler)
    {
        _eppConnector = eppConnector;
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
    }

    /// <summary>
    ///     Service to check a domain availability
    /// </summary>
    /// <param name="domainName">Domain name to check</param>
    /// <returns>True if domain is available, false otherwise</returns>
    public async Task<bool> CheckDomainAvailability(string domainName)
    {
        await _eppConnector.Login();
        var domainCheckResponse = await _eppConnector.CheckDomain(domainName);

        return domainCheckResponse;
    }

    /// <summary>
    ///     Get information about a domain
    /// </summary>
    /// <param name="domainName">Domain name to get information about</param>
    /// <returns>Domain information</returns>
    public async Task<EPPDomainInfo> GetDomainInfo(string domainName)
    {
        await _eppConnector.Login();
        var domainInfoResponse = await _eppConnector.GetDomainInfo(domainName);

        return domainInfoResponse;
    }

    /// <summary>
    ///     Register a new domain
    /// </summary>
    /// <param name="registerDomainViewModel">Domain information</param>
    /// <returns>Domain registration result</returns>
    public async Task<ValidationResult> RegisterDomain(RegisterEppDomainViewModel registerDomainViewModel)
    {
        var domainCreateRequest = _mapper.Map<RegisterEppDomainCommand>(registerDomainViewModel);
        return await _mediatorHandler.SendCommand(domainCreateRequest);
    }

    /// <summary>
    ///     Transfer a domain
    /// </summary>
    /// <param name="transferDomainViewModel">Domain information</param>
    /// <returns>Domain transfer result</returns>
    public async Task<ValidationResult> TransferDomain(TransferEppDomainViewModel transferDomainViewModel)
    {
        var domainTransferRequest = _mapper.Map<TransferEppDomainCommand>(transferDomainViewModel);
        return await _mediatorHandler.SendCommand(domainTransferRequest);
    }

    /// <summary>
    ///     Renew a domain
    /// </summary>
    /// <param name="renewDomainViewModel">Domain information</param>
    /// <returns>Domain renewal result</returns>
    public async Task<ValidationResult> RenewDomain(RenewEppDomainViewModel renewDomainViewModel)
    {
        var domainRenewRequest = _mapper.Map<RenewEppDomainCommand>(renewDomainViewModel);
        return await _mediatorHandler.SendCommand(domainRenewRequest);
    }

    /// <summary>
    ///     Add a nameserver to a domain
    /// </summary>
    /// <param name="addNameserverViewModel">Domain information</param>
    /// <returns>Domain nameserver addition result</returns>
    public async Task<ValidationResult> AddNameserver(RegisterEppNameserverViewModel addNameserverViewModel)
    {
        var domainAddNameserverRequest = _mapper.Map<RegisterEppNameserverCommand>(addNameserverViewModel);
        return await _mediatorHandler.SendCommand(domainAddNameserverRequest);
    }

    /// <summary>
    ///     Remove all nameservers from a domain
    /// </summary>
    /// <param name="domainName">Domain name</param>
    /// <returns>Domain nameserver removal result</returns>
    public async Task<ValidationResult> RemoveAllNameservers(string domainName)
    {
        var domainRemoveNameserverRequest = new RemoveAllEppNameserversCommand(domainName);
        return await _mediatorHandler.SendCommand(domainRemoveNameserverRequest);
    }

    /// <summary>
    ///     Add a list of nameservers to a domain
    /// </summary>
    /// <param name="addNameserversViewModel">Domain information</param>
    /// <returns>Domain nameserver addition result</returns>
    public async Task<ValidationResult> AddNameservers(RegisterEppNameserversViewModel addNameserversViewModel)
    {
        var domainAddNameserversRequest = _mapper.Map<RegisterListEppNameserversCommand>(addNameserversViewModel);
        return await _mediatorHandler.SendCommand(domainAddNameserversRequest);
    }
}
using FluentValidation.Results;
using Play.Application.Epp.ViewModels;
using Play.Epp.Connector.Models.Domains;

namespace Play.Application.Epp.Interfaces;

public interface IEppDomainService
{
    /// <summary>
    ///     Service to check a domain availability
    /// </summary>
    /// <param name="domainName">Domain name to check</param>
    /// <returns>True if domain is available, false otherwise</returns>
    Task<bool> CheckDomainAvailability(string domainName);

    /// <summary>
    ///     Get information about a domain
    /// </summary>
    /// <param name="domainName">Domain name to get information about</param>
    /// <returns>Domain information</returns>
    Task<EPPDomainInfo> GetDomainInfo(string domainName);

    /// <summary>
    ///     Register a new domain
    /// </summary>
    /// <param name="registerDomainViewModel">Domain information</param>
    /// <returns>Domain registration result</returns>
    Task<ValidationResult> RegisterDomain(RegisterEppDomainViewModel registerDomainViewModel);

    /// <summary>
    ///     Transfer a domain
    /// </summary>
    /// <param name="transferDomainViewModel">Domain information</param>
    /// <returns>Domain transfer result</returns>
    Task<ValidationResult> TransferDomain(TransferEppDomainViewModel transferDomainViewModel);

    /// <summary>
    ///     Renew a domain
    /// </summary>
    /// <param name="renewDomainViewModel">Domain information</param>
    /// <returns>Domain renewal result</returns>
    Task<ValidationResult> RenewDomain(RenewEppDomainViewModel renewDomainViewModel);

    /// <summary>
    ///     Add a nameserver to a domain
    /// </summary>
    /// <param name="addNameserverViewModel">Domain information</param>
    /// <returns>Domain nameserver addition result</returns>
    Task<ValidationResult> AddNameserver(RegisterEppNameserverViewModel addNameserverViewModel);

    /// <summary>
    ///     Remove all nameservers from a domain
    /// </summary>
    /// <param name="domainName">Domain name</param>
    /// <returns>Domain nameserver removal result</returns>
    Task<ValidationResult> RemoveAllNameservers(string domainName);

    /// <summary>
    ///     Add a list of nameservers to a domain
    /// </summary>
    /// <param name="addNameserversViewModel">Domain information</param>
    /// <returns>Domain nameserver addition result</returns>
    Task<ValidationResult> AddNameservers(RegisterEppNameserversViewModel addNameserversViewModel);
}
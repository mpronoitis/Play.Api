namespace Play.Application._20i.Interfaces;

public interface ITwentyDomainService
{
    /// <summary>
    ///     Get all domains owned on 20i
    /// </summary>
    /// <returns></returns>
    Task<string> GetDomains();

    /// <summary>
    ///     List all possible domains that are supported with the periods that are supported for registration
    /// </summary>
    Task<string> GetDomainPeriods();

    /// <summary>
    ///     Searches for one or more domain names.
    ///     If you provide a domain name, this will search for that name only,
    ///     otherwise it will search for that prefix on all supported TLDs.
    ///     You may supply multiple literal domain names if you separate them with commas.
    ///     You may supply arbitrary text, which will be stripped down to something suitable for domain search.
    /// </summary>
    /// <param name="domainName"></param>
    /// <returns></returns>
    Task<string> SearchDomain(string domainName);

    /// <summary>
    ///     Get total count of 20i domains
    /// </summary>
    /// <returns>The total count of domains</returns>
    Task<int> GetDomainCount();
}
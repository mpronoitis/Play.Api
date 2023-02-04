using Newtonsoft.Json;
using Play.Application._20i.Interfaces;
using Play.Domain._20i.Models;
using TwentyI_dotnet.Interfaces;

namespace Play.Application._20i.Services;

public class TwentyDomainService : ITwentyDomainService
{
    private readonly ITwentyIApi _twentyIApi;

    public TwentyDomainService(ITwentyIApi twentyIApi)
    {
        _twentyIApi = twentyIApi;
    }

    /// <summary>
    ///     Get all domains owned on 20i
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetDomains()
    {
        return await _twentyIApi.GetDomains();
    }

    /// <summary>
    ///     List all possible domains that are supported with the periods that are supported for registration
    /// </summary>
    public async Task<string> GetDomainPeriods()
    {
        return await _twentyIApi.GetDomainPeriods();
    }

    /// <summary>
    ///     Searches for one or more domain names.
    ///     If you provide a domain name, this will search for that name only,
    ///     otherwise it will search for that prefix on all supported TLDs.
    ///     You may supply multiple literal domain names if you separate them with commas.
    ///     You may supply arbitrary text, which will be stripped down to something suitable for domain search.
    /// </summary>
    /// <param name="domainName"></param>
    /// <returns></returns>
    public async Task<string> SearchDomain(string domainName)
    {
        return await _twentyIApi.GetDomainSearch(domainName);
    }

    /// <summary>
    ///     Get total count of 20i domains
    /// </summary>
    /// <returns>The total count of domains</returns>
    public async Task<int> GetDomainCount()
    {
        var domains = await _twentyIApi.GetDomains();
        var domainList = JsonConvert.DeserializeObject<List<TwentyDomainModel>>(domains);
        return domainList?.Count ?? 0;
    }
}
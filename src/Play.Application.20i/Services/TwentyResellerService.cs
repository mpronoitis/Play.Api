using Newtonsoft.Json;
using Play.Application._20i.Interfaces;
using TwentyI_dotnet.Interfaces;

namespace Play.Application._20i.Services;

public class TwentyResellerService : ITwentyResellerService
{
    private readonly ITwentyIApi _twentyIApi;

    public TwentyResellerService(ITwentyIApi twentyIApi)
    {
        _twentyIApi = twentyIApi;
    }

    /// <summary>
    ///     Get package type information.
    /// </summary>
    /// <returns>json string</returns>
    public async Task<string> GetPackageTypes()
    {
        var response = await _twentyIApi.ResellerPackageTypes("14500");
        return response;
    }

    /// <summary>
    ///     Renew a domain name.
    ///     This will charge the appropriate registration fee to your 20i Balance.
    ///     If you don't have enough left, this will fail.
    /// </summary>
    /// <param name="domainName">The domain name to renew.</param>
    /// <param name="period">The number of years to renew for.</param>
    /// <returns>json string</returns>
    public async Task<string> RenewDomain(string domainName, int period)
    {
        //create json body { name: "string", years: 1, renewPrivacy: true }
        var body = new
        {
            name = domainName,
            years = period,
            renewPrivacy = true
        };
        //convert to json string
        var jsonBody = JsonConvert.SerializeObject(body);
        var response = await _twentyIApi.ResellerRenewDomain("14500", jsonBody);
        return response;
    }

    /// <summary>
    ///     Returns your Stack user config.
    /// </summary>
    /// <returns>json string</returns>
    public async Task<string> GetStackUserConfig()
    {
        var response = await _twentyIApi.ResellerSusers("14500");
        return response;
    }
}
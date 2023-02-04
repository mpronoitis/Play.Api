using Play.Application.Epp.Interfaces;
using Play.Epp.Connector.Interfaces;
using Play.Epp.Connector.Models;

namespace Play.Application.Epp.Services;

public class EppAccountRegistrantService : IEppAccountRegistrantService
{
    private readonly IEppConnector _eppConnector;
   
    public EppAccountRegistrantService(IEppConnector eppConnector)
    {
        _eppConnector = eppConnector;
    }


    /// <summary>
    ///     Fetch Account Registrant Information
    /// </summary>
    /// <returns></returns>

    public async Task<AccountRegistrant> GetAccountRegistrantInfo()
    {
        await _eppConnector.Login();
        var accountRegistrantInfo = await _eppConnector.GetAccountRegistrantInfo();

        return accountRegistrantInfo;
    }

}
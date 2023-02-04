using Play.Epp.Connector.Models;

namespace Play.Application.Epp.Interfaces;

public interface IEppAccountRegistrantService
{
    /// <summary>
    /// Fetch Account Registrant Info
    /// </summary>
    /// <returns></returns>
    Task<AccountRegistrant> GetAccountRegistrantInfo();
}
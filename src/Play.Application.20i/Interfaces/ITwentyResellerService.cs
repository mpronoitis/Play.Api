namespace Play.Application._20i.Interfaces;

public interface ITwentyResellerService
{
    /// <summary>
    ///     Get package type information.
    /// </summary>
    /// <returns>json string</returns>
    Task<string> GetPackageTypes();

    /// <summary>
    ///     Renew a domain name.
    ///     This will charge the appropriate registration fee to your 20i Balance.
    ///     If you don't have enough left, this will fail.
    /// </summary>
    /// <param name="domainName">The domain name to renew.</param>
    /// <param name="period">The number of years to renew for.</param>
    /// <returns>json string</returns>
    Task<string> RenewDomain(string domainName, int period);

    /// <summary>
    ///     Returns your Stack user config.
    /// </summary>
    /// <returns>json string</returns>
    Task<string> GetStackUserConfig();
}
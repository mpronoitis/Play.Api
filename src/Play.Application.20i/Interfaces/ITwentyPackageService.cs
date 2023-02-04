namespace Play.Application._20i.Interfaces;

public interface ITwentyPackageService
{
    Task<string> GetPackages();
    Task<string> GetPackage(string id);
    Task<string> GetPackageBundleTypeLimits(string id);

    Task<string> GetPackagesWebLogs(string id);

    Task<string> GetCheckMalwareScan(string id);

    Task<string> GetStartMalwareScan(string id);

    Task<string> GetStartMassScan(List<string> ids);

    /// <summary>
    ///     Get total count of packages
    /// </summary>
    /// <returns> The total count of packages </returns>
    Task<int> GetPackagesCount();
}
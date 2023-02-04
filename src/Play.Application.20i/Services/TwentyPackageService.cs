using Newtonsoft.Json;
using Play.Application._20i.Interfaces;
using Play.Domain._20i.Models;
using TwentyI_dotnet.Interfaces;

namespace Play.Application._20i.Services;

public class TwentyPackageService : ITwentyPackageService
{
    private readonly ITwentyIApi _twentyIApi;

    public TwentyPackageService(ITwentyIApi twentyIApi)
    {
        _twentyIApi = twentyIApi;
    }

    public async Task<string> GetPackages()
    {
        return await _twentyIApi.Package();
    }

    public async Task<string> GetPackage(string id)
    {
        return await _twentyIApi.Package(id);
    }

    public async Task<string> GetPackageBundleTypeLimits(string id)
    {
        return await _twentyIApi.PackageBundleTypeLimits(id);
    }

    public async Task<string> GetPackagesWebLogs(string id)
    {
        return await _twentyIApi.PackageWebLogs(id);
    }

    public async Task<string> GetCheckMalwareScan(string id)
    {
        return await _twentyIApi.PackageWebMalwareScan(id);
    }

    public async Task<string> GetStartMalwareScan(string id)
    {
        var body = new //sample body: { "LockState":"new"}
        {
            LockState = "new"
        };
        //convert to json string
        var jsonBody = JsonConvert.SerializeObject(body);
        var response = await _twentyIApi.PackageWebMalwareScan(id, jsonBody);
        return response;
    }

    public async Task<string> GetStartMassScan(List<string> ids)
    {
        var body = new //sample body: { "LockState":"new"}
        {
            LockState = "new"
        };
        //convert to json string
        var jsonBody = JsonConvert.SerializeObject(body);
        //retrieve all ids and malwarese scan
        foreach (var id in ids) await _twentyIApi.PackageWebMalwareScan(id, jsonBody);

        return "Completed";
    }

    /// <summary>
    ///     Get total count of packages
    /// </summary>
    /// <returns> The total count of packages </returns>
    public async Task<int> GetPackagesCount()
    {
        var response = await _twentyIApi.Package();
        var packages = JsonConvert.DeserializeObject<List<TwentyPackageModel>>(response);
        return (packages?.Count).GetValueOrDefault();
    }
}
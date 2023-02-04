using Newtonsoft.Json;
using Play.Domain.Whmcs.Models;

namespace Play.Domain.Whmcs.ResponseModels;

public class WhmcsGetClientsResponseModelClients
{
    public List<WhmcsClient> client { get; set; } = null!;
}

public class WhmcsGetClientsResponseModel
{
    public WhmcsGetClientsResponseModelRoot whmcsapi { get; set; } = null!;
}

public class WhmcsGetClientsResponseModelRoot
{
    [JsonProperty("@version")] public string Version { get; set; } = null!;

    public string action { get; set; } = null!;
    public string result { get; set; } = null!;
    public string totalresults { get; set; } = null!;
    public string startnumber { get; set; } = null!;
    public string numreturned { get; set; } = null!;
    public WhmcsGetClientsResponseModelClients clients { get; set; } = null!;
}
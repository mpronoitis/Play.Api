using Newtonsoft.Json;
using Play.Domain.Whmcs.Models;

namespace Play.Domain.Whmcs.ResponseModels;

public class WhmcsGetActivityLogModelActivity
{
    public List<WhmcsActivityLog> entry { get; set; } = null!;
}

public class WhmcsGetActivityLogModel
{
    public WhmcsGetActivityLogModelWhmcsApi whmcsapi { get; set; } = null!;
}

public class WhmcsGetActivityLogModelWhmcsApi
{
    [JsonProperty("@version")] public string Version { get; set; } = null!;

    public string action { get; set; } = null!;
    public string result { get; set; } = null!;
    public string totalresults { get; set; } = null!;
    public string startnumber { get; set; } = null!;
    public WhmcsGetActivityLogModelActivity activity { get; set; } = null!;
}
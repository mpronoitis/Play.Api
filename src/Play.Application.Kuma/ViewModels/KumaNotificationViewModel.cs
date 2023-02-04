using Newtonsoft.Json;

namespace Play.Application.Kuma.ViewModels;

public class KumaNotificationViewModel
{
    public KumaHeartbeatViewModel? heartbeat { get; set; }
    public KumaMonitorViewModel? monitor { get; set; }
    public string msg { get; set; } = null!;
}

public class KumaHeartbeatViewModel
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? monitorID { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? status { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? time { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? msg { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public bool? important { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? duration { get; set; }
}

public class KumaMonitorViewModel
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? name { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? url { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? hostname { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? port { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? maxretries { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? weight { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? active { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? type { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? interval { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? keyword { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, bool>? notificationIDList { get; set; }
}
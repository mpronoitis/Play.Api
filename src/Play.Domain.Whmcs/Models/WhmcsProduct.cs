using Newtonsoft.Json;
using Play.Domain.Whmcs.ResponseModels;

namespace Play.Domain.Whmcs.Models;

public class WhmcsProduct
{
    [JsonProperty("pid")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Pid { get; set; }

    [JsonProperty("gid")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Gid { get; set; }

    [JsonProperty("type")] public string Type { get; set; } = null!;

    [JsonProperty("name")] public string Name { get; set; } = null!;

    [JsonProperty("slug")] public string Slug { get; set; } = null!;

    [JsonProperty("product_url")] public Uri ProductUrl { get; set; } = null!;

    [JsonProperty("module")] public string Module { get; set; } = null!;

    [JsonProperty("paytype")] public string Paytype { get; set; } = null!;
}
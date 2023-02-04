using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Play.Domain.Whmcs.Models;

namespace Play.Domain.Whmcs.ResponseModels;

public partial class WhmcsGetProductsResponseModel
{
    [JsonProperty("whmcsapi")] public WhmcsGetProductsResponseModelWhmcsapi Whmcsapi { get; set; } = null!;
}

public class WhmcsGetProductsResponseModelWhmcsapi
{
    [JsonProperty("@version")] public string Version { get; set; } = null!;

    [JsonProperty("action")] public string Action { get; set; } = null!;

    [JsonProperty("result")] public string Result { get; set; } = null!;

    [JsonProperty("totalresults")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Totalresults { get; set; }

    [JsonProperty("products")] public WhmcsGetProductsResponseModelProducts Products { get; set; } = null!;
}

public class WhmcsGetProductsResponseModelProducts
{
    [JsonProperty("product")] public WhmcsProduct[] Product { get; set; } = null!;
}

public class WhmcsGetProductsResponseModelPricing
{
    [JsonProperty("prefix")] public string Prefix { get; set; } = null!;

    [JsonProperty("suffix")] public string Suffix { get; set; } = null!;

    [JsonProperty("msetupfee")] public string Msetupfee { get; set; } = null!;

    [JsonProperty("qsetupfee")] public string Qsetupfee { get; set; } = null!;

    [JsonProperty("ssetupfee")] public string Ssetupfee { get; set; } = null!;

    [JsonProperty("asetupfee")] public string Asetupfee { get; set; } = null!;

    [JsonProperty("bsetupfee")] public string Bsetupfee { get; set; } = null!;

    [JsonProperty("tsetupfee")] public string Tsetupfee { get; set; } = null!;

    [JsonProperty("monthly")] public string Monthly { get; set; } = null!;

    [JsonProperty("quarterly")] public string Quarterly { get; set; } = null!;

    [JsonProperty("semiannually")] public string Semiannually { get; set; } = null!;

    [JsonProperty("annually")] public string Annually { get; set; } = null!;

    [JsonProperty("biennially")] public string Biennially { get; set; } = null!;

    [JsonProperty("triennially")] public string Triennially { get; set; } = null!;
}

public partial class WhmcsGetProductsResponseModel
{
    public static WhmcsGetProductsResponseModel FromJson(string json)
    {
        return JsonConvert.DeserializeObject<WhmcsGetProductsResponseModel>(json,
                   WhmcsGetProductsResponseModelConverter.Settings) ??
               throw new InvalidOperationException("Could not deserialize json");
    }
}

internal static class WhmcsGetProductsResponseModelConverter
{
    public static readonly JsonSerializerSettings Settings = new()
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
        {
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        }
    };
}
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Play.Domain.Whmcs.Models;

namespace Play.Domain.Whmcs.ResponseModels;

public partial class WhmcsGetDomainsResponseModel
{
    [JsonProperty("whmcsapi")] public WhmcsGetDomainsResponseModelWhmcsapi Whmcsapi { get; set; } = null!;
}

public class WhmcsGetDomainsResponseModelWhmcsapi
{
    [JsonProperty("@version")] public string Version { get; set; } = null!;

    [JsonProperty("action")] public string Action { get; set; } = null!;

    [JsonProperty("result")] public string Result { get; set; } = null!;

    [JsonProperty("clientid")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Clientid { get; set; }

    [JsonProperty("domainid")] public string Domainid { get; set; } = null!;

    [JsonProperty("totalresults")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Totalresults { get; set; }

    [JsonProperty("startnumber")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Startnumber { get; set; }

    [JsonProperty("numreturned")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Numreturned { get; set; }

    [JsonProperty("domains")] public WhmcsGetDomainsResponseModelDomains Domains { get; set; } = null!;
}

public class WhmcsGetDomainsResponseModelDomains
{
    //domain can be a single domain or a list of domains depending on the number of domains
    //to handle this we need to use a custom converter
    [JsonProperty("domain")]
    [JsonConverter(typeof(WhmcsGetDomainsResponseModelProductsConverter))]
    public WhmcsDomain[] Domain { get; set; } = null!;
}

public class WhmcsGetDomainsResponseModelProductsConverter : JsonConverter
{
    public override bool CanConvert(Type t)
    {
        return t == typeof(WhmcsDomain[]) || t == typeof(WhmcsDomain);
    }

    public override object? ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
    {
        switch (reader.TokenType)
        {
            case JsonToken.Null:
                return null;
            case JsonToken.StartArray:
            {
                var value = serializer.Deserialize<WhmcsDomain[]>(reader);
                return value;
            }
            case JsonToken.StartObject:
            {
                var value = serializer.Deserialize<WhmcsDomain>(reader);
                return new[] { value };
            }
            case JsonToken.None:
            case JsonToken.StartConstructor:
            case JsonToken.PropertyName:
            case JsonToken.Comment:
            case JsonToken.Raw:
            case JsonToken.Integer:
            case JsonToken.Float:
            case JsonToken.String:
            case JsonToken.Boolean:
            case JsonToken.Undefined:
            case JsonToken.EndObject:
            case JsonToken.EndArray:
            case JsonToken.EndConstructor:
            case JsonToken.Date:
            case JsonToken.Bytes:
            default:
                throw new Exception("Cannot unmarshal type WhmcsProduct");
        }
    }

    public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
    {
        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }

        var value = (WhmcsDomain[])untypedValue;
        if (value.Length == 1)
        {
            serializer.Serialize(writer, value[0]);
            return;
        }

        serializer.Serialize(writer, value);
    }
}

public partial class WhmcsGetDomainsResponseModel
{
    public static WhmcsGetDomainsResponseModel FromJson(string json)
    {
        return JsonConvert.DeserializeObject<WhmcsGetDomainsResponseModel>(json,
                   WhmcsGetDomainsResponseModelConverter.Settings) ??
               throw new InvalidOperationException("Failed to deserialize json");
    }
}

public static class WhmcsGetDomainsResponseModelConverterSerialize
{
    public static string ToJson(this WhmcsGetDomainsResponseModel self)
    {
        return JsonConvert.SerializeObject(self, WhmcsGetDomainsResponseModelConverter.Settings);
    }
}

internal static class WhmcsGetDomainsResponseModelConverter
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

internal class WhmcsGetDomainsResponseModelConverterParseStringConverter : JsonConverter
{
    public override bool CanConvert(Type t)
    {
        return t == typeof(long) || t == typeof(long?);
    }

    public override object? ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        if (long.TryParse(value, out var l)) return l;

        throw new Exception("Cannot unmarshal type long");
    }

    public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
    {
        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }

        var value = (long)untypedValue;
        serializer.Serialize(writer, value.ToString());
    }
}
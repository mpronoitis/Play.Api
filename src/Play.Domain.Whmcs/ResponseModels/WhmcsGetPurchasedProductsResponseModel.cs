using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Play.Domain.Whmcs.Models;

namespace Play.Domain.Whmcs.ResponseModels;

public partial class WhmcsGetPurchasedProductsResponseModel
{
    [JsonProperty("whmcsapi")] public WhmcsGetPurchasedProductsResponseModelWhmcsapi Whmcsapi { get; set; } = null!;
}

public class WhmcsGetPurchasedProductsResponseModelWhmcsapi
{
    [JsonProperty("@version")] public string Version { get; set; } = null!;

    [JsonProperty("action")] public string Action { get; set; } = null!;

    [JsonProperty("result")] public string Result { get; set; } = null!;

    [JsonProperty("clientid")] public string Clientid { get; set; } = null!;

    [JsonProperty("serviceid")] public string Serviceid { get; set; } = null!;

    [JsonProperty("pid")] public string Pid { get; set; } = null!;

    [JsonProperty("domain")] public string Domain { get; set; } = null!;

    [JsonProperty("totalresults")] public string Totalresults { get; set; } = null!;

    [JsonProperty("startnumber")] public string Startnumber { get; set; } = null!;

    [JsonProperty("numreturned")] public string Numreturned { get; set; } = null!;

    [JsonProperty("products")] public WhmcsGetPurchasedProductsResponseModelProducts Products { get; set; } = null!;
}

public class WhmcsGetPurchasedProductsResponseModelProducts
{
    //can be either public WhmcsPurchasedProduct[] Product { get; set; } or public WhmcsPurchasedProduct Product { get; set; }
    //to handle this we need to use a custom converter
    [JsonProperty("product")]
    [JsonConverter(typeof(WhmcsGetPurchasedProductsResponseModelProductsConverter))]
    public WhmcsPurchasedProduct[] Product { get; set; } = null!;
}

/// <summary>
///     Converter for handling the different types of products returned by WHMCS
///     They can either be a single product or an array of products
/// </summary>
public class WhmcsGetPurchasedProductsResponseModelProductsConverter : JsonConverter
{
    public static readonly WhmcsGetPurchasedProductsResponseModelProductsConverter Singleton = new();

    public override bool CanConvert(Type t)
    {
        return t == typeof(WhmcsPurchasedProduct[]) || t == typeof(WhmcsPurchasedProduct);
    }

    public override object? ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
    {
        switch (reader.TokenType)
        {
            case JsonToken.Null:
                return null;
            case JsonToken.StartArray:
            {
                var value = serializer.Deserialize<WhmcsPurchasedProduct[]>(reader);
                return value;
            }
            case JsonToken.StartObject:
            {
                var value = serializer.Deserialize<WhmcsPurchasedProduct>(reader);
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
                throw new Exception("Cannot unmarshal type WhmcsPurchasedProduct");
        }
    }

    public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
    {
        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }

        var value = (WhmcsPurchasedProduct[])untypedValue;
        if (value.Length == 1)
        {
            serializer.Serialize(writer, value[0]);
            return;
        }

        serializer.Serialize(writer, value);
    }
}

public class WhmcsGetPurchasedProductsResponseModelCustomfields
{
    [JsonProperty("customfield")]
    public WhmcsGetPurchasedProductsResponseModelCustomfield Customfield { get; set; } = null!;
}

public class WhmcsGetPurchasedProductsResponseModelCustomfield
{
    [JsonProperty("id")] public string Id { get; set; } = null!;

    [JsonProperty("name")] public string Name { get; set; } = null!;

    [JsonProperty("translated_name")] public string TranslatedName { get; set; } = null!;

    [JsonProperty("value")] public string Value { get; set; } = null!;
}

public partial class WhmcsGetPurchasedProductsResponseModel
{
    public static WhmcsGetPurchasedProductsResponseModel FromJson(string json)
    {
        return JsonConvert.DeserializeObject<WhmcsGetPurchasedProductsResponseModel>(json,
                   WhmcsGetPurchasedProductsResponseModelConverter.Settings) ??
               throw new InvalidOperationException("Unable to deserialize WHMCS response");
    }
}

internal static class WhmcsGetPurchasedProductsResponseModelConverter
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
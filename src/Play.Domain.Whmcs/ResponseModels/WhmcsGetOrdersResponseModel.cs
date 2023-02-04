using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Play.Domain.Whmcs.Models;

namespace Play.Domain.Whmcs.ResponseModels;

public partial class WhmcsGetOrdersResponseModel
{
    [JsonProperty("whmcsapi")] public WhmcsGetOrdersResponseModelWhmcsapi Whmcsapi { get; set; } = null!;
}

public class WhmcsGetOrdersResponseModelWhmcsapi
{
    [JsonProperty("@version")] public string Version { get; set; } = null!;

    [JsonProperty("action")] public string Action { get; set; } = null!;

    [JsonProperty("result")] public string Result { get; set; } = null!;

    [JsonProperty("totalresults")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Totalresults { get; set; }

    [JsonProperty("startnumber")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Startnumber { get; set; }

    [JsonProperty("numreturned")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Numreturned { get; set; }

    [JsonProperty("orders")] public WhmcsGetOrdersResponseModelOrders Orders { get; set; } = null!;
}

public class WhmcsGetOrdersResponseModelOrders
{
    [JsonProperty("order")] public WhmcsOrder[] Order { get; set; } = null!;
}

public class WhmcsGetOrdersResponseModelLineitems
{
    [JsonProperty("lineitem")] public WhmcsGetOrdersResponseModelLineitemUnion Lineitem { get; set; }
}

public class WhmcsGetOrdersResponseModelLineitemElement
{
    [JsonProperty("type")] public string Type { get; set; } = null!;

    [JsonProperty("relid")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Relid { get; set; }

    [JsonProperty("producttype")] public string Producttype { get; set; } = null!;

    [JsonProperty("product")] public string Product { get; set; } = null!;

    [JsonProperty("domain")] public string Domain { get; set; } = null!;

    [JsonProperty("billingcycle")] public string Billingcycle { get; set; } = null!;

    [JsonProperty("amount")] public string Amount { get; set; } = null!;

    [JsonProperty("status")] public string Status { get; set; } = null!;

    [JsonProperty("dnsmanagement", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(ParseStringConverter))]
    public long? Dnsmanagement { get; set; }

    [JsonProperty("emailforwarding", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(ParseStringConverter))]
    public long? Emailforwarding { get; set; }

    [JsonProperty("idprotection", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(ParseStringConverter))]
    public long? Idprotection { get; set; }
}

public class WhmcsGetOrdersResponseModelTransfersecretClass
{
    [JsonProperty("#cdata-section")] public string CdataSection { get; set; } = null!;
}

public struct WhmcsGetOrdersResponseModelLineitemUnion
{
    public WhmcsGetOrdersResponseModelLineitemElement? LineitemElement;
    public WhmcsGetOrdersResponseModelLineitemElement[]? LineitemElementArray;

    public static implicit operator WhmcsGetOrdersResponseModelLineitemUnion(
        WhmcsGetOrdersResponseModelLineitemElement lineitemElement)
    {
        return new WhmcsGetOrdersResponseModelLineitemUnion { LineitemElement = lineitemElement };
    }

    public static implicit operator WhmcsGetOrdersResponseModelLineitemUnion(
        WhmcsGetOrdersResponseModelLineitemElement[] lineitemElementArray)
    {
        return new WhmcsGetOrdersResponseModelLineitemUnion { LineitemElementArray = lineitemElementArray };
    }
}

public struct WhmcsGetOrdersResponseModelTransfersecretUnion
{
    public string? String;
    public WhmcsGetOrdersResponseModelTransfersecretClass? TransfersecretClass;

    public static implicit operator WhmcsGetOrdersResponseModelTransfersecretUnion(string @string)
    {
        return new WhmcsGetOrdersResponseModelTransfersecretUnion { String = @string };
    }

    public static implicit operator WhmcsGetOrdersResponseModelTransfersecretUnion(
        WhmcsGetOrdersResponseModelTransfersecretClass transfersecretClass)
    {
        return new WhmcsGetOrdersResponseModelTransfersecretUnion { TransfersecretClass = transfersecretClass };
    }
}

public partial class WhmcsGetOrdersResponseModel
{
    public static WhmcsGetOrdersResponseModel FromJson(string json)
    {
        return JsonConvert.DeserializeObject<WhmcsGetOrdersResponseModel>(json,
                   WhmcsGetOrdersResponseModelConverter.Settings) ??
               throw new InvalidOperationException("Unable to deserialize response");
    }
}

public static class Serialize
{
    public static string ToJson(this WhmcsGetOrdersResponseModel self)
    {
        return JsonConvert.SerializeObject(self, WhmcsGetOrdersResponseModelConverter.Settings);
    }
}

internal static class WhmcsGetOrdersResponseModelConverter
{
    public static readonly JsonSerializerSettings Settings = new()
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
        {
            WhmcsGetOrdersResponseModelLineitemUnionConverter.Singleton,
            TransfersecretUnionConverter.Singleton,
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        }
    };
}

internal class ParseStringConverter : JsonConverter
{
    public static readonly ParseStringConverter Singleton = new();

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

internal class WhmcsGetOrdersResponseModelLineitemUnionConverter : JsonConverter
{
    public static readonly WhmcsGetOrdersResponseModelLineitemUnionConverter Singleton = new();

    public override bool CanConvert(Type t)
    {
        return t == typeof(WhmcsGetOrdersResponseModelLineitemUnion) ||
               t == typeof(WhmcsGetOrdersResponseModelLineitemUnion?);
    }

    public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
    {
        switch (reader.TokenType)
        {
            case JsonToken.StartObject:
                var objectValue = serializer.Deserialize<WhmcsGetOrdersResponseModelLineitemElement>(reader);
                return new WhmcsGetOrdersResponseModelLineitemUnion { LineitemElement = objectValue };
            case JsonToken.StartArray:
                var arrayValue = serializer.Deserialize<WhmcsGetOrdersResponseModelLineitemElement[]>(reader);
                return new WhmcsGetOrdersResponseModelLineitemUnion { LineitemElementArray = arrayValue };
        }

        throw new Exception("Cannot unmarshal type WhmcsGetOrdersResponseModelLineitemUnion");
    }

    public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
    {
        var value = (WhmcsGetOrdersResponseModelLineitemUnion)(untypedValue ??
                                                               throw new ArgumentNullException(nameof(untypedValue)));
        if (value.LineitemElementArray != null)
        {
            serializer.Serialize(writer, value.LineitemElementArray);
            return;
        }

        if (value.LineitemElement == null)
            throw new Exception("Cannot marshal type WhmcsGetOrdersResponseModelLineitemUnion");
        serializer.Serialize(writer, value.LineitemElement);
    }
}

internal class TransfersecretUnionConverter : JsonConverter
{
    public static readonly TransfersecretUnionConverter Singleton = new();

    public override bool CanConvert(Type t)
    {
        return t == typeof(WhmcsGetOrdersResponseModelTransfersecretUnion) ||
               t == typeof(WhmcsGetOrdersResponseModelTransfersecretUnion?);
    }

    public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
    {
        switch (reader.TokenType)
        {
            case JsonToken.String:
            case JsonToken.Date:
                var stringValue = serializer.Deserialize<string>(reader);
                return new WhmcsGetOrdersResponseModelTransfersecretUnion { String = stringValue };
            case JsonToken.StartObject:
                var objectValue = serializer.Deserialize<WhmcsGetOrdersResponseModelTransfersecretClass>(reader);
                return new WhmcsGetOrdersResponseModelTransfersecretUnion { TransfersecretClass = objectValue };
            case JsonToken.None:
                break;
            case JsonToken.StartArray:
                break;
            case JsonToken.StartConstructor:
                break;
            case JsonToken.PropertyName:
                break;
            case JsonToken.Comment:
                break;
            case JsonToken.Raw:
                break;
            case JsonToken.Integer:
                break;
            case JsonToken.Float:
                break;
            case JsonToken.Boolean:
                break;
            case JsonToken.Null:
                break;
            case JsonToken.Undefined:
                break;
            case JsonToken.EndObject:
                break;
            case JsonToken.EndArray:
                break;
            case JsonToken.EndConstructor:
                break;
            case JsonToken.Bytes:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        throw new Exception("Cannot unmarshal type TransfersecretUnion");
    }

    public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
    {
        var value = (WhmcsGetOrdersResponseModelTransfersecretUnion)(untypedValue ??
                                                                     throw new ArgumentNullException(
                                                                         nameof(untypedValue)));
        if (value.String != null)
        {
            serializer.Serialize(writer, value.String);
            return;
        }

        if (value.TransfersecretClass != null)
        {
            serializer.Serialize(writer, value.TransfersecretClass);
            return;
        }

        throw new Exception("Cannot marshal type TransfersecretUnion");
    }
}
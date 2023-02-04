using Newtonsoft.Json;
using Play.Domain.Whmcs.ResponseModels;

namespace Play.Domain.Whmcs.Models;

public class WhmcsDomain
{
    [JsonProperty("id")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Id { get; set; }

    [JsonProperty("userid")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Userid { get; set; }

    [JsonProperty("orderid")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Orderid { get; set; }

    [JsonProperty("regtype")] public string Regtype { get; set; } = null!;

    [JsonProperty("domainname")] public string Domainname { get; set; } = null!;

    [JsonProperty("registrar")] public string Registrar { get; set; } = null!;

    [JsonProperty("regperiod")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Regperiod { get; set; }

    [JsonProperty("firstpaymentamount")] public string Firstpaymentamount { get; set; } = null!;

    [JsonProperty("recurringamount")] public string Recurringamount { get; set; } = null!;

    [JsonProperty("paymentmethod")] public string Paymentmethod { get; set; } = null!;

    [JsonProperty("paymentmethodname")] public string Paymentmethodname { get; set; } = null!;

    [JsonProperty("regdate")] public string Regdate { get; set; } = null!;

    [JsonProperty("expirydate")] public string Expirydate { get; set; } = null!;

    [JsonProperty("nextduedate")] public string Nextduedate { get; set; } = null!;

    [JsonProperty("status")] public string Status { get; set; } = null!;

    [JsonProperty("subscriptionid")] public string Subscriptionid { get; set; } = null!;

    [JsonProperty("promoid")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Promoid { get; set; }

    [JsonProperty("dnsmanagement")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Dnsmanagement { get; set; }

    [JsonProperty("emailforwarding")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Emailforwarding { get; set; }

    [JsonProperty("idprotection")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Idprotection { get; set; }

    [JsonProperty("donotrenew")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Donotrenew { get; set; }

    [JsonProperty("notes")] public string Notes { get; set; } = null!;
}
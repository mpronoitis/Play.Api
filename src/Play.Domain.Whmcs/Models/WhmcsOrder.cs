using Newtonsoft.Json;
using Play.Domain.Whmcs.ResponseModels;

namespace Play.Domain.Whmcs.Models;

public class WhmcsOrder
{
    [JsonProperty("id")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Id { get; set; }

    [JsonProperty("ordernum")] public string Ordernum { get; set; } = null!;

    [JsonProperty("userid")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Userid { get; set; }

    [JsonProperty("contactid")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Contactid { get; set; }

    [JsonProperty("requestor_id")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long RequestorId { get; set; }

    [JsonProperty("admin_requestor_id")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long AdminRequestorId { get; set; }

    [JsonProperty("date")] public DateTimeOffset Date { get; set; }

    [JsonProperty("nameservers")] public string Nameservers { get; set; } = null!;

    [JsonProperty("transfersecret")] public object Transfersecret { get; set; } = null!;

    [JsonProperty("renewals")] public string Renewals { get; set; } = null!;

    [JsonProperty("promocode")] public string Promocode { get; set; } = null!;

    [JsonProperty("promotype")] public string Promotype { get; set; } = null!;

    [JsonProperty("promovalue")] public string Promovalue { get; set; } = null!;

    [JsonProperty("orderdata")] public string Orderdata { get; set; } = null!;

    [JsonProperty("amount")] public string Amount { get; set; } = null!;

    [JsonProperty("paymentmethod")] public string Paymentmethod { get; set; } = null!;

    [JsonProperty("invoiceid")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Invoiceid { get; set; }

    [JsonProperty("status")] public string Status { get; set; } = null!;

    [JsonProperty("ipaddress")] public string Ipaddress { get; set; } = null!;

    [JsonProperty("fraudmodule")] public string Fraudmodule { get; set; } = null!;

    [JsonProperty("fraudoutput")] public string Fraudoutput { get; set; } = null!;

    [JsonProperty("notes")] public string Notes { get; set; } = null!;

    [JsonProperty("paymentmethodname")] public string Paymentmethodname { get; set; } = null!;

    [JsonProperty("paymentstatus")] public string Paymentstatus { get; set; } = null!;

    [JsonProperty("name")] public string Name { get; set; } = null!;

    [JsonProperty("currencyprefix")] public string Currencyprefix { get; set; } = null!;

    [JsonProperty("currencysuffix")] public string Currencysuffix { get; set; } = null!;

    [JsonProperty("frauddata")] public string Frauddata { get; set; } = null!;

    [JsonProperty("validationdata")] public string Validationdata { get; set; } = null!;

    [JsonProperty("lineitems", NullValueHandling = NullValueHandling.Ignore)]
    public WhmcsGetOrdersResponseModelLineitems Lineitems { get; set; } = null!;
}
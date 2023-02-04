using Newtonsoft.Json;
using Play.Domain.Whmcs.ResponseModels;

namespace Play.Domain.Whmcs.Models;

public class WhmcsPurchasedProduct
{
    [JsonProperty("id")] public string Id { get; set; } = null!;

    [JsonProperty("clientid")] public string Clientid { get; set; } = null!;

    [JsonProperty("orderid")] public string Orderid { get; set; } = null!;

    [JsonProperty("ordernumber")] public string Ordernumber { get; set; } = null!;

    [JsonProperty("pid")] public string Pid { get; set; } = null!;

    [JsonProperty("regdate")] public DateTimeOffset Regdate { get; set; }

    [JsonProperty("name")] public string Name { get; set; } = null!;

    [JsonProperty("translated_name")] public string TranslatedName { get; set; } = null!;

    [JsonProperty("groupname")] public string Groupname { get; set; } = null!;

    [JsonProperty("translated_groupname")] public string TranslatedGroupname { get; set; } = null!;

    [JsonProperty("domain")] public string Domain { get; set; } = null!;

    [JsonProperty("dedicatedip")] public string Dedicatedip { get; set; } = null!;

    [JsonProperty("serverid")] public string Serverid { get; set; } = null!;

    [JsonProperty("servername")] public string Servername { get; set; } = null!;

    [JsonProperty("serverip")] public string Serverip { get; set; } = null!;

    [JsonProperty("serverhostname")] public string Serverhostname { get; set; } = null!;

    [JsonProperty("suspensionreason")] public string Suspensionreason { get; set; } = null!;

    [JsonProperty("firstpaymentamount")] public string Firstpaymentamount { get; set; } = null!;

    [JsonProperty("recurringamount")] public string Recurringamount { get; set; } = null!;

    [JsonProperty("paymentmethod")] public string Paymentmethod { get; set; } = null!;

    [JsonProperty("paymentmethodname")] public string Paymentmethodname { get; set; } = null!;

    [JsonProperty("billingcycle")] public string Billingcycle { get; set; } = null!;

    [JsonProperty("nextduedate")] public string Nextduedate { get; set; } = null!;

    [JsonProperty("status")] public string Status { get; set; } = null!;

    [JsonProperty("username")] public string Username { get; set; } = null!;

    [JsonProperty("password")] public string Password { get; set; } = null!;

    [JsonProperty("subscriptionid")] public string Subscriptionid { get; set; } = null!;

    [JsonProperty("promoid")] public string Promoid { get; set; } = null!;

    [JsonProperty("overideautosuspend")] public string Overideautosuspend { get; set; } = null!;

    [JsonProperty("overidesuspenduntil")] public string Overidesuspenduntil { get; set; } = null!;

    [JsonProperty("ns1")] public string Ns1 { get; set; } = null!;

    [JsonProperty("ns2")] public string Ns2 { get; set; } = null!;

    [JsonProperty("assignedips")] public string Assignedips { get; set; } = null!;

    [JsonProperty("notes")] public string Notes { get; set; } = null!;

    [JsonProperty("diskusage")] public string Diskusage { get; set; } = null!;

    [JsonProperty("disklimit")] public string Disklimit { get; set; } = null!;

    [JsonProperty("bwusage")] public string Bwusage { get; set; } = null!;

    [JsonProperty("bwlimit")] public string Bwlimit { get; set; } = null!;

    [JsonProperty("lastupdate")] public DateTimeOffset Lastupdate { get; set; }

    [JsonProperty("customfields", NullValueHandling = NullValueHandling.Ignore)]
    public WhmcsGetPurchasedProductsResponseModelCustomfields Customfields { get; set; } = null!;
}
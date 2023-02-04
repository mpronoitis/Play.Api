namespace Play.Domain.Whmcs.Models;

public class WhmcsAddOrder
{
    /// <summary>
    ///     The ID of the client to add the order for
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    ///     The payment method for the order in the system format. eg. paypal, mailin. Required
    /// </summary>
    public string PaymentMethod { get; set; } = null!;

    /// <summary>
    ///     The array of product ids to add the order for. Optional
    /// </summary>
    public int[]? ProductIds { get; set; }

    /// <summary>
    ///     The array of domain names associated with the products/domains. Optional
    /// </summary>
    public string[]? DomainNames { get; set; }

    /// <summary>
    ///     The array of billing cycles for the products. Optional
    /// </summary>
    public string[]? BillingCycles { get; set; }

    /// <summary>
    ///     For domain registrations, an array of register or transfer values. Optional
    /// </summary>
    public string[]? DomainRegTypes { get; set; }

    /// <summary>
    ///     For domain registrations, the registration periods for the domains in the order. Optional
    /// </summary>
    public int[]? DomainRegPeriods { get; set; }

    /// <summary>
    ///     For IDN domain registrations. The language code for the domain being registered. Optional
    /// </summary>
    public string[]? DomainIdnLangs { get; set; }

    /// <summary>
    ///     For domain transfers. The epp codes for the domains being transferred in the order. Optional
    /// </summary>
    public string[]? DomainEppCodes { get; set; }

    /// <summary>
    ///     The first nameserver to apply to all domains in the order. Optional
    /// </summary>
    public string? FirstNameserver { get; set; }

    /// <summary>
    ///     The second nameserver to apply to all domains in the order. Optional
    /// </summary>
    public string? SecondNameserver { get; set; }

    /// <summary>
    ///     The third nameserver to apply to all domains in the order. Optional
    /// </summary>
    public string? ThirdNameserver { get; set; }

    /// <summary>
    ///     The fourth nameserver to apply to all domains in the order. Optional
    /// </summary>
    public string? FourthNameserver { get; set; }

    /// <summary>
    ///     The fifth nameserver to apply to all domains in the order. Optional
    /// </summary>
    public string? FifthNameserver { get; set; }

    /// <summary>
    ///     an array of base64 encoded serialized array of product custom field values. Optional
    /// </summary>
    public string[]? CustomFields { get; set; }

    /// <summary>
    ///     an array of base64 encoded serialized array of product configurable options values.
    ///     Optional
    /// </summary>
    public string[]? ConfigOptions { get; set; }

    /// <summary>
    ///     Override the price of the product being ordered. Optional
    /// </summary>
    public decimal[]? OverridePrice { get; set; }

    /// <summary>
    ///     The promotion code to apply to the order. Optional
    /// </summary>
    public string? PromoCode { get; set; }

    /// <summary>
    ///     Should the promotion apply to the order even without matching promotional products.
    ///     Optional
    /// </summary>
    public bool PromoOverride { get; set; } = false;

    /// <summary>
    ///     The affiliate id to associate with the order. Optional
    /// </summary>
    public int AffiliateId { get; set; } = 0;

    /// <summary>
    ///     Set to true to suppress the invoice generating for the whole order. Optional
    /// </summary>
    public bool NoInvoice { get; set; } = false;

    /// <summary>
    ///     Set to true to suppress the Invoice Created email being sent for the order. Optional
    /// </summary>
    public bool NoInvoiceEmail { get; set; } = false;

    /// <summary>
    ///     Set to true to suppress the order confirmation email being sent for the order.
    ///     Optional
    /// </summary>
    public bool NoEmail { get; set; } = false;

    /// <summary>
    ///     A comma separated list of addons to create on order with the products. Optional
    /// </summary>
    public string? Addons { get; set; }

    /// <summary>
    ///     The hostname of the server for VPS/Dedicated Server orders. Optional
    /// </summary>
    public string? ServerHostname { get; set; }

    /// <summary>
    ///     The first nameserver prefix for the VPS/Dedicated server. Eg. ns1 in ns1.hostname.com. Optional
    /// </summary>
    public string? ServerNameserver1 { get; set; }

    /// <summary>
    ///     The second nameserver prefix for the VPS/Dedicated server. Eg. ns2 in ns2.hostname.com. Optional
    /// </summary>
    public string? ServerNameserver2 { get; set; }

    /// <summary>
    ///     The desired root password for the VPS/Dedicated server. Optional
    /// </summary>
    public string? ServerRootPassword { get; set; }

    /// <summary>
    ///     The id of the contact, associated with the client, that should apply to all domains in the
    ///     order. Optional
    /// </summary>
    public int DomainContactId { get; set; } = 0;

    /// <summary>
    ///     Add DNS Management to the Domain Order. Optional
    /// </summary>
    public bool DomainDnsManagement { get; set; } = false;

    /// <summary>
    ///     an array of base64 encoded serialized array of TLD Specific Field Values. Optional
    /// </summary>
    public string[]? TldSpecificFields { get; set; }

    /// <summary>
    ///     Add Email Forwarding to the Domain Order. Optional
    /// </summary>
    public bool DomainEmailForwarding { get; set; } = false;

    /// <summary>
    ///     Add ID Protection to the Domain Order. Optional
    /// </summary>
    public bool DomainIdProtection { get; set; } = false;

    /// <summary>
    ///     Override the price of the registration price on the domain being ordered. Optional
    /// </summary>
    public decimal[]? DomainOverridePrice { get; set; }

    /// <summary>
    ///     Override the price of the renewal price on the domain being ordered. Optional
    /// </summary>
    public decimal[]? DomainOverrideRenewalPrice { get; set; }

    /// <summary>
    ///     A name -> value array of $domainName -> $renewalPeriod renewals to add an order for.
    ///     Optional
    /// </summary>
    public string[]? DomainRenewals { get; set; }

    /// <summary>
    ///     The ip address to associate with the order. Optional
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    ///     The Addon ID for an Addon Only Order. Optional
    /// </summary>
    public int AddonId { get; set; } = 0;

    /// <summary>
    ///     An Array of addon ids for an Addon Only Order. Optional
    /// </summary>
    public int[]? AddonIds { get; set; }

    /// <summary>
    ///     An array of service ids to associate the addons for an Addon Only order. Optional
    /// </summary>
    public int[]? AddonServiceIds { get; set; }
}
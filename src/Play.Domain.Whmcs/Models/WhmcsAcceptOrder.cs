namespace Play.Domain.Whmcs.Models;

public class WhmcsAcceptOrder
{
    /// <summary>
    ///     The order id
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    ///     The specific server to assign to products within the order
    ///     Optional
    /// </summary>
    public int? ServerId { get; set; }

    /// <summary>
    ///     Service username
    ///     Optional
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    ///     Service password
    ///     Optional
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    ///     Registrar for domain products
    ///     Optional
    /// </summary>
    public string Registrar { get; set; } = null!;

    /// <summary>
    ///     Send the request to the registrar to register the domain.
    ///     Optional
    /// </summary>
    public bool? SendRegistrar { get; set; }

    /// <summary>
    ///     Send the request to the product module to activate the service. This can override the product configuration.
    ///     Optional
    /// </summary>
    public bool? SendModule { get; set; }

    /// <summary>
    ///     Send any automatic emails. This can be Product Welcome, Domain Renewal, Domain Transfer etc.
    ///     Optional
    /// </summary>
    public bool? SendEmail { get; set; }
}
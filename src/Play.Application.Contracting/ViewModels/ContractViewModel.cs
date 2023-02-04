using System.ComponentModel.DataAnnotations;

namespace Play.Application.Contracting.ViewModels;

public class ContractViewModel
{
    [Required(ErrorMessage = "Client name is required")]
    public string ClientName { get; set; } = null!;

    /// <summary>
    ///     The clients TIN
    /// </summary>
    [Required(ErrorMessage = "Client TIN is required")]
    public string ClientTin { get; set; } = null!;

    /// <summary>
    ///     The item's name
    /// </summary>
    [Required(ErrorMessage = "Item name is required")]
    public string ItemName { get; set; } = null!;

    /// <summary>
    ///     The status of the contract
    /// </summary>
    public string Status { get; set; } = null!;

    /// <summary>
    ///     The contracts start date
    /// </summary>
    [Required(ErrorMessage = "Start date is required")]
    public DateTime StartDate { get; set; }

    /// <summary>
    ///     The contracts end date
    /// </summary>
    [Required(ErrorMessage = "End date is required")]
    public DateTime EndDate { get; set; }

    /// <summary>
    ///     The heid of the client (hecontacts id)
    /// </summary>
    [Required(ErrorMessage = "Client is required")]
    public Guid ClientId { get; set; }

    /// <summary>
    ///     The id of the item
    /// </summary>
    [Required(ErrorMessage = "Item is required")]
    public Guid ItemId { get; set; }
}
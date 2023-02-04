using System.ComponentModel.DataAnnotations;

namespace Play.Application.Edi.ViewModels;

public class UpdateEdiCreditViewModel
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Customer id is required")]
    public Guid CustomerId { get; set; }

    [Required(ErrorMessage = "Credit amount is required")]
    public int CreditAmount { get; set; }
}
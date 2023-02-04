using System.ComponentModel.DataAnnotations;

namespace Play.Application.Edi.ViewModels;

public class RegisterEdiCreditViewModel
{
    [Required(ErrorMessage = "Customer id is required")]
    public Guid CustomerId { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    public int Amount { get; set; }
}
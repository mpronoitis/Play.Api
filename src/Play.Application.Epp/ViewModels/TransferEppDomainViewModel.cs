using System.ComponentModel.DataAnnotations;

namespace Play.Application.Epp.ViewModels;

public class TransferEppDomainViewModel
{
    [Required(ErrorMessage = "Domain name is required")]
    public string DomainName { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Contact id is required")]
    public string ContactId { get; set; } = null!;

    [Required(ErrorMessage = "New password is required")]
    public string NewPassword { get; set; } = null!;
}
using System.ComponentModel.DataAnnotations;

namespace Play.Application.Epp.ViewModels;

public class RegisterEppNameserverViewModel
{
    [Required(ErrorMessage = "The domain name is required")]
    public string DomainName { get; set; } = null!;

    [Required(ErrorMessage = "The nameserver is required")]
    public string Nameserver { get; set; } = null!;
}
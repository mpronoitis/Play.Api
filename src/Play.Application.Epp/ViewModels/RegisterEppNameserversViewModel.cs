using System.ComponentModel.DataAnnotations;

namespace Play.Application.Epp.ViewModels;

public class RegisterEppNameserversViewModel
{
    [Required(ErrorMessage = "Nameservers are required")]
    public string[] Nameservers { get; set; }

    [Required(ErrorMessage = "Domain is required")]
    public string Domain { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Play.Application.Epp.ViewModels;

public class RenewEppDomainViewModel
{
    [Required(ErrorMessage = "Domain name is required")]
    public string DomainName { get; set; } = null!;

    [Required(ErrorMessage = "Years is required")]
    [Range(2, 10, ErrorMessage = "Years must be between 2 and 10")]
    public int Years { get; set; }
}
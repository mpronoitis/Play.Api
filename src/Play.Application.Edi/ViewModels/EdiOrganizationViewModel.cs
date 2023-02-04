using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Play.Application.Edi.ViewModels;

public class EdiOrganizationViewModel
{
    [Key] public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MinLength(2)]
    [MaxLength(100)]
    [DisplayName("Name")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    [DisplayName("Email")]
    public string Email { get; set; } = null!;
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Play.Application.Edi.ViewModels;

public class EdiVariableViewModel
{
    [Key] public Guid Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [DisplayName("Title")]
    [MaxLength(50)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Description is required")]
    [DisplayName("Description")]
    [MaxLength(500)]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Placeholder is required")]
    [DisplayName("Placeholder")]
    [MaxLength(50)]
    public string Placeholder { get; set; } = null!;
}
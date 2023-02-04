using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Play.Application.Edi.ViewModels;

public class EdiSegmentViewModel
{
    [Key] public Guid Id { get; set; }

    [Required(ErrorMessage = "Model Id is required")]
    [DisplayName("Model Id")]
    public Guid Model_Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [DisplayName("Title")]
    [MaxLength(50)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Description is required")]
    [DisplayName("Description")]
    [MaxLength(500)]
    public string Description { get; set; } = null!;
}
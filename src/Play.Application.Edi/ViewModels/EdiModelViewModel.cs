using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Play.Application.Edi.ViewModels;

public class EdiModelViewModel
{
    [Key] public Guid Id { get; set; }

    [Required(ErrorMessage = "Org Id is required")]
    [DisplayName("Org Id")]
    public Guid Org_Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [DisplayName("Title")]
    [MinLength(3)]
    [MaxLength(50)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Segment Terminator is required")]
    [DisplayName("Segment Terminator")]
    public char SegmentTerminator { get; set; }

    [Required(ErrorMessage = "Sub Element Separator is required")]
    [DisplayName("Sub Element Separator")]
    public char SubElementSeparator { get; set; }

    [Required(ErrorMessage = "Element Separator is required")]
    [DisplayName("Element Separator")]
    public char ElementSeparator { get; set; }

    [Required(ErrorMessage = "Enabled flag is required")]
    [DisplayName("Enabled")]
    public bool Enabled { get; set; }
}
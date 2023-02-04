using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Play.Application.Edi.ViewModels;

public class EdiDocumentViewModel
{
    [Key] public Guid Id { get; set; }

    [Required(ErrorMessage = "Customer Id is required")]
    [DisplayName("Customer Id")]
    public Guid Customer_Id { get; set; }

    [Required(ErrorMessage = "Document Title is required")]
    [DisplayName("Document Title")]
    [MaxLength(50)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Edi payload is required")]
    [DisplayName("Edi payload")]
    public string EdiPayload { get; set; } = null!;

    [Required(ErrorMessage = "Document Payload is required")]
    [DisplayName("Document Payload")]
    public string DocumentPayload { get; set; } = null!;

    [Required(ErrorMessage = "Hedentid is required")]
    [DisplayName("Hedentid")]
    [MaxLength(50)]
    public string Hedentid { get; set; } = null!;

    [Required(ErrorMessage = "IsProcessed is required")]
    [DisplayName("IsProcessed")]
    public bool IsProcessed { get; set; }

    [Required(ErrorMessage = "IsSent is required")]
    [DisplayName("IsSent")]
    public bool IsSent { get; set; }

    [DisplayName("Created_At")] public DateTime Created_At { get; set; }
}
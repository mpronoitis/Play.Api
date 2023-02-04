using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Play.Application.Edi.ViewModels;

public class EdiDocumentReceiverViewModel
{
    [Required(ErrorMessage = "Customer Id is required")]
    [DisplayName("Customer Id")]
    public Guid Customer_Id { get; set; }

    [Required(ErrorMessage = "Document Title is required")]
    [DisplayName("Document Title")]
    [MaxLength(250)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Document Payload is required")]
    [DisplayName("Document Payload")]
    public string DocumentPayload { get; set; } = null!;

    [Required(ErrorMessage = "Hedentid is required")]
    [DisplayName("Hedentid")]
    [MaxLength(50)]
    public string Hedentid { get; set; } = null!;
}
using System.ComponentModel.DataAnnotations;

namespace Play.Application.Core.ViewModels;

public class UpdateEmailTemplateViewModel
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Subject is required")]
    public string Subject { get; set; } = null!;

    [Required(ErrorMessage = "Body is required")]
    public string Body { get; set; } = null!;
}
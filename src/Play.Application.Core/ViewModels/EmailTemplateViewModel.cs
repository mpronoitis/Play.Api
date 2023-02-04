using System.ComponentModel.DataAnnotations;

namespace Play.Application.Core.ViewModels;

public class EmailTemplateViewModel
{
    [Required(ErrorMessage = "Email Template Name is required")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email Template Subject is required")]
    public string Subject { get; set; } = null!;

    [Required(ErrorMessage = "Email Template Body is required")]
    public string Body { get; set; } = null!;
}
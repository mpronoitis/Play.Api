using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Play.Application.Contact.ViewModels;

public class ContactRequestViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [DisplayName("Email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Subject is required")]
    [DisplayName("Subject")]
    public string Subject { get; set; } = null!;

    [DisplayName("Message")] public string Message { get; set; } = null!;

    [DisplayName("PhoneNumber")] public string PhoneNumber { get; set; } = null!;
}
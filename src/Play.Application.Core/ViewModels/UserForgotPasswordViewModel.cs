using System.ComponentModel.DataAnnotations;

namespace Play.Application.Core.ViewModels;

public class UserForgotPasswordViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;
}
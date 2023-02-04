using System.ComponentModel.DataAnnotations;

namespace Play.Application.Core.ViewModels;

public class UpdateUserViewModel
{
    [Key] public Guid Id { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
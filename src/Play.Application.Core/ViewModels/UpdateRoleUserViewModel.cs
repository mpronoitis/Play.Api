using System.ComponentModel.DataAnnotations;

namespace Play.Application.Core.ViewModels;

public class UpdateRoleUserViewModel
{
    [Key] public Guid Id { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Role is required")]
    public string Role { get; set; } = null!;
}
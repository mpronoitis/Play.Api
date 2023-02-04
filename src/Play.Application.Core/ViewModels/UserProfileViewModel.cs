using System.ComponentModel.DataAnnotations;

namespace Play.Application.Core.ViewModels;

public class UserProfileViewModel
{
    [Key] public Guid Id { get; set; }

    [Required] [Key] public Guid User_Id { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    [StringLength(100, ErrorMessage = "First Name cannot be longer than 100 characters")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last Name is required")]
    [StringLength(100, ErrorMessage = "Last Name cannot be longer than 100 characters")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Date Of Birth is required")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Company Name is required")]
    [StringLength(100, ErrorMessage = "Company Name cannot be longer than 100 characters")]
    public string CompanyName { get; set; } = null!;

    [Required(ErrorMessage = "LanguagePreference is required")]
    [StringLength(2, ErrorMessage = "LanguagePreference cannot be longer than 2 characters")]
    public string LanguagePreference { get; set; } = null!;

    [Required(ErrorMessage = "ThemePreference is required")]
    [StringLength(20, ErrorMessage = "ThemePreference cannot be longer than 20 characters")]
    public string ThemePreference { get; set; } = null!;

    [Required(ErrorMessage = "TIN is required")]
    [StringLength(20, ErrorMessage = "TIN cannot be longer than 20 characters")]
    public string TIN { get; set; } = null!;
}
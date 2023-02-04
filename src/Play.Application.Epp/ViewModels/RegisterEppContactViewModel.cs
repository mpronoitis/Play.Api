using System.ComponentModel.DataAnnotations;

namespace Play.Application.Epp.ViewModels;

public class RegisterEppContactViewModel
{
    [Required(ErrorMessage = "Please enter an id")]
    public string Id { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a localized name")]
    public string LocalizedName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a localized organization")]
    public string LocalizedOrganization { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a localized street")]
    public string LocalizedStreet { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a localized city")]
    public string LocalizedCity { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a localized state")]
    public string LocalizedState { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a localized postal code")]
    public string LocalizedPostalCode { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a localized country")]
    public string LocalizedCountry { get; set; } = null!;

    [Required(ErrorMessage = "Please enter an international organization")]
    public string InternationalOrganization { get; set; } = null!;

    [Required(ErrorMessage = "Please enter an international name")]
    public string InternationalName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter an international street")]
    public string InternationalStreet { get; set; } = null!;

    [Required(ErrorMessage = "Please enter an international city")]
    public string InternationalCity { get; set; } = null!;

    [Required(ErrorMessage = "Please enter an international state")]
    public string InternationalState { get; set; } = null!;

    [Required(ErrorMessage = "Please enter an international postal code")]
    public string InternationalPostalCode { get; set; } = null!;

    [Required(ErrorMessage = "Please enter an international country")]
    public string InternationalCountry { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a voice phone")]
    public string VoicePhone { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a fax phone")]
    public string FaxPhone { get; set; } = null!;

    [Required(ErrorMessage = "Please enter an email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a password")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a disclose flag (0-1)")]
    public string DiscloseFlag { get; set; } = null!;
}
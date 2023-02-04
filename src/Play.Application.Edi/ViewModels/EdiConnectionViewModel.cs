using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Play.Application.Edi.ViewModels;

public class EdiConnectionViewModel
{
    [Key] public Guid Id { get; set; }

    [Required(ErrorMessage = "Customer id is required")]
    [DisplayName("Customer Id")]
    public Guid Customer_Id { get; set; }

    [Required(ErrorMessage = "Model Id is required")]
    [DisplayName("Model Id")]
    public Guid Model_Id { get; set; }

    [Required(ErrorMessage = "Org id is required")]
    [DisplayName("Org id")]
    public Guid Org_Id { get; set; }

    [Required(ErrorMessage = "Profile id is required")]
    [DisplayName("Profile id")]
    public Guid Profile_Id { get; set; }

    [Required(ErrorMessage = "Ftp hostname is required")]
    [DisplayName("Ftp hostname")]
    public string Ftp_Hostname { get; set; } = null!;

    [Required(ErrorMessage = "Ftp username is required")]
    [DisplayName("Ftp username")]
    public string Ftp_Username { get; set; } = null!;

    [Required(ErrorMessage = "Ftp password is required")]
    [DisplayName("Ftp password")]
    public string Ftp_Password { get; set; } = null!;
    
    [Required(ErrorMessage = "Ftp port is required")]
    [DisplayName("Ftp port")]
    public int Ftp_Port { get; set; }
    
    [Required(ErrorMessage = "File type is required")]
    [DisplayName("File type")]
    public string File_Type { get; set; } = null!;
}
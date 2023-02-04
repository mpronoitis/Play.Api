using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Play.Application.Edi.ViewModels;

public class EdiProfileViewModel
{
    [Key] public Guid Id { get; set; }

    //the customer id that is related to this profile
    [Required(ErrorMessage = "Customer Id is required")]
    [DisplayName("Customer Id")]
    public Guid Customer_Id { get; set; }

    //the model id that is related to this profile
    [Required(ErrorMessage = "Model Id is required")]
    [DisplayName("Model Id")]
    public Guid Model_Id { get; set; }

    //the title of the profile
    [Required(ErrorMessage = "Title is required")]
    [DisplayName("Title")]
    [MaxLength(50)]
    public string Title { get; set; } = null!;

    //the payload of the 
    [Required(ErrorMessage = "Payload is required")]
    [DisplayName("Payload")]
    [MaxLength(100000)]
    public string Payload { get; set; } = null!;

    //the enabled flag of the profile
    [Required(ErrorMessage = "Enabled is required")]
    [DisplayName("Enabled")]
    public bool Enabled { get; set; }
}
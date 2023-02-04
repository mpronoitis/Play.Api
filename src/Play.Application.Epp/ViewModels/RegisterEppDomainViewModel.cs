using System.ComponentModel.DataAnnotations;

namespace Play.Application.Epp.ViewModels;

public class RegisterEppDomainViewModel
{
    //domain name , must end with .gr
    [Required(ErrorMessage = "Το όνομα του domain είναι υποχρεωτικό")]
    [Display(Name = "Όνομα Domain")]
    public string DomainName { get; set; } = null!;

    //domain registrant, must start with b68_
    [Required(ErrorMessage = "Ο κωδικός του κατόχου του domain είναι υποχρεωτικό")]
    [Display(Name = "Κωδικός Κατόχου")]
    public string Registrant { get; set; } = null!;

    //domain admin, must start with b68_
    [Required(ErrorMessage = "Ο κωδικός του διαχειριστή του domain είναι υποχρεωτικό")]
    [Display(Name = "Κωδικός Διαχειριστή")]
    public string Admin { get; set; } = null!;

    //domain tech, must start with b68_
    [Required(ErrorMessage = "Ο κωδικός του τεχνικού του domain είναι υποχρεωτικό")]
    [Display(Name = "Κωδικός Τεχνικού")]
    public string Tech { get; set; } = null!;

    //domain billing, must start with b68_
    [Required(ErrorMessage = "Ο κωδικός του χρέωσης του domain είναι υποχρεωτικό")]
    [Display(Name = "Κωδικός Χρέωσης")]
    public string Billing { get; set; } = null!;

    [Required(ErrorMessage = "Ο κωδικός του domain είναι υποχρεωτικός")]
    [Display(Name = "Κωδικός Domain")]
    public string Password { get; set; } = null!;

    //domain period in years, can be 2-4-6-8-10
    [Required(ErrorMessage = "Η διάρκεια του domain είναι υποχρεωτική")]
    [Display(Name = "Διάρκεια Domain")]
    public int Period { get; set; }
}
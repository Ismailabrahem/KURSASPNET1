using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class AccountBasicInfoModel
{
    [Required(ErrorMessage = "You must enter a first name")]
    [Display(Name = "First name", Prompt = "Enter your first name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a last name")]
    [Display(Name = "Last name", Prompt = "Enter your last name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "You must enter an e-mail address")]
    [Display(Name = "Email address", Prompt = "Enter your email address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Display(Name = "Phone", Prompt = "Enter your phone number")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Bio (Optional)", Prompt = "Add a short bio...")]
    public string? Biography {  get; set; }
}

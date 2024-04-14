using System.ComponentModel.DataAnnotations;

namespace Silicon_1.Models;

public class SignInViewModel
{
    [Required]
    [Display(Name = "Email", Prompt = "Enter your email address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Password", Prompt = "Enter your password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Keep me signed in")]
    public bool RememberMe { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class AccountSecurityInfo
{

    [Required(ErrorMessage = "You must enter a password")]
    [Display(Name = "Current password", Prompt = "Enter your current password")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a password")]
    [Display(Name = "New Password", Prompt = "Enter your new password")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_])(?!.*\s).{8,}$", ErrorMessage = "A valid password is required")]
    public string NewPassword { get; set; } = null!;

    [Required(ErrorMessage = "Password and confirmation password do not match.")]
    [Display(Name = "Confirm new password", Prompt = "Confirm your password")]
    [Compare(nameof(NewPassword))]
    [DataType(DataType.Password)]
    public string ConfirmNewPassword { get; set; } = null!;
}

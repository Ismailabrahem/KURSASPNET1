using System.ComponentModel.DataAnnotations;

namespace Silicon_1.Models;

public class SubscribeViewModel
{

    [Required]
    [EmailAddress]

    [Display(Prompt = "Enter your email address")]
    [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;

    [Display(Name = "Daily newsletter")]
    public bool DailyNewsletter { get; set; }

    [Display(Name = "Advertising updated")]
    public bool AdvertisingUpdates { get; set; }

    [Display(Name = "Week in review")]
    public bool WeekinReview { get; set; }

    [Display(Name = "Event updates")]
    public bool EventUpdates { get; set; }

    [Display(Name = "Startups weekly")]
    public bool StartupsWeekly { get; set; }

    [Display(Name = "Podcasts")]
    public bool Podcasts { get; set; }
}

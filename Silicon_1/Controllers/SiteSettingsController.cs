using Microsoft.AspNetCore.Mvc;

namespace Silicon_1.Controllers;

public class SiteSettingsController : Controller
{
    public IActionResult Theme(string mode)
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(180)
        };
        Response.Cookies.Append("theme", mode, option);
        return Ok();
    }


    public IActionResult Consent(string mode)
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddYears(1),
            HttpOnly = true,
            Secure = true
        };
        Response.Cookies.Append("consent", "true", option);
        return Ok();
    }
}

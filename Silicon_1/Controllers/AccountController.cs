using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Silicon_1.Models;
using System.Security.Claims;

namespace Silicon_1.Controllers;

[Authorize]
public class AccountController(AccountService accountService, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IPasswordHasher<UserEntity> passwordHash) : Controller
{
    private readonly AccountService _accountService = accountService;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly IPasswordHasher<UserEntity> _passwordHash = passwordHash;

 

    public async Task<IActionResult> Details()
    {
        var user = await _accountService.GetUserAsync(User);

        var viewModel = new AccountDetailsViewModel
        {
            AccountBasicInfo = new AccountBasicInfoModel
            {
                FirstName = user.FirstName!,
                LastName = user.LastName!,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Biography = user.Bio,
            },
            AddressInfo = new AccountAddressInfo
            {
                AddressLine_1 = user.AddressLine_1!,
                AddressLine_2 = user.AddressLine_2,
                PostalCode = user.PostalCode!,
                City = user.City!,
            }
        };

        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> UpdateBasicInfo(AccountDetailsViewModel model)
    {
        if (model.AccountBasicInfo != null)
        {
            if (!string.IsNullOrEmpty(model.AccountBasicInfo.FirstName) && !string.IsNullOrEmpty(model.AccountBasicInfo.LastName))
            {
                var result = await _accountService.UpdatebasicInfoAsync(User, model.AccountBasicInfo);
            }
        }
        return RedirectToAction("Details", "Account");
    }

    [HttpPost]
    public async Task<IActionResult> ProfileImageUpload(IFormFile file)
    {
        var result = await _accountService.UploadUserProfileImageAsync(User, file);
        return RedirectToAction("Details", "Account");
    }


    [HttpPost]
    public async Task<IActionResult> UpdateAddressInfo(AccountDetailsViewModel model)
    {
        if (model.AddressInfo != null)
        {
            if (!string.IsNullOrEmpty(model.AddressInfo.AddressLine_1) && !string.IsNullOrEmpty(model.AddressInfo.PostalCode) && !string.IsNullOrEmpty(model.AddressInfo.City))
            {
                var result = await _accountService.UpdateAddressInfoAsync(User, model.AddressInfo);
            }
        }
        return RedirectToAction("Details", "Account");
    }

    [HttpGet]
    public IActionResult AccountSecurity()
    {
        return View("AccountSecurity");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AccountSecurity(AccountSecurityInfo model)
    {
        if(!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        if (result.Succeeded)
        {
            await _signInManager.RefreshSignInAsync(user);

            return RedirectToAction("AccountSecurity", "Account");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View("AccountSecurity");
    }

    [HttpGet]
    public IActionResult ConfirmDelete()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser()
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                TempData["AccountDeleted"] = "Your account has benn successfully deleted.";
                return RedirectToAction("SignIn", "Auth");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("Error");
            }
        }
        else
            return RedirectToAction("LogIn", "Auth");
    }
}

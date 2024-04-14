using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silicon_1.Models;

namespace Silicon_1.Controllers
{
    public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signInManager = signInManager;

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (!await _userManager.Users.AnyAsync(x => x.Email == model.Email))
                {
                    var userEntity = new UserEntity
                    {  
                        FirstName = model.Firstname,
                        LastName = model.Lastname,
                        Email = model.Email,
                        UserName = model.Email,
                    };

                    var result = await _userManager.CreateAsync(userEntity, model.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("SignIn", "Auth");
                    }
                    else
                    {
                        ViewData["StatusMessage"] = "Something went wrong, please try again.";
                    }
                }
                else 
                {
                    ViewData["StatusMessage"] = "User with submitted email adress already exists";
                }
            }
            
            return View(model);
        }



        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null) 
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ViewData["StatusMessage"] = "Incorrect email or password";
            return View(model);
        }


        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Auth");
        }
    }
}

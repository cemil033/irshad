using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Irsad.Models;
using Irsad.Models.ViewModels;
using Irsad.Models.Entities;

namespace Irsad.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        // UserManager
        // RoleManager
        // SignInManager
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email                    
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, true);
                    return RedirectToAction("GetAll", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                        ModelState.AddModelError(item.Code, item.Description);
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("GetAll", "Home");
        }

        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    if(await userManager.CheckPasswordAsync(user, model.Password))
                    {
                        await signInManager.SignInAsync(user, true);
                        if(!string.IsNullOrWhiteSpace(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("GetAll", "Home");
                    }
                }
            }
            ModelState.AddModelError("login", "Incorrect username or password");
            return View();
        }



    }
}

using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.ApplicationUser;
using Microsoft.AspNetCore.Mvc;

namespace CocktailHeaven.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IActionResult Login()
        {
            var model = new UserLoginModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await accountService.SignInUserAsync(model.Email, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login!");

            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await this.accountService.SignOutUserAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            var model = new UserRegisterModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await accountService.RegisterUserAsync(model.UserName, model.Email, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login!");

            return View(model);
        }
    }
}


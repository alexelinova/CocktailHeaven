using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.ApplicationUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailHeaven.Controllers
{
	public class AccountController : BaseController
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService _accountService)
        {
            this.accountService = _accountService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new UserLoginModel();

            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await this.accountService.SignInUserAsync(model.Email, model.Password);
            if (result.Succeeded)
            {
                return this.RedirectToAction("Index", "Home");
            }

            this.ModelState.AddModelError(string.Empty, "Invalid login!");

            return this.View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await this.accountService.SignOutUserAsync();

            return this.RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new UserRegisterModel();

            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await this.accountService.RegisterUserAsync(model.UserName, model.Email, model.Password);

            if (result.Succeeded)
            {
                return this.RedirectToAction(nameof(Login));
            }

            foreach (var item in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, item.Description);
            }

            return this.View(model);
        }
    }
}


using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.Cocktail;
using CocktailHeaven.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CocktailHeaven.Controllers
{
	public class HomeController : BaseController
	{
		private readonly ICocktailService cocktailService;
		public HomeController(ICocktailService cocktailService)
		{
			this.cocktailService = cocktailService;
		}

		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			if(this.User.IsInRole("Admin"))
			{
				return this.RedirectToAction("Index", "Home", new { area = "Admin" });
			}
			var model = new CocktailHomeModel()
			{
				Cocktails = await this.cocktailService.GetTopRatedCocktailsAsync(),
				CocktailsCount = await this.cocktailService.CocktailCountAsync()
			};

			return this.View(model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
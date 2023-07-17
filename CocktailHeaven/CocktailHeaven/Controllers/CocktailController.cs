using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.Cocktail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CocktailHeaven.Controllers
{
	public class CocktailController : BaseController
	{
		private readonly ICategoryService categoryService;
		private readonly ICocktailService cocktailService;
		public CocktailController(ICategoryService _categoryService, ICocktailService _cocktailService)
		{
			this.categoryService = _categoryService;
			this.cocktailService = _cocktailService;
		}

		[HttpGet]
		public IActionResult AddIngredientCount()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddIngredientCount(int count)
		{
			if (count <= 0)
			{
				return this.View();
			}

			return this.RedirectToAction(nameof(Create), new { ingredientCount = count });
		}

		public async Task<IActionResult> Create(int ingredientCount)
		{
			var model = new CocktailFormModel
			{
				Categories = await this.categoryService.GetAllCategoriesAsync(),
				CountOfIngredients = ingredientCount
			};

			return this.View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CocktailFormModel model)
		{
			if (!this.ModelState.IsValid)
			{
				model.Categories = await this.categoryService.GetAllCategoriesAsync();
				return this.View(model);
			}

			var userId = User.Claims
				.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

			if (userId != null && Guid.TryParse(userId.Value, out Guid id))
			{
				await this.cocktailService.CreateCocktailAsync(model, id);
			}

			return this.RedirectToAction("Index", "Home");
		}

		[AllowAnonymous]
		public async Task<IActionResult> All()
		{
			var cocktails = await this.cocktailService.GetCocktailDetailsAsync();

			return this.View(cocktails);
		}

		public async Task<IActionResult> ShowMore(int id)
		{
			var cocktail = await this.cocktailService.GetCocktailById(id);

			return this.View(cocktail);
		}

		[AllowAnonymous]
		public async Task<IActionResult> RandomCocktail()
		{
			var model = await this.cocktailService.GetRandomCocktailAsync();

			return this.View(model);
		}
	}
}

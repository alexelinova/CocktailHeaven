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
		[Authorize(Roles = "CocktailEditor")]
		public IActionResult AddIngredientCount()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "CocktailEditor")]
		public IActionResult AddIngredientCount(int count)
		{
			if (count <= 0)
			{
				return this.View();
			}

			return this.RedirectToAction(nameof(Create), new { ingredientCount = count });
		}

		[Authorize(Roles = "CocktailEditor")]
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

		[Authorize(Roles = "CocktailEditor")]
		public async Task<IActionResult> Edit(int id)
		{
			var cocktail = await cocktailService.GetCocktailByIdAsync(id);
			var categoyId = await cocktailService.GetCocktailCategoryAsync(id);

			var model = new CocktailEditModel()
			{
				Id = id,
				Name = cocktail.Name,
				Description = cocktail.Description,
				Instruction = cocktail.Instructions,
				Garnish = cocktail.Garnish,
				ImageURL = cocktail.Image,
				Ingredients = cocktail.Ingredients.ToList(),
				CategoryId = categoyId,
				Categories = await this.categoryService.GetAllCategoriesAsync(),
			};

			return this.View(model);
		}

		[HttpPost]
		[Authorize(Roles = "CocktailEditor")]
		public async Task<IActionResult> Edit(CocktailEditModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Categories = await categoryService.GetAllCategoriesAsync();
				return View(model);
			}

			await cocktailService.Edit(model, model.Id);

			return RedirectToAction(nameof(ShowMore), new { model.Id });
		}

		[HttpPost]
		[Authorize(Roles = "CocktailEditor")]
		public async Task<IActionResult> Delete(int id)
		{
			await cocktailService.Delete(id);

			return RedirectToAction(nameof(All));
		}


		[AllowAnonymous]
		public async Task<IActionResult> All(int id = 1)
		{
			const int ItemsPerPage = 4;

			var model = new CocktailAllViewModel()
			{
				CocktailsPerPage = ItemsPerPage,
				Cocktails = await this.cocktailService.GetCocktailDetailsAsync(id, ItemsPerPage),
				CocktailsCount = await this.cocktailService.CocktailCountAsync(),
				PageNumber = id
			};

			return this.View(model);
		}

		public async Task<IActionResult> ShowMore(int id)
		{
			var cocktail = await this.cocktailService.GetCocktailByIdAsync(id);

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

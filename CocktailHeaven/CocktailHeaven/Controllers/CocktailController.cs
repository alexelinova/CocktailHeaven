using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.Cocktail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CocktailHeaven.Extensions;
using CocktailHeaven.Core.Models.Ingredient;
using static CocktailHeaven.Infrastructure.Models.DataConstants.MessageConstant;

namespace CocktailHeaven.Controllers
{
	public class CocktailController : BaseController
	{
		private readonly ICategoryService categoryService;
		private readonly ICocktailService cocktailService;
		private readonly IUserCollectionService userCollectionService;
		public CocktailController(ICategoryService _categoryService,
			 ICocktailService _cocktailService,
			 IUserCollectionService _userCollectionService)
		{
			this.categoryService = _categoryService;
			this.cocktailService = _cocktailService;
			this.userCollectionService = _userCollectionService;
		}

		
		[Authorize(Roles = "Cocktail Editor")]
		public async Task<IActionResult> Create()
		{
			var model = new CocktailFormModel
			{
				Categories = await this.categoryService.GetAllCategoriesAsync(),
				Ingredients = new List<IngredientFormModel>
				{
					new IngredientFormModel(),
					new IngredientFormModel()
				}
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

			var userId = this.User.Id();
			await this.cocktailService.CreateCocktailAsync(model, userId);

			return this.RedirectToAction("Index", "Home");
		}

		[Authorize(Roles = "Cocktail Editor")]
		public async Task<IActionResult> Edit(int id)
		{
			if (await this.cocktailService.ExistsByIdAsync(id) == false)
			{
				this.TempData[ErrorMessage] = ErrorMessageCocktail;
				return this.RedirectToAction(nameof(All));
			}

			var cocktail = await this.cocktailService.GetCocktailByIdAsync(id);
			var categoyId = await this.cocktailService.GetCocktailCategoryAsync(id);

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
		[Authorize(Roles = "Cocktail Editor")]
		public async Task<IActionResult> Edit(CocktailEditModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Categories = await this.categoryService.GetAllCategoriesAsync();
				return this.View(model);
			}

			if(await this.cocktailService.ExistsByIdAsync(model.Id) ==  false)
			{
				this.ModelState.AddModelError("", ErrorMessageCocktail);
				return this.View(model);
			}

			await this.cocktailService.Edit(model, model.Id);

			return this.RedirectToAction(nameof(ShowMore), new { model.Id });
		}

		[HttpPost]
		[Authorize(Roles = "Cocktail Editor")]
		public async Task<IActionResult> Delete(int id)
		{
			if (await this.cocktailService.ExistsByIdAsync(id) == false)
			{
				this.TempData[ErrorMessage] = ErrorMessageCocktail;
				return this.RedirectToAction(nameof(All));
			}

			await this.cocktailService.Delete(id);

			return this.RedirectToAction(nameof(All));
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
			if (await this.cocktailService.ExistsByIdAsync(id) == false)
			{
				return this.RedirectToAction(nameof(All));
			}

			var model = new CocktailShowDetailsModel()
			{
				Cocktail = await this.cocktailService.GetCocktailByIdAsync(id),
				isInFavourites = await this.userCollectionService.IsCocktailInFavouritesAsync(User.Id(), id),
				isInTried = await this.userCollectionService.IsCocktailInTriedAsync(User.Id(), id),
				isInWishList = await this.userCollectionService.IsCocktailInWishListAsync(User.Id(), id)
			};

			return this.View(model);
		}

		[AllowAnonymous]
		public async Task<IActionResult> RandomCocktail()
		{
			var model = await this.cocktailService.GetRandomCocktailAsync();

			return this.View(model);
		}
	}
}

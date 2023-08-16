using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Extensions;
using Microsoft.AspNetCore.Mvc;
using static CocktailHeaven.Infrastructure.Models.DataConstants.MessageConstant;

namespace CocktailHeaven.Controllers
{
	public class UserCollectionController : BaseController
	{
		private readonly IUserCollectionService userCollectionService;
		private readonly ICocktailService cocktailService;

        public UserCollectionController(IUserCollectionService _userCollectionService, ICocktailService cocktailService)
        {
            this.userCollectionService = _userCollectionService;
			this.cocktailService = cocktailService;
        }

		public async Task<IActionResult> ShowFavourite()
		{
			var userId = User.Id();

			var favouriteCocktails = await this.userCollectionService.GetFavouriteCocktailsAsync(userId);

			return this.View(favouriteCocktails);
		}

		public async Task<IActionResult> ShowTried()
		{
			var userId = User.Id();
		
			var triedCocktails = await this.userCollectionService.GetTriedCocktailsAsync(userId);

			return this.View(triedCocktails);
		}

		public async Task<IActionResult> ShowWishlist()
		{
			var userId = User.Id();

			var wishlistCocktails = await this.userCollectionService.GetWishlistCocktailsAsync(userId);

			return this.View(wishlistCocktails);
		}


		public async Task<IActionResult> AddToFavourite(int id)
		{
			if (await this.cocktailService.ExistsByIdAsync(id) == false)
			{
				this.TempData[ErrorMessage] = ErrorMessageCocktail;
				return this.RedirectToAction("All", "Cocktail");
			}

			var userId = this.User.Id();

			if(await this.userCollectionService.IsCocktailInFavouritesAsync(userId, id))
			{
				return this.RedirectToAction("ShowMore", "Cocktail", new { id });
			}

			await this.userCollectionService.AddToFavouriteAsync(userId, id);

			return this.RedirectToAction(nameof(ShowFavourite));
		}

		public async Task<IActionResult> AddToWishlist(int id)
		{
			if (await this.cocktailService.ExistsByIdAsync(id) == false)
			{
				this.TempData[ErrorMessage] = ErrorMessageCocktail;
				return this.RedirectToAction("All", "Cocktail");
			}

			var userId = this.User.Id();

			if (await this.userCollectionService.IsCocktailInWishListAsync(userId, id))
			{
				return this.RedirectToAction("ShowMore", "Cocktail", new { id });
			}

			await this.userCollectionService.AddToWishListAsync(userId, id);

			return this.RedirectToAction(nameof(ShowWishlist));
		}

	
		public async Task<IActionResult> AddToTried(int id)
		{
			if (await this.cocktailService.ExistsByIdAsync(id) == false)
			{
				this.TempData[ErrorMessage] = ErrorMessageCocktail;
				return this.RedirectToAction("All", "Cocktail");
			}

			var userId = this.User.Id();

			if (await this.userCollectionService.IsCocktailInTriedAsync(userId, id))
			{
				return RedirectToAction("ShowMore", "Cocktail", new { id });
			}

			await this.userCollectionService.AddToTriedAsync(userId, id);

			return this.RedirectToAction(nameof(ShowTried));
		}

		public async Task<IActionResult> RemoveFromWishlist(int id)
		{
			var userId = User.Id();

			if (await this.userCollectionService.IsCocktailInWishListAsync(userId, id) == false)
			{
				this.TempData[ErrorMessage] = ErrorMessageUserCollection;
				return this.RedirectToAction(nameof(ShowWishlist));
			}

			await this.userCollectionService.RemoveFromWishListAsync(userId, id);

			return this.RedirectToAction(nameof(ShowWishlist));
		}

		public async Task<IActionResult> RemoveFromTried(int id)
		{
			var userId = User.Id();

			if (await this.userCollectionService.IsCocktailInTriedAsync(userId, id) == false)
			{
				this.TempData[ErrorMessage] = ErrorMessageUserCollection;
				return this.RedirectToAction(nameof(ShowTried));
			}

			await this.userCollectionService.RemoveFromTriedAsync(userId, id);

			return this.RedirectToAction(nameof(ShowTried));
		}

		public async Task<IActionResult> RemoveFromFavourite(int id)
		{
			var userId = User.Id();

			if (await this.userCollectionService.IsCocktailInFavouritesAsync(userId, id) == false )
			{
				this.TempData[ErrorMessage] = ErrorMessageUserCollection;
				return this.RedirectToAction(nameof(ShowFavourite));
			}

			await this.userCollectionService.RemoveFromFavouriteAsync(userId, id);

			return this.RedirectToAction(nameof(ShowFavourite));
		}
	}
}

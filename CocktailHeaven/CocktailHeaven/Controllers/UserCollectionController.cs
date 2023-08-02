using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.Cocktail;
using CocktailHeaven.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CocktailHeaven.Controllers
{
	public class UserCollectionController : BaseController
	{
		private readonly IUserCollectionService userCollectionService;

        public UserCollectionController(IUserCollectionService _userCollectionService)
        {
            this.userCollectionService = _userCollectionService;
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
			var userId = this.User.Id();

			if(await this.userCollectionService.IsCocktailInFavouritesAsync(userId, id))
			{
				return RedirectToAction("ShowMore", "Cocktail", new { id });
			}

			await this.userCollectionService.AddToFavouriteAsync(userId, id);

			return RedirectToAction(nameof(ShowFavourite));
		}

		public async Task<IActionResult> AddToWishlist(int id)
		{
			var userId = this.User.Id();

			if (await this.userCollectionService.IsCocktailInWishListAsync(userId, id))
			{
				return RedirectToAction("ShowMore", "Cocktail", new { id });
			}

			await this.userCollectionService.AddToWishListAsync(userId, id);

			return RedirectToAction(nameof(ShowWishlist));
		}

	
		public async Task<IActionResult> AddToTried(int id)
		{
			var userId = this.User.Id();

			if (await this.userCollectionService.IsCocktailInTriedAsync(userId, id))
			{
				return RedirectToAction("ShowMore", "Cocktail", new { id });
			}

			await this.userCollectionService.AddToTriedAsync(userId, id);

			return RedirectToAction(nameof(ShowTried));
		}

		public async Task<IActionResult> RemoveFromWishlist(int id)
		{
			var userId = User.Id();

			await this.userCollectionService.RemoveFromWishListAsync(userId, id);

			return RedirectToAction(nameof(ShowWishlist));
		}

		public async Task<IActionResult> RemoveFromTried(int id)
		{
			var userId = User.Id();

			await this.userCollectionService.RemoveFromTriedAsync(userId, id);

			return RedirectToAction(nameof(ShowTried));
		}

		public async Task<IActionResult> RemoveFromFavourite(int id)
		{
			var userId = User.Id();

			await this.userCollectionService.RemoveFromFavouriteAsync(userId, id);

			return RedirectToAction(nameof(ShowFavourite));

		}
	}
}

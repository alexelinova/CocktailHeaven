using CocktailHeaven.Core.Contracts;
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

		public async Task<IActionResult> UserCollections()
		{
			return this.View();
		}

		
		public async Task<IActionResult> AddToFavourite(int id)
		{
			var userId = this.User.Id();

			if(await this.userCollectionService.IsCocktailInFavourites(userId, id))
			{
				return RedirectToAction("ShowMore", "Cocktail", new { id });
			}

			await this.userCollectionService.AddToFavourite(userId, id);

			return RedirectToAction(nameof(UserCollections));
		}

		public async Task<IActionResult> AddToWishlist(int id)
		{
			var userId = this.User.Id();

			if (await this.userCollectionService.IsCocktailInWishList(userId, id))
			{
				return RedirectToAction("ShowMore", "Cocktail", new { id });
			}

			await this.userCollectionService.AddToWishList(userId, id);

			return RedirectToAction(nameof(UserCollections));
		}

	
		public async Task<IActionResult> AddToTried(int id)
		{
			var userId = this.User.Id();

			if (await this.userCollectionService.IsCocktailInWishList(userId, id))
			{
				return RedirectToAction("ShowMore", "Cocktail", new { id });
			}

			await this.userCollectionService.AddToTried(userId, id);

			return RedirectToAction(nameof(UserCollections));
		}
	}
}

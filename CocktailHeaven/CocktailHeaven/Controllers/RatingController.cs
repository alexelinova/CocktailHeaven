using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.Rating;
using CocktailHeaven.Extensions;
using Microsoft.AspNetCore.Mvc;
using static CocktailHeaven.Infrastructure.Models.DataConstants.MessageConstant;

namespace CocktailHeaven.Controllers
{
	public class RatingController : BaseController
	{
		private readonly IRatingService ratingService;
		private readonly ICocktailService cocktailService;

		public RatingController(IRatingService ratingService, ICocktailService cocktailService)
		{
			this.ratingService = ratingService;
			this.cocktailService = cocktailService;
		}

		[HttpPost]
		public async Task<IActionResult> Rate(RateCocktailModel model)
		{
			if (await cocktailService.ExistsByIdAsync(model.CocktailId) == false)
			{
				ModelState.AddModelError(nameof(model.CocktailId), ErrorMessageCocktail);
			}

			if (!this.ModelState.IsValid)
			{
				return RedirectToAction("ShowTried", "UserCollection");
			}

			var userId = User.Id();
			await ratingService.RateAsync(model.CocktailId, userId, model.Value, model.Comment);

			return RedirectToAction("ShowTried", "UserCollection");
		}
	}
}

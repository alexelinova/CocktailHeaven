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
			if (await this.cocktailService.ExistsByIdAsync(model.CocktailId) == false
				|| !this.ModelState.IsValid)
			{
				this.TempData[ErrorMessage] = "Invalid rating data provided";
				return this.RedirectToAction("ShowTried", "UserCollection");
			}

			var userId = User.Id();
			await this.ratingService.RateAsync(model.CocktailId, userId, model.Value, model.Comment);

			return this.RedirectToAction("ShowTried", "UserCollection");
		}
	}
}

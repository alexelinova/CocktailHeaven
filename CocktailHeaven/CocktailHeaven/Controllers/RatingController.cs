using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CocktailHeaven.Controllers
{
	public class RatingController : BaseController
	{
		private readonly IRatingService ratingService;

		public RatingController(IRatingService ratingService)
		{
			this.ratingService = ratingService;
		}

		[HttpPost]
		public async Task<IActionResult> Rate(int cocktailId, int value, string? comment)
		{
			var userId = User.Id();
			await ratingService.RateAsync(cocktailId, userId, value, comment);

			return RedirectToAction("ShowTried", "UserCollection");
		}
	}
}

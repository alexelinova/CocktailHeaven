using CocktailHeaven.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using static CocktailHeaven.Infrastructure.Models.DataConstants.MessageConstant;

namespace CocktailHeaven.Areas.Admin.Controllers
{
    public class RatingController : BaseAdminController
    {
        private readonly IRatingService ratingService;

        public RatingController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

        public async Task<IActionResult> All()
        {
            var model = await this.ratingService.GetAllRatingsAsync();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRating(int id)
        {
            if (await this.ratingService.RatingExistsAsync(id) == false)
            {
                this.TempData[ErrorMessage] = ErrorMessageRating;
                this.RedirectToAction("Index", "Home");
            }

            await this.ratingService.DeleteRating(id);

            return this.RedirectToAction(nameof(All));
        }
    }
}

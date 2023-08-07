using CocktailHeaven.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

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

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRating(int id)
        {
            if (await this.ratingService.RatingExistsAsync(id) == false)
            {
                return NotFound();
            }

            await this.ratingService.DeleteRating(id);

            return RedirectToAction("All");
        }
    }
}

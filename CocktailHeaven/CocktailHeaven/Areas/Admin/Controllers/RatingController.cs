using CocktailHeaven.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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

            try
            {
				var model = await this.ratingService.GetAllRatingsAsync();
				return View(model);
			}
            catch (ArgumentException ex)
            {

                TempData[ErrorMessage] = ex.Message;
            }

			return RedirectToAction("Index", "Home");

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

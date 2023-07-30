using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.Search;
using Microsoft.AspNetCore.Mvc;

namespace CocktailHeaven.Controllers
{
	public class SearchController : BaseController
	{
		private readonly ICocktailService cocktailService;
		private readonly ICategoryService categoryService;

		public SearchController(ICocktailService _cocktailService, ICategoryService _categoryService)
		{
			this.cocktailService = _cocktailService;
			this.categoryService = _categoryService;
		}
		public async Task<IActionResult> Index([FromQuery] SearchViewModel model)
		{
			var viewModel = new SearchViewModel()
			{
				Cocktails = await this.cocktailService.Search(model.SearchQuery, model.SearchCriteria, model.Category),
				Categories = (await this.categoryService.GetAllCategoriesAsync()).Select(c => c.Name).ToList()
			};

			return View(viewModel);
		}
	}
}

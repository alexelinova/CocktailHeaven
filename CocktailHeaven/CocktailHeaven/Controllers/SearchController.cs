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
			const int itemsPerPage = 5;

			var viewModel = await this.cocktailService.Search(model.SearchQuery, model.SearchCriteria, model.Category, model.PageNumber, itemsPerPage);

			viewModel.Categories = (await this.categoryService.GetAllCategoriesAsync()).Select(c => c.Name).ToList();
			viewModel.SearchCriteria = model.SearchCriteria;
			viewModel.SearchQuery = model.SearchQuery;
			viewModel.Category = model.Category;
			viewModel.CocktailsPerPage = itemsPerPage;
			viewModel.PageNumber = model.PageNumber;

			return View(viewModel);
		}
	}
}

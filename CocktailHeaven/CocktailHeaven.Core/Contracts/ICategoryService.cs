using CocktailHeaven.Core.Models.Category;

namespace CocktailHeaven.Core.Contracts
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
	}
}

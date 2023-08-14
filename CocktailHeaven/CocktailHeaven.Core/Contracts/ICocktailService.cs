using CocktailHeaven.Core.Models.Cocktail;
using CocktailHeaven.Core.Models.Search;

namespace CocktailHeaven.Core.Contracts
{
	public interface ICocktailService
	{
		Task<int> CreateCocktailAsync(CocktailFormModel model, Guid userId);

		Task<bool> ExistsByIdAsync(int cocktailId);

		Task<bool> ExistsByNameAsync(string cocktailName);

		Task<CocktailFullModel> GetRandomCocktailAsync();

		Task<IEnumerable<CocktailDetailsModel>> GetCocktailDetailsAsync(int page, int itemsPerPage);

		Task<CocktailFullModel> GetCocktailByIdAsync(int id);

		Task<int> CocktailCountAsync();

		Task<IEnumerable<CocktailCollectionModel>> GetTopRatedCocktailsAsync();

		Task<int> GetCocktailCategoryAsync(int cocktailId);

		Task EditAsync(CocktailEditModel model, int cocktailId);

		Task DeleteAsync(int cocktailId);

		Task<SearchViewModel> SearchAsync(string? queryString, SearchCriteria? searchCriteria, string? categoryName, int currentPage, int itemsPerPage);
	}
}

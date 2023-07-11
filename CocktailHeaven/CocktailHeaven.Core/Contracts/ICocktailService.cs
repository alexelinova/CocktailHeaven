using CocktailHeaven.Core.Models.Cocktail;

namespace CocktailHeaven.Core.Contracts
{
	public interface ICocktailService
	{
		Task CreateCocktailAsync(CocktailFormModel model, Guid userId);

		Task<RandomCocktailModel> GetRandomCocktailAsync();

		Task<IEnumerable<CocktailDetailsModel>> GetCocktailDetailsAsync();
	}
}

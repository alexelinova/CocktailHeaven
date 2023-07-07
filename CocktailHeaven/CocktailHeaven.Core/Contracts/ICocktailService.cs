using CocktailHeaven.Core.Models.Cocktail;

namespace CocktailHeaven.Core.Contracts
{
	public interface ICocktailService
	{
		Task CreateCocktailAsync(AddCocktailFormModel model, Guid userId);
	}
}

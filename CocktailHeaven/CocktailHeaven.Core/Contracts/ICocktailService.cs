﻿using CocktailHeaven.Core.Models.Cocktail;

namespace CocktailHeaven.Core.Contracts
{
	public interface ICocktailService
	{
		Task CreateCocktailAsync(CocktailFormModel model, Guid userId);

		Task<CocktailFullModel> GetRandomCocktailAsync();

		Task<IEnumerable<CocktailDetailsModel>> GetCocktailDetailsAsync(int page, int itemsPerPage);

		Task<CocktailFullModel> GetCocktailByIdAsync(int id);

		Task<int> CocktailCountAsync();

		Task<IEnumerable<CocktailCollectionModel>> GetTopRatedCocktailsAsync();

		Task<int> GetCocktailCategoryAsync(int cocktailId);

		Task Edit(CocktailEditModel model, int cocktailId);

		Task Delete(int cocktailId);
	}
}

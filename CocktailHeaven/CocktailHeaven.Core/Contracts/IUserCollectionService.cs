using CocktailHeaven.Core.Models.Cocktail;
using CocktailHeaven.Infrastructure.Models;

namespace CocktailHeaven.Core.Contracts
{
	public interface IUserCollectionService
	{
		Task AddToWishListAsync(Guid userId, int cocktailId);

		Task AddToFavouriteAsync(Guid userId, int cocktailId);

		Task AddToTriedAsync(Guid userId, int cocktailId);

		Task RemoveFromWishListAsync(Guid userId, int cocktailId);

		Task RemoveFromTriedAsync(Guid userId, int cocktailId);

		Task RemoveFromFavouriteAsync(Guid userId, int cocktailId);

		Task<bool> IsCocktailInFavouritesAsync(Guid userId, int cocktailId);

		Task<bool> IsCocktailInTriedAsync(Guid userId, int cocktailId);

		Task<bool> IsCocktailInWishListAsync(Guid userId, int cocktailId);

		Task<IEnumerable<CocktailCollectionModel>> GetFavouriteCocktailsAsync(Guid userId);

        Task<IEnumerable<CocktailTriedRatingModel>> GetTriedCocktailsAsync(Guid userId);

        Task<IEnumerable<CocktailCollectionModel>> GetWishlistCocktailsAsync(Guid userId);
    }
}

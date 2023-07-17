using CocktailHeaven.Infrastructure.Models;

namespace CocktailHeaven.Core.Contracts
{
	public interface IUserCollectionService
	{
		Task AddToWishList(Guid userId, int cocktailId);

		Task AddToFavourite(Guid userId, int cocktailId);

		Task AddToTried(Guid userId, int cocktailId);

		Task<UserCollection> FindOrCreateUserCollection(Guid userId, int cocktailId);

		Task RemoveFromWishList(Guid userId, int cocktailId);

		Task RemoveFromTried(Guid userId, int cocktailId);

		Task RemoveFromFavourite(Guid userId, int cocktailId);

		Task<bool> IsCocktailInFavourites(Guid userId, int cocktailId);

		Task<bool> IsCocktailInTried(Guid userId, int cocktailId);

		Task<bool> IsCocktailInWishList(Guid userId, int cocktailId);

		Task UpdateUserCollectionProperty(Guid userId, int cocktailId, Action<UserCollection> updateCollection);
	}
}

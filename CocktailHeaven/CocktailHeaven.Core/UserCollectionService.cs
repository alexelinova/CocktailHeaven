using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CocktailHeaven.Core
{
	public class UserCollectionService : IUserCollectionService
	{
		private readonly IRepository repo;
		public UserCollectionService(IRepository repo)
		{
			this.repo = repo;
		}

		public async Task AddToFavourite(Guid userId, int cocktailId)
		{
			var userCollection = await this.FindOrCreateUserCollection(userId, cocktailId);

			userCollection.IsFavourite = true;
			await this.repo.SaveChangesAsync();
		}

		public async Task AddToTried(Guid userId, int cocktailId)
		{
			var userCollection = await this.FindOrCreateUserCollection(userId, cocktailId);

			userCollection.HasTried = true;
			await this.repo.SaveChangesAsync();
		}

		public async Task AddToWishList(Guid userId, int cocktailId)
		{
			var userCollection = await this.FindOrCreateUserCollection(userId, cocktailId);

			userCollection.WishList = true;
			await this.repo.SaveChangesAsync();
		}

		public async Task<UserCollection> FindOrCreateUserCollection(Guid userId, int cocktailId)
		{
			var userCollection = await this.repo
				.All<UserCollection>()
				.FirstOrDefaultAsync(uc => uc.AddedByUserId == userId 
				&& uc.CocktailId == cocktailId);

			if (userCollection == null)
			{
				userCollection = new UserCollection()
				{
					AddedByUserId = userId,
					CocktailId = cocktailId,
				};

				await this.repo.AddAsync(userCollection);
			}

			return userCollection;
		}

		public async Task<bool> IsCocktailInFavourites(Guid userId, int cocktailId)
		{
			return await this.repo.AllReadonly<UserCollection>()
				.AnyAsync(u => u.AddedByUserId == userId && u.CocktailId == cocktailId && u.IsFavourite == true);
		}

		public async Task<bool> IsCocktailInTried(Guid userId, int cocktailId)
		{
			return await this.repo.AllReadonly<UserCollection>()
				.AnyAsync(u => u.AddedByUserId == userId && u.CocktailId == cocktailId && u.HasTried == true);
		}

		public async Task<bool> IsCocktailInWishList(Guid userId, int cocktailId)
		{
			return await this.repo.AllReadonly<UserCollection>()
				.AnyAsync(u => u.AddedByUserId == userId && u.CocktailId == cocktailId && u.WishList == true);
		}

		public Task RemoveFromFavourite(Guid userId, int cocktailId)
		{
			return UpdateUserCollectionProperty(userId, cocktailId, uc => uc.IsFavourite = false);
		}

		public Task RemoveFromTried(Guid userId, int cocktailId)
		{
			return UpdateUserCollectionProperty(userId, cocktailId, uc => uc.HasTried = false);
		}

		public Task RemoveFromWishList(Guid userId, int cocktailId)
		{
			return UpdateUserCollectionProperty(userId, cocktailId, uc => uc.WishList = false);
		}

		public async Task UpdateUserCollectionProperty(Guid userId, int cocktailId, Action<UserCollection> updateCollection)
		{
			var userCollection = await this.repo
				.All<UserCollection>()
				.FirstOrDefaultAsync(uc => uc.AddedByUserId == userId && uc.CocktailId == cocktailId);

			if (userCollection != null)
			{
				updateCollection(userCollection);
				await this.repo.SaveChangesAsync();
			}
		}
	}
}

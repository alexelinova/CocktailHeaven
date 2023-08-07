using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.Cocktail;
using CocktailHeaven.Core.Models.NewFolder;
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

		public async Task AddToFavouriteAsync(Guid userId, int cocktailId)
		{
			var userCollection = await this.GetUserCollectionAsync(userId, cocktailId);

			if (userCollection == null)
			{
				userCollection= await CreateNewUserCollectionAsync(userId, cocktailId);
			}

			userCollection.IsFavourite = true;
			await this.repo.SaveChangesAsync();
		}

		public async Task AddToTriedAsync(Guid userId, int cocktailId)
		{
			var userCollection = await this.GetUserCollectionAsync(userId, cocktailId);

			if (userCollection == null)
			{
				userCollection = await CreateNewUserCollectionAsync(userId, cocktailId);
			}

			userCollection.HasTried = true;
			await this.repo.SaveChangesAsync();
		}

		public async Task AddToWishListAsync(Guid userId, int cocktailId)
		{
			var userCollection = await this.GetUserCollectionAsync(userId, cocktailId);

			if (userCollection == null)
			{
				userCollection = await CreateNewUserCollectionAsync(userId, cocktailId);
			}

			userCollection.WishList = true;
			await this.repo.SaveChangesAsync();
		}

		public async Task<IEnumerable<CocktailCollectionModel>> GetFavouriteCocktailsAsync(Guid userId)
		{
			return await this.repo.AllReadonly<UserCollection>(uc => uc.AddedByUserId == userId && uc.IsFavourite == true)
				 .Select(uc => new CocktailCollectionModel()
				 {
					 Id = uc.CocktailId,
					 Name = uc.Cocktail.Name,
					 ImageUrl = uc.Cocktail.Image.ExternalURL ?? string.Empty
				 })
				 .OrderBy(uc => uc.Name)
				 .ToListAsync();
		}

		public async Task<IEnumerable<CocktailTriedRatingModel>> GetTriedCocktailsAsync(Guid userId)
		{
			return await this.repo.AllReadonly<UserCollection>(uc => uc.AddedByUserId == userId && uc.HasTried == true)
				.Select(uc => new CocktailTriedRatingModel()
				{
					Id = uc.CocktailId,
					Name = uc.Cocktail.Name,
					ImageUrl = uc.Cocktail.Image.ExternalURL ?? string.Empty,
					Rating = uc.Cocktail.Ratings.Where(r => r.AddedByUserId == userId && r.IsDeleted == false)
					.Select(r => new RatingFormModel()
					{
						Value = r.Value,
						Comment = r.Comment,
						CreatedOn = r.CreatedOn,
						Username = r.AddedByUser.UserName
					})
					.FirstOrDefault()
				})
				.OrderBy(uc => uc.Name)
				.ToListAsync();
		}

		public async Task<IEnumerable<CocktailCollectionModel>> GetWishlistCocktailsAsync(Guid userId)
		{
			return await this.repo.AllReadonly<UserCollection>(uc => uc.AddedByUserId == userId && uc.WishList == true)
		   .Select(uc => new CocktailCollectionModel()
		   {
			   Id = uc.CocktailId,
			   Name = uc.Cocktail.Name,
			   ImageUrl = uc.Cocktail.Image.ExternalURL ?? string.Empty
		   })
		   .OrderBy(uc => uc.Name)
		   .ToListAsync();
		}

		public async Task<bool> IsCocktailInFavouritesAsync(Guid userId, int cocktailId)
		{
			return await this.repo.AllReadonly<UserCollection>()
				.AnyAsync(u => u.AddedByUserId == userId && u.CocktailId == cocktailId && u.IsFavourite == true);
		}

		public async Task<bool> IsCocktailInTriedAsync(Guid userId, int cocktailId)
		{
			return await this.repo.AllReadonly<UserCollection>()
				.AnyAsync(u => u.AddedByUserId == userId && u.CocktailId == cocktailId && u.HasTried == true);
		}

		public async Task<bool> IsCocktailInWishListAsync(Guid userId, int cocktailId)
		{
			return await this.repo.AllReadonly<UserCollection>()
				.AnyAsync(u => u.AddedByUserId == userId && u.CocktailId == cocktailId && u.WishList == true);
		}

		public async Task RemoveFromFavouriteAsync(Guid userId, int cocktailId)
		{
			var userCollection = await GetUserCollectionAsync(userId, cocktailId);

			if (userCollection == null)
			{
				return;
			}

			userCollection.IsFavourite = null;

			if (IsEmptyCollectionAsync(userCollection))
			{
				this.repo.Delete<UserCollection>(userCollection);
			}

			await this.repo.SaveChangesAsync();
		}

		public async Task RemoveFromTriedAsync(Guid userId, int cocktailId)
		{
			var userCollection = await GetUserCollectionAsync(userId, cocktailId);

			if (userCollection == null)
			{
				return;
			}

			userCollection.HasTried = null;

			if (IsEmptyCollectionAsync(userCollection))
			{
				this.repo.Delete<UserCollection>(userCollection);
			}
			await this.repo.SaveChangesAsync();
		}

		public async Task RemoveFromWishListAsync(Guid userId, int cocktailId)
		{
			var userCollection = await GetUserCollectionAsync(userId, cocktailId);

			if (userCollection == null)
			{
				return;
			}

			userCollection.WishList = null;

			if (IsEmptyCollectionAsync(userCollection))
			{
				this.repo.Delete<UserCollection>(userCollection);
			}

			await this.repo.SaveChangesAsync();
		}

		private async Task<UserCollection?> GetUserCollectionAsync(Guid userId, int cocktailId)
		{
			var userCollection = await this.repo
				.All<UserCollection>()
				.FirstOrDefaultAsync(uc => uc.AddedByUserId == userId && uc.CocktailId == cocktailId);

			return userCollection;
		}

		private async Task<UserCollection> CreateNewUserCollectionAsync(Guid userId, int cocktailId)
		{
			var userCollection = new UserCollection()
			{
				AddedByUserId = userId,
				CocktailId = cocktailId,
			};

			await this.repo.AddAsync(userCollection);

			return userCollection;
		}

		private bool IsEmptyCollectionAsync(UserCollection userCollection)
		{
			return userCollection.WishList == null
				&& userCollection.IsFavourite == null
				&& userCollection.HasTried == null;
		}
	}
}

using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.Cocktail;
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
            var userCollection = await this.FindOrCreateUserCollectionAsync(userId, cocktailId);

            userCollection.IsFavourite = true;
            await this.repo.SaveChangesAsync();
        }

        public async Task AddToTriedAsync(Guid userId, int cocktailId)
        {
            var userCollection = await this.FindOrCreateUserCollectionAsync(userId, cocktailId);

            userCollection.HasTried = true;
            await this.repo.SaveChangesAsync();
        }

        public async Task AddToWishListAsync(Guid userId, int cocktailId)
        {
            var userCollection = await this.FindOrCreateUserCollectionAsync(userId, cocktailId);

            userCollection.WishList = true;
            await this.repo.SaveChangesAsync();
        }

        public async Task<UserCollection> FindOrCreateUserCollectionAsync(Guid userId, int cocktailId)
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

        public async Task<IEnumerable<CocktailCollectionModel>> GetTriedCocktailsAsync(Guid userId)
        {
            return await this.repo.AllReadonly<UserCollection>(uc => uc.AddedByUserId == userId && uc.HasTried == true)
                .Select(uc => new CocktailCollectionModel()
                {
                    Id = uc.CocktailId,
                    Name = uc.Cocktail.Name,
                    ImageUrl = uc.Cocktail.Image.ExternalURL ?? string.Empty
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

        public Task RemoveFromFavouriteAsync(Guid userId, int cocktailId)
        {
            return UpdateUserCollectionPropertyAsync(userId, cocktailId, uc => uc.IsFavourite = false);
        }

        public Task RemoveFromTriedAsync(Guid userId, int cocktailId)
        {
            return UpdateUserCollectionPropertyAsync(userId, cocktailId, uc => uc.HasTried = false);
        }

        public Task RemoveFromWishListAsync(Guid userId, int cocktailId)
        {
            return UpdateUserCollectionPropertyAsync(userId, cocktailId, uc => uc.WishList = false);
        }

        public async Task UpdateUserCollectionPropertyAsync(Guid userId, int cocktailId, Action<UserCollection> updateCollection)
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

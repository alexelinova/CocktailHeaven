using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.ApplicationUser;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Models;
using CocktailHeaven.Infrastructure.Models.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static CocktailHeaven.Infrastructure.Models.DataConstants.MessageConstant;

namespace CocktailHeaven.Core
{
    public class UserService : IUserService
    {
        private readonly IRepository repo;

        public UserService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await this.repo
             .All<ApplicationUser>(au => au.Id == userId)
             .Include(au => au.UserCollection)
             .Include(au => au.Ratings)
             .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException(ErrorMessageUser);
            }

            foreach (var userCollection in user.UserCollection)
            {
                this.repo.Delete<UserCollection>(userCollection);
            }

            foreach (var rating in user.Ratings)
            {
                rating.IsDeleted = true;
            }

            user.UserName = null;
            user.NormalizedUserName = null;
            user.Email = null;
            user.NormalizedEmail = null;
            user.EmailConfirmed = false;
            user.PasswordHash = null;
            user.SecurityStamp = null;
            user.ConcurrencyStamp = null;
            user.LockoutEnd = null;
            user.LockoutEnabled = false;
            user.AccessFailedCount = 0;
            user.IsDeleted = true;

            await this.repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            return await this.repo.AllReadonly<ApplicationUser>()
                .Where(au => au.IsDeleted == false)
                .Select(au => new UserViewModel()
                {
                    Id = au.Id,
                    Username = au.UserName,
                    Email = au.Email,
                })
                .OrderBy(au => au.Email)
                .ToListAsync();
        }

        public async Task<bool> UserExistsAsync(Guid userId)
        {
            var user = await this.repo
             .AllReadonly<ApplicationUser>(au => au.IsDeleted == false && au.Id == userId)
             .FirstOrDefaultAsync();

            return user != null;
        }
	}
}

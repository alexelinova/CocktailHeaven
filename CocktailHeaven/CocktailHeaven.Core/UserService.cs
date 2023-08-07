﻿using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.ApplicationUser;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Models;
using CocktailHeaven.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CocktailHeaven.Core
{
    public class UserService : IUserService
    {
        private readonly IRepository repo;

        private readonly UserManager<ApplicationUser> manager;

        public UserService(IRepository repo, UserManager<ApplicationUser> manager)
        {
            this.repo = repo;
            this.manager = manager;
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
                return;
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
            return await repo.AllReadonly<ApplicationUser>()
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

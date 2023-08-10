using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CocktailHeaven.Core
{
	public class RoleService : IRoleService
	{
		private readonly UserManager<ApplicationUser> userManager;

		private readonly RoleManager<IdentityRole<Guid>> roleManager;

		private readonly IRepository repo;

		public RoleService(UserManager<ApplicationUser> userManager, IRepository repo, RoleManager<IdentityRole<Guid>> roleManager)
		{
			this.userManager = userManager;
			this.repo = repo;
			this.roleManager = roleManager;
		}

		public async Task AssignRoleAsync(Guid userId, string roleName)
		{
			var user = await this.repo.GetByIdAsync<ApplicationUser>(userId);

			await this.userManager.AddToRoleAsync(user, roleName);
		}

		public async Task<IEnumerable<string>> GetRolesAsync()
		{
			var roles = await this.roleManager.Roles.Select(role => role.Name).ToListAsync();

			return roles;
		}

		public async Task RemoveUserFromRoleAsync(Guid userId, string roleName)
		{
			var user = await this.repo.GetByIdAsync<ApplicationUser>(userId);

			await this.userManager.RemoveFromRoleAsync(user, roleName);
		}

		public async Task<bool> RoleExists(string roleName)
		{
			return await this.roleManager.RoleExistsAsync(roleName);
		}
	}
}

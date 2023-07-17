using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CocktailHeaven.Core
{
	public class AccountService : IAccountService
	{
		private readonly IRepository repo;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> userManager;

		public AccountService(IRepository repo,
			SignInManager<ApplicationUser> signInManager,
			UserManager<ApplicationUser> userManager)
		{
			this.repo = repo;
			this.signInManager = signInManager;
			this.userManager = userManager;
		}
		public async Task<IdentityResult> RegisterUserAsync(
			string username,
			string email,
			string password)
		{
			bool emailExists = await UserExistsAsync(email);

			if (emailExists)
			{
				return IdentityResult.Failed(new IdentityError { Code = "DuplicateEmail", Description = "Email address is already registered." });
			}

			var user = new ApplicationUser()
			{
				Email = email,
				UserName = username,
			};

			var result = await this.userManager.CreateAsync(user, password);

			return result;
		}

		public async Task<SignInResult> SignInUserAsync(string email, string password)
		{
			var user = await this.repo
				.AllReadonly<ApplicationUser>(a => a.Email == email && a.IsDeleted == false)
				.FirstOrDefaultAsync();

			if (user == null)
			{
				return SignInResult.Failed;
			}

			return await this.signInManager.PasswordSignInAsync(user, password, false, false);
		}

		public async Task SignOutUserAsync()
		{
			await this.signInManager.SignOutAsync();
		}

		public async Task<bool> UserExistsAsync(string email)
		{
			var user = await this.repo.AllReadonly<ApplicationUser>
				(x => x.Email == email)
				.FirstOrDefaultAsync();

			return user != null;
		}
	}
}

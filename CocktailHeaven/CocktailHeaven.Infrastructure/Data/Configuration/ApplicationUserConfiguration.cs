using CocktailHeaven.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailHeaven.Infrastructure.Data.Configuration
{
	public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			//builder.HasData(SeedUsers());
		}

		//private List<ApplicationUser> SeedUsers()
		//{
		//	var users = new List<ApplicationUser>();
		//	var hasher = new PasswordHasher<ApplicationUser>();

		//	var firstUser = new ApplicationUser()
		//	{
		//		Id = Guid.Parse("d273e367-ebf6-44b3-afa7-7759a0a579ee"),
		//		UserName = "George",
		//		NormalizedUserName = "GEORGE",
		//		Email = "gmihov@mail.com",
		//		NormalizedEmail = "GMIHOV@MAIL.COM",
		//		SecurityStamp = Guid.NewGuid().ToString("D")
		//	};

		//	firstUser.PasswordHash = hasher.HashPassword(firstUser, "Gmihov123");
		//	users.Add(firstUser);

		//	var secondUser = new ApplicationUser()
		//	{
		//		Id = Guid.Parse("9622851e-71b4-439b-8d86-9e8cba27ca1e"),
		//		UserName = "Dev",
		//		NormalizedUserName = "DEV",
		//		Email = "devora@mail.com",
		//		NormalizedEmail = "DEVORA@MAIL.COM",
		//		SecurityStamp = Guid.NewGuid().ToString("D")
		//	};

		//	secondUser.PasswordHash = hasher.HashPassword(secondUser, "Devora123");
		//	users.Add(secondUser);

		//	var thirdUser = new ApplicationUser()
		//	{
		//		Id = Guid.Parse("4e797b0b-c669-4bf1-913c-a90fe951241f"),
		//		UserName = "aleks",
		//		NormalizedUserName = "ALEKS",
		//		Email = "aleks@mail.com",
		//		NormalizedEmail = "ALEKS@MAIL.COM",
		//		SecurityStamp = Guid.NewGuid().ToString("D")
		//	};

		//	thirdUser.PasswordHash = hasher.HashPassword(thirdUser, "Aleks123");
		//	users.Add(thirdUser);

		//	return users;
		//}
	}
}

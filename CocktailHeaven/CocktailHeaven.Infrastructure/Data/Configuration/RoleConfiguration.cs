using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailHeaven.Infrastructure.Data.Configuration
{
	public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
	{
		public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
		{
			//builder.HasData(SeedRoles());
		}

		//private List<IdentityRole<Guid>> SeedRoles()
		//{
		//	var roles = new List<IdentityRole<Guid>>()
		//	{
		//		new IdentityRole<Guid>()
		//		{
		//			Id = Guid.Parse("ebabec4b-1413-4d79-8d3a-b55742b6f7b6"),
		//			Name = "Admin",
		//			NormalizedName = "ADMIN"
		//		},

		//		new IdentityRole<Guid>()
		//		{
		//			Id = Guid.Parse("ed23ddd6-0cab-4a38-943a-61c5d396bfba"),
		//			Name = "Cocktail Editor",
		//			NormalizedName = "COCKTAIL EDITOR"
		//		}
		//	};

		//	return roles;
		//}
	}
}
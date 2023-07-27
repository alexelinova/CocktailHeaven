using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailHeaven.Infrastructure.Data.Configuration
{
	public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
	{
		public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
		{
			builder.HasData(SeedRoles());
		}

		private List<IdentityRole<Guid>> SeedRoles()
		{
			var roles = new List<IdentityRole<Guid>>()
			{
				new IdentityRole<Guid>()
				{
					Id = Guid.NewGuid(),
					Name = "Admin",
					NormalizedName = "ADMIN"
				},

				new IdentityRole<Guid>()
				{
					Id = Guid.NewGuid(),
					Name = "CocktailEditor",
					NormalizedName = "COCKTAILEDITOR"
				}
			};

			return roles;
		}
	}
}
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailHeaven.Infrastructure.Data.Configuration
{
	public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
		{
			builder.HasData(SeedUserRoles());
		}

		private List<IdentityUserRole<Guid>> SeedUserRoles()
		{
			var roles = new List<IdentityUserRole<Guid>>()
			{
				new IdentityUserRole<Guid>()
				{
					RoleId = Guid.Parse("ed23ddd6-0cab-4a38-943a-61c5d396bfba"),
					UserId = Guid.Parse("4e797b0b-c669-4bf1-913c-a90fe951241f")
				},

				new IdentityUserRole<Guid>()
				{
					RoleId = Guid.Parse("ebabec4b-1413-4d79-8d3a-b55742b6f7b6"),
					UserId = Guid.Parse("d273e367-ebf6-44b3-afa7-7759a0a579ee")
				}
			};

			return roles;
		}
	}
}

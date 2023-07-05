using CocktailHeaven.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailHeaven.Infrastructure.Data.Configuration
{
	public class UserCollectionConfiguration : IEntityTypeConfiguration<UserCollection>
	{
		public void Configure(EntityTypeBuilder<UserCollection> builder)
		{
			builder.HasOne(u => u.Cocktail)
				.WithMany(c => c.UserCollection)
				.HasForeignKey(u => u.CocktailId)
				.OnDelete(DeleteBehavior.Restrict);
			
			builder.HasOne(u => u.AddedByUser)
				.WithMany(a => a.UserCollection)
				.HasForeignKey(u => u.AddedByUserId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}

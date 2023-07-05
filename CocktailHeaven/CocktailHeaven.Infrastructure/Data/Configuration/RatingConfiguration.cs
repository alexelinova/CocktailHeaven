using CocktailHeaven.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailHeaven.Infrastructure.Data.Configuration
{
	public class RatingConfiguration : IEntityTypeConfiguration<Rating>
	{
		public void Configure(EntityTypeBuilder<Rating> builder)
		{
			builder.HasOne(r => r.AddedByUser)
				.WithMany(a => a.Ratings)
				.HasForeignKey(r => r.AddedByUserId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(e => e.Cocktail)
				.WithMany(c => c.Ratings)
				.HasForeignKey(c => c.CocktailId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.Property(p => p.IsDeleted)
				.HasDefaultValue(false);
		}
	}
}

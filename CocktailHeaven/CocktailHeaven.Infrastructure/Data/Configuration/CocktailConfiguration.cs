using CocktailHeaven.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailHeaven.Infrastructure.Data.Configuration
{
	public class CocktailConfiguration : IEntityTypeConfiguration<Cocktail>
	{
		public void Configure(EntityTypeBuilder<Cocktail> builder)
		{
			builder
				.HasOne(c => c.Image)
				.WithOne(i => i.Cocktail)
				.HasForeignKey<Image>(i => i.CocktailId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(c => c.Category)
				.WithMany(c => c.Cocktails)
				.HasForeignKey(c => c.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.Property(p => p.IsDeleted)
				.HasDefaultValue(false);
		}
	}
}

using CocktailHeaven.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailHeaven.Infrastructure.Data.Configuration
{
	public class ImageConfiguration : IEntityTypeConfiguration<Image>
	{
		public void Configure(EntityTypeBuilder<Image> builder)
		{
			builder.HasOne(i => i.Cocktail)
				.WithOne(c => c.Image)
				.HasForeignKey<Cocktail>(c => c.ImageId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.Property(p => p.IsDeleted)
				.HasDefaultValue(false);
		}
	}
}

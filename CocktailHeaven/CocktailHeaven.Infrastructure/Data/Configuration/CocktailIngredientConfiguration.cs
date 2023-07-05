using CocktailHeaven.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailHeaven.Infrastructure.Data.Configuration
{
	public class CocktailIngredientConfiguration : IEntityTypeConfiguration<CocktailIngredient>
	{
		public void Configure(EntityTypeBuilder<CocktailIngredient> builder)
		{
			builder.HasKey(x => new { x.CocktailId, x.IngredientId });
		}
	}
}

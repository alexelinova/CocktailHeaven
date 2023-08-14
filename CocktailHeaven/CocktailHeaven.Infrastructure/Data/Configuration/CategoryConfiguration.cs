using CocktailHeaven.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailHeaven.Infrastructure.Data.Configuration
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder
				.Property(p => p.IsDeleted)
				.HasDefaultValue(false);

			builder.HasData(CreateCategories());
		}

		private List<Category> CreateCategories()
		{
			var categories = new List<Category>()
			{
				new Category
				{
					Id = 1,
					Name= "Contemporary Classics",
				},

				new Category
				{
					Id = 2,
					Name = "New Era Drinks",
				},

				new Category
				{
					Id = 3,
					Name = "The Unforgettables",
				}
			};

			return categories;
		}
	}
}

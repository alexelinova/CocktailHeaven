using CocktailHeaven.Infrastructure.Data.Configuration;
using CocktailHeaven.Infrastructure.Models;
using CocktailHeaven.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CocktailHeaven.Infrastructure.Data;

public class CocktailHeavenDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
	public CocktailHeavenDbContext(DbContextOptions<CocktailHeavenDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfiguration(new ApplicationUserConfiguration());
		builder.ApplyConfiguration(new CategoryConfiguration());
		builder.ApplyConfiguration(new CocktailConfiguration());
		builder.ApplyConfiguration(new CocktailIngredientConfiguration());
		builder.ApplyConfiguration(new ImageConfiguration());
		builder.ApplyConfiguration(new RatingConfiguration());
		builder.ApplyConfiguration(new UserCollectionConfiguration());

		base.OnModelCreating(builder);
	}

	public DbSet<Category> Categories { get; set; } = null!;

	public DbSet<Cocktail> Cocktails { get; set; } = null!;

	public DbSet<CocktailIngredient> CocktailIngredients { get; set; } = null!;

	public DbSet<Ingredient> Ingredients { get; set; } = null!;

	public DbSet<Rating> Rating { get; set; } = null!;

	public DbSet<UserCollection> UserCollections { get; set; } = null!;
}
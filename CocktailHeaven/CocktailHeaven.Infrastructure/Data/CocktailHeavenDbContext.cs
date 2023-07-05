using CocktailHeaven.Infrastructure.Models;
using CocktailHeaven.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CocktailHeaven.Infrastructure.Data;

public class CocktailHeavenDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
	public CocktailHeavenDbContext(DbContextOptions<CocktailHeavenDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<CocktailIngredient>()
		   .HasKey(x => new { x.CocktailId, x.IngredientId });

		builder.Entity<Cocktail>()
			.HasOne(c => c.Image)
			.WithOne(i => i.Cocktail)
			.HasForeignKey<Image>(i => i.CocktailId);

		builder.Entity<Rating>()
		   .HasOne(e => e.Cocktail)
		   .WithMany(c => c.Ratings)
		   .HasForeignKey(c => c.CocktailId)
		   .OnDelete(DeleteBehavior.Restrict);

		builder.Entity<UserCollection>()
	   .HasOne(e => e.Cocktail)
	   .WithMany(c => c.UserCollection)
	   .HasForeignKey(c => c.CocktailId)
	   .OnDelete(DeleteBehavior.Restrict);

		base.OnModelCreating(builder);
	}

	public DbSet<Category> Categories { get; set; } = null!;

	public DbSet<Cocktail> Cocktails { get; set; } = null!;

	public DbSet<CocktailIngredient> CocktailIngredients { get; set; } = null!;

	public DbSet<Ingredient> Ingredients { get; set; } = null!;

	public DbSet<Rating> Rating { get; set; } = null!;

	public DbSet<UserCollection> UserCollections { get; set; } = null!;
}
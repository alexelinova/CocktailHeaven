using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CocktailHeaven.Core;
using CocktailHeaven.Infrastructure.Migrations;
using CocktailHeaven.Infrastructure.Models;

namespace CocktalHeaven.UnitTests
{
	public class CategoryServiceTests
	{
		private IRepository repo;
		private ICategoryService categoryService;
		private CocktailHeavenDbContext dbContext;

		[SetUp]
		public void Setup()
		{
			var contextOptions = new DbContextOptionsBuilder<CocktailHeavenDbContext>()
				.UseInMemoryDatabase("CocktailDb")
				.Options;

			this.dbContext = new CocktailHeavenDbContext(contextOptions);

			this.dbContext.Database.EnsureDeleted();
			this.dbContext.Database.EnsureCreated();

			SeedTestData();
		}

		[Test]
		public async Task GetAllCategoriesAsync_ShouldReturnValidData()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.categoryService = new CategoryService(repo);

			var categories = await this.categoryService.GetAllCategoriesAsync();
			var expectedCategoriesCount = 2;
			
			Assert.That(categories.Count(), Is.EqualTo(expectedCategoriesCount));
		}

		[TearDown]
		public void TearDown()
		{
			dbContext.Dispose();
		}

		private async void SeedTestData()
		{
			var categories = new List<Category>()
			{
				new Category()
				{
					Id = 1,
					Name = "NewCategory"
				},

				new Category()
				{
					Id = 2,
					Name = "OldCategory"
				}
			};

			await this.dbContext.AddRangeAsync(categories);
			await this.dbContext.SaveChangesAsync();
		}
	}
}

﻿using CocktailHeaven.Core;
using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Infrastructure.Data;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CocktalHeaven.UnitTests
{
	public class CategoryServiceTests
	{
		private IRepository repo;
		private ICategoryService categoryService;
		private CocktailHeavenDbContext dbContext;

		[SetUp]
		public async Task Setup()
		{
			var contextOptions = new DbContextOptionsBuilder<CocktailHeavenDbContext>()
				.UseInMemoryDatabase("CocktailDb")
				.Options;

			this.dbContext = new CocktailHeavenDbContext(contextOptions);

			this.dbContext.Database.EnsureDeleted();
			this.dbContext.Database.EnsureCreated();

			await this.SeedTestData();
		}

		[Test]
		public async Task GetAllCategoriesAsync_ShouldReturnValidData()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.categoryService = new CategoryService(repo);

			var expectedCategoriesCount = 2;
			
			Assert.That((await categoryService.GetAllCategoriesAsync()).Count(), Is.EqualTo(expectedCategoriesCount));
		}

		[TearDown]
		public void TearDown()
		{
			dbContext.Dispose();
		}

		private async Task SeedTestData()
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

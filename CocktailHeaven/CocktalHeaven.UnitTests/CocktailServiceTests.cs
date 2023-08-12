﻿using CocktailHeaven.Core;
using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.Cocktail;
using CocktailHeaven.Core.Models.Ingredient;
using CocktailHeaven.Core.Models.Search;
using CocktailHeaven.Infrastructure.Data;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CocktalHeaven.UnitTests
{
	public class CocktailServiceTests
	{
		private IRepository repo;
		private ICocktailService cocktailService;
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
		public async Task CountAsync_ShouldReturnCorrectResult()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.cocktailService = new CocktailService(repo);

			var count = await this.cocktailService.CocktailCountAsync();
			var expectedResult = 2;

			Assert.That(count, Is.EqualTo(expectedResult));
		}

		[Test]
		public async Task CreateCocktailAsync_ShouldCreateCocktail()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.cocktailService = new CocktailService(repo);

			var cocktail = new CocktailFormModel()
			{
				Name = "ThirdCocktail",
				Instruction = "Pour all ingredients into the mixing glass with ice and stir them gently.",
				Description = "Delightful combination of sparkling wine and peach puree",
				CategoryId = 1,
				Ingredients = new List<IngredientFormModel>()
				{
					new IngredientFormModel()
					{
						IngredientName = "Cola",
						Quantity = "100 ml"
					},
					new IngredientFormModel()
					{
						IngredientName = "Vodka",
						Quantity = "50 ml"
					}
				}
			};

			var userId = Guid.Parse("07d84c7f-ac4b-4561-8c48-1c6405b77f3f");

			await this.cocktailService.CreateCocktailAsync(cocktail, userId);
			var newCocktail = await this.repo.GetByIdAsync<Cocktail>(3);
			var expectedResult = 3;

			Assert.That(this.repo.AllReadonly<Cocktail>().Count(), Is.EqualTo(expectedResult));
			Assert.That(newCocktail.Name, Is.EqualTo(cocktail.Name));
			Assert.That(newCocktail.Instruction, Is.EqualTo(cocktail.Instruction));
			Assert.That(newCocktail.Description, Is.EqualTo(cocktail.Description));
			Assert.That(newCocktail.CategoryId, Is.EqualTo(cocktail.CategoryId));
			Assert.That(newCocktail.Ingredients.Count(), Is.EqualTo(cocktail.Ingredients.Count()));
		}

		[Test]
		public async Task Delete_ShouldSetIsDeletedToTrue()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.cocktailService = new CocktailService(repo);

			await this.cocktailService.Delete(1);
			var deletedCocktail = await this.repo.AllReadonly<Cocktail>(c => c.Id == 1)
				.Include(c => c.Image)
				.Include(c => c.Ratings)
				.FirstOrDefaultAsync();

			Assert.NotNull(deletedCocktail);
			Assert.True(deletedCocktail.IsDeleted);
			Assert.True(deletedCocktail.Image.IsDeleted);
			Assert.That(deletedCocktail.Ratings.All(r => r.IsDeleted));
			Assert.That(this.repo.AllReadonly<Cocktail>().Count(), Is.EqualTo(2));
		}

		[Test]
		public void Delete_ShouldThrowAnExceptionWithInvalidId()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.cocktailService = new CocktailService(repo);

			int nonExistentId = 3;

			Assert.ThrowsAsync<ArgumentException>(async () => await this.cocktailService.Delete(nonExistentId));
		}

		[Test]
		public async Task Edit_ShouldUpdateCocktailPropertiesAndIngredients()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.cocktailService = new CocktailService(repo);

			var newCocktailDetails = new CocktailEditModel()
			{
				Id = 1,
				Name = "UpdatedCocktail",
				Instruction = "Updated",
				Description = "Updated",
				CategoryId = 1,
				Ingredients = new List<IngredientFormModel>()
				{
					new IngredientFormModel()
					{
						IngredientName = "Pepsi",
						Quantity = "50 ml",
						Note = "enjoy"
					},
					new IngredientFormModel()
					{
						IngredientName = "Gin",
						Quantity = "20 ml",
					}
				}
			};

			await this.repo.SaveChangesAsync();

			await this.cocktailService.Edit(newCocktailDetails, 1);

			var updatedCocktail = await this.repo
				.AllReadonly<Cocktail>(uc => uc.Id == 1)
				.Include(c => c.Image)
				.Include(uc => uc.Ingredients)
				.ThenInclude(ci => ci.Ingredient)
				.FirstOrDefaultAsync();

			//Cocktail Properties
			Assert.NotNull(updatedCocktail);
			Assert.That(updatedCocktail.Name, Is.EqualTo(newCocktailDetails.Name));
			Assert.That(updatedCocktail.Description, Is.EqualTo(newCocktailDetails.Description));
			Assert.That(updatedCocktail.Instruction, Is.EqualTo(newCocktailDetails.Instruction));
			Assert.That(updatedCocktail.Image.ExternalURL, Is.EqualTo(newCocktailDetails.ImageURL));

			//Ingredients
			Assert.That(updatedCocktail.Ingredients.Count, Is.EqualTo(newCocktailDetails.Ingredients.Count));

			foreach (var ingredient in newCocktailDetails.Ingredients)
			{
				var updatedIngredient = updatedCocktail
					.Ingredients
					.FirstOrDefault(i => i.Ingredient.Name == ingredient.IngredientName);

				Assert.NotNull(updatedIngredient);
				Assert.That(updatedIngredient.Quantity, Is.EqualTo(ingredient.Quantity));
				Assert.That(updatedIngredient.Note, Is.EqualTo(ingredient.Note));
			}
		}

		[Test]
		public async Task ExistsByIdAsync_ShouldReturnTrueWithValidId()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.cocktailService = new CocktailService(repo);

			var result = await this.cocktailService.ExistsByIdAsync(1);

			Assert.True(result);
		}

		[Test]
		public async Task ExistsByIdAsync_ShouldReturnFalseWithInvalidId()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.cocktailService = new CocktailService(repo);

			var nonExistentId = 3;
			var result = await this.cocktailService.ExistsByIdAsync(nonExistentId);

			Assert.False(result);
		}

		[Test]
		public async Task GetCocktailCategoryAsync_ShouldReturnValidCategoryId()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.cocktailService = new CocktailService(repo);

			var categoryId = await this.cocktailService.GetCocktailCategoryAsync(1);
			var expectedResult = 1;

			Assert.That(categoryId, Is.EqualTo(expectedResult));
		}


		[Test]
		public async Task GetCocktailDetailsAsync_ShouldReturnCorrectCount()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.cocktailService = new CocktailService(repo);

			var pageNum = 1;
			var itemsPerPage = 1;

			var cocktails = await this.cocktailService.GetCocktailDetailsAsync(pageNum, itemsPerPage);
			var expectedResult = 1;

			Assert.That(cocktails.Count(), Is.EqualTo(expectedResult));
		}

		[Test]
		public async Task GetRandomCocktailAsync_ReturnsCocktail()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.cocktailService = new CocktailService(repo);

			var result = await this.cocktailService.GetRandomCocktailAsync();

			Assert.NotNull(result);
		}

		[Test]
		public async Task GetTopRatedCocktailAsync_ReturnsTopThreeHighestRated()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.cocktailService = new CocktailService(repo);

			var result = await this.cocktailService.GetTopRatedCocktailsAsync();
			var resultList = result.ToList();

			var topRatedCocktailId = 2;

			Assert.That(result.Count, Is.EqualTo(2));
			Assert.That(resultList[0].Id, Is.EqualTo(topRatedCocktailId));
		}

		[Test]
		public async Task GetCocktailByIdAsync_ShouldReturnCorrectCocktail()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.cocktailService = new CocktailService(repo);

			var cocktail = await this.cocktailService.GetCocktailByIdAsync(1);
			var expectedCocktailName = "Mojito";
			var expectedId = 1;

			Assert.That(cocktail.Id, Is.EqualTo(expectedId));
			Assert.That(cocktail.Name, Is.EqualTo(expectedCocktailName));
		}

		[Test]
		public async Task Search_ShouldReturnCorrectCocktailWithValidCriteria()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.cocktailService = new CocktailService(repo);

			var pageNum = 1;
			var itemsPerPage = 1;

			var result = await this.cocktailService.Search("Bloody Mary", SearchCriteria.CocktailName, null, pageNum, itemsPerPage);
			var resultCocktail = result.Cocktails.FirstOrDefault();

			var expectedCocktailName = "Bloody Mary";
			var expectedCocktailCount = 1;

			Assert.That(result.Cocktails.Count(), Is.EqualTo(expectedCocktailCount));
			Assert.NotNull(resultCocktail);
			Assert.That(resultCocktail.Name, Is.EqualTo(expectedCocktailName));
		}

		[Test]
		public async Task Search_ShouldReturnEmptyCollectionWithInvalidCriteria()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.cocktailService = new CocktailService(repo);

			var pageNum = 1;
			var itemsPerPage = 1;
			var searchCriteria = SearchCriteria.CocktailName;
			var nonExistentCocktail = "Pina Colada";
			var result = await this.cocktailService.Search(nonExistentCocktail, searchCriteria, null, pageNum, itemsPerPage);

			Assert.NotNull(result.Cocktails);
			Assert.That(result.Cocktails.Count(), Is.EqualTo(0));
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

			var cocktails = new List<Cocktail>()
			{
				new Cocktail()
				{
					Id = 1,
					Name = "Mojito",
					Instruction = "Shake mint, lime, and sugar with ice. Add rum and soda water.",
					Description = "A refreshing cocktail made with rum, mint, lime, and sugar.",
					Garnish = "Mint leaves",
					CategoryId = 1,
					Image = new Image()
					{
						Id = Guid.Parse("9c5a479a-5371-4244-a26a-636874c91fc1"),
						ExternalURL = "MojitoLink"
					},
					AddedByUserId = Guid.Parse("360faada-d82c-4184-9ffa-d6a4ff3d8da4"),
					CreatedOn = DateTime.Now,
				},

				new Cocktail()
				{
					Id = 2,
					Name = "Bloody Mary",
					Instruction = "Mix all ingredients and enjoy a refreshing drink",
					Description = "A refreshing cocktail made with vodka, tomato juice and tabasco.",
					Garnish = "Mint leaves",
					CategoryId = 1,
					Image = new Image()
					{
						Id = Guid.Parse("07d84c7f-ac4b-4561-8c48-1c6405b77f3f"),
						ExternalURL = "BloodyMaryLink",
					},
					AddedByUserId = Guid.Parse("c9405c11-dac5-4a22-92c4-f50642938e2e"),
					CreatedOn = DateTime.Now,
				}
			};

			await this.dbContext.AddRangeAsync(cocktails);

			var ingredients = new List<Ingredient>()
			{
				new Ingredient()
				{
					Id = 1,
					Name = "Rum"
				},

				new Ingredient()
				{
					Id = 2,
					Name = "Lime Juice"
				},

				new Ingredient()
				{
					Id = 3,
					Name = "Vodka"
				},

				new Ingredient()
				{
					Id = 4,
					Name = "Tomato juice"
				}
			};

			await this.dbContext.AddRangeAsync(ingredients);

			var cocktailIngredients = new List<CocktailIngredient>()
			{
				new CocktailIngredient()
				{
					CocktailId = 1,
					IngredientId = 1,
					Quantity = "50 ml"
				},

				new CocktailIngredient()
				{
					CocktailId = 1,
					IngredientId = 2,
					Quantity = "20 ml"
				},

				new CocktailIngredient()
				{
					CocktailId = 2,
					IngredientId = 3,
					Quantity = "50 ml"
				},

				new CocktailIngredient()
				{
					CocktailId = 2,
					IngredientId = 4,
					Quantity = "30 ml"
				}
			};

			await this.dbContext.AddRangeAsync(cocktailIngredients);

			var ratings = new List<Rating>()
			{
				new Rating()
				{
					Id = 1,
					CocktailId = 2,
					Value = 5
				},

				new Rating()
				{
					Id = 2,
					CocktailId = 2,
					Value = 4
				},

				new Rating()
				{
					Id = 3,
					CocktailId = 1,
					Value = 2
				},
			};

			await this.dbContext.AddRangeAsync(ratings);
			await this.dbContext.SaveChangesAsync();
		}
	}
}

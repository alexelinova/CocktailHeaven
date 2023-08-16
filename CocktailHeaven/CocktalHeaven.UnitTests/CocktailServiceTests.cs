using CocktailHeaven.Core;
using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.Cocktail;
using CocktailHeaven.Core.Models.Ingredient;
using CocktailHeaven.Core.Models.Search;
using CocktailHeaven.Infrastructure.Data;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace CocktalHeaven.UnitTests
{
	public class CocktailServiceTests
	{
		private IRepository repo;
		private ICocktailService cocktailService;
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
		public async Task CountAsync_ShouldReturnCorrectResult()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			var expectedResult = 2;

			Assert.That(await this.cocktailService.CocktailCountAsync(), Is.EqualTo(expectedResult));
		}

		[Test]
		public async Task CreateCocktailAsync_ShouldCreateCocktail()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

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
		public async Task DeleteAsync_ShouldSetIsDeletedToTrue()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			await this.cocktailService.DeleteAsync(1);
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
		[TestCase(100)]
		[TestCase(101)]
		public void DeleteAsync_ShouldThrowAnException_WhenIdIsNotValid(int cocktailId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			Assert.ThrowsAsync<ArgumentException>(async () => await this.cocktailService.DeleteAsync(cocktailId));
		}

		[Test]
		public async Task EditAync_ShouldUpdateCocktailPropertiesAndIngredients()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

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

			await this.cocktailService.EditAsync(newCocktailDetails, 1);

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
		[TestCase(100)]
		[TestCase(101)]
		public void EditAsync_ShouldTrhowAnException_WhenIdIsNotValid(int cocktailId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			var cocktail = new CocktailEditModel();

			Assert.ThrowsAsync<ArgumentException>(async () => await this.cocktailService.EditAsync(cocktail, cocktailId));
		}

		[Test]
		[TestCase(1)]
		[TestCase(2)]
		public async Task ExistsByIdAsync_ShouldReturnTrue_WhenIdIsValid(int cocktailId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			Assert.True(await this.cocktailService.ExistsByIdAsync(cocktailId));
		}

		[Test]
		[TestCase(100)]
		[TestCase(101)]
		public async Task ExistsByIdAsync_ShouldReturnFalse_WhenIdIsNotValid(int cocktailId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			Assert.False(await this.cocktailService.ExistsByIdAsync(cocktailId));
		}

		[Test]
		[TestCase("Bloody Mary")]
		[TestCase("Mojito")]
		public async Task ExistsByNameAsync_ShouldReturnTrue_WhenNameIsValid(string cocktailName)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			Assert.True(await this.cocktailService.ExistsByNameAsync(cocktailName));
		}

		[Test]
		[TestCase("Irish Coffee")]
		[TestCase("Margarita")]
		public async Task ExistsByNameAsync_ShouldReturnFalse_WhenNameIsNotValid(string cocktailName)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			Assert.False(await this.cocktailService.ExistsByNameAsync(cocktailName));
		}

		[Test]
		[TestCase(1, 1)]
		[TestCase(2, 1)]
		public async Task GetCocktailCategoryAsync_ShouldReturnValidCategoryId(int cocktailId, int categoryId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			Assert.That(await this.cocktailService.GetCocktailCategoryAsync(cocktailId), Is.EqualTo(categoryId));
		}


		[Test]
		[TestCase(1, 1, 1)]
		[TestCase(2, 2, 0)]
		public async Task GetCocktailDetailsAsync_ShouldReturnCorrectCount(int pageNum, int itemsPerPage, int count)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			Assert.That((await this.cocktailService.GetCocktailDetailsAsync(pageNum, itemsPerPage)).Count(), Is.EqualTo(count));
		}

		[Test]
		public async Task GetRandomCocktailAsync_ReturnsCocktail()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			Assert.NotNull(await this.cocktailService.GetRandomCocktailAsync());
		}


		[Test]
		public void GetRandomCocktailAsync_ShouldThrowAnError_WhenCollectionIsEmpty()
		{
			this.dbContext.Database.EnsureDeleted();
			this.dbContext.Database.EnsureCreated();

			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			Assert.ThrowsAsync<ArgumentException>(async () => await this.cocktailService.GetRandomCocktailAsync());
		}

		[Test]
		public async Task GetTopRatedCocktailAsync_ReturnsTopThreeHighestRated()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			var result = await this.cocktailService.GetTopRatedCocktailsAsync();
			var resultList = result.ToList();

			var topRatedCocktailId = 2;

			Assert.That(result.Count, Is.EqualTo(2));
			Assert.That(resultList[0].Id, Is.EqualTo(topRatedCocktailId));
		}

		[Test]
		[TestCase(1, "Mojito")]
		[TestCase(2, "Bloody Mary")]
		public async Task GetCocktailByIdAsync_ShouldReturnCorrectCocktail(int cocktailId, string cocktailName)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			var cocktail = await this.cocktailService.GetCocktailByIdAsync(cocktailId);

			Assert.That(cocktail.Id, Is.EqualTo(cocktailId));
			Assert.That(cocktail.Name, Is.EqualTo(cocktailName));
		}

		[Test]
		public void GetCocktailByIdAsync_ShouldThrowAnError_WhenIdIsNotValid()
		{ 
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			Assert.ThrowsAsync<ArgumentException>(async () => await this.cocktailService.GetCocktailByIdAsync(5));
		}

		[Test]
		public async Task SearchAsync_ShouldReturnCocktailByCocktailName()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			var queryString = "Bloody Mary";
			var searchCriteria = SearchCriteria.CocktailName;
			var pageNum = 1;
			var itemsPerPage = 1;

			var result = await this.cocktailService.SearchAsync(queryString, searchCriteria, null, pageNum, itemsPerPage);

			Assert.NotNull(result.Cocktails);
			Assert.That(result.Cocktails.Count(), Is.EqualTo(1));
			Assert.That(result.Cocktails.First().Name, Is.EqualTo(queryString));
		}

		[Test]
		public async Task SearchAsync_ShouldReturnCocktailsByCategory_WhenSelected()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			var category = "New Category";
			var pageNum = 1;
			var itemsPerPage = 2;

			var result = await this.cocktailService.SearchAsync(null, null, category, pageNum, itemsPerPage);

			Assert.NotNull(result.Cocktails);
			Assert.That(result.Cocktails.Count(), Is.EqualTo(2));
		}

		[Test]
		public async Task SearchAsync_ShouldReturnEmptyCollection_WhenCriteriaIsNotValid()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.cocktailService = new CocktailService(this.repo);

			var pageNum = 1;
			var itemsPerPage = 1;
			var searchCriteria = SearchCriteria.Ingredient;
			var nonExistentIngredient = "Soda";
			var result = await this.cocktailService.SearchAsync(nonExistentIngredient, searchCriteria, null, pageNum, itemsPerPage);

			Assert.NotNull(result.Cocktails);
			Assert.That(result.Cocktails.Count(), Is.EqualTo(0));
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
					Name = "New Category"
				},

				new Category()
				{
					Id = 2,
					Name = "Old Category"
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


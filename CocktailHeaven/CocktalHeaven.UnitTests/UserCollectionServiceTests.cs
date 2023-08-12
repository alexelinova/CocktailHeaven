using CocktailHeaven.Core;
using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Infrastructure.Data;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CocktalHeaven.UnitTests
{
	public class UserCollectionServiceTests
	{
		private IRepository repo;
		private IUserCollectionService userCollectionService;
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
		public async Task AddToFavouriteAsync_ShouldChangeIsFavouriteToTrue()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			var userId = Guid.Parse("d79def98-a398-4562-bdaf-656e891d95ca");
			var cocktailId = 2;
			var userCollectionId = 2;

			await this.userCollectionService.AddToFavouriteAsync(userId, cocktailId);

			var collection = await this.repo.GetByIdAsync<UserCollection>(userCollectionId);

			Assert.True(collection.IsFavourite);
		}

		[Test]
		public async Task AddToTriedAsync_ShouldChangeHasTriedToTrue()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			var userId = Guid.Parse("d79def98-a398-4562-bdaf-656e891d95ca");
			var cocktailId = 2;
			var userCollectionId = 2;

			await this.userCollectionService.AddToTriedAsync(userId, cocktailId);

			var collection = await this.repo.GetByIdAsync<UserCollection>(userCollectionId);

			Assert.True(collection.HasTried);
		}


		[Test]
		public async Task AddToWishlist_ShouldChangeWishlistToTrue()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			var userId = Guid.Parse("ee0a373c-0c2b-4ea9-a9f4-6e3dece011a5");
			var cocktailId = 2;
			var userCollectionId = 3;

			await this.userCollectionService.AddToWishListAsync(userId, cocktailId);

			var collection = await this.repo.GetByIdAsync<UserCollection>(userCollectionId);

			Assert.True(collection.WishList);
		}

		[Test]
		public async Task GetFavouriteCocktailsAsync_ShouldReturnCorrectCount()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			var userId = Guid.Parse("d79def98-a398-4562-bdaf-656e891d95ca");

			var favouriteCocktails = await this.userCollectionService.GetFavouriteCocktailsAsync(userId);
			var expectedCount = 1;

			Assert.That(favouriteCocktails.Count(), Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task GetTriedCocktailsAsync_ShouldReturnCorrectCount()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			var userId = Guid.Parse("d79def98-a398-4562-bdaf-656e891d95ca");

			var favouriteCocktails = await this.userCollectionService.GetTriedCocktailsAsync(userId);
			var expectedCount = 1;

			Assert.That(favouriteCocktails.Count(), Is.EqualTo(expectedCount));	
		}

		[Test]
		public async Task GetWishlistCocktailsAsync_ShouldReturnCorrectCount()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			var userId = Guid.Parse("d79def98-a398-4562-bdaf-656e891d95ca");

			var favouriteCocktails = await this.userCollectionService.GetWishlistCocktailsAsync(userId);
			var expectedCount = 0;

			Assert.That(favouriteCocktails.Count(), Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task IsCocktailInFavourites_ShouldReturnTrueWhenInCollection()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			var userId = Guid.Parse("d79def98-a398-4562-bdaf-656e891d95ca");
			var cocktailId = 1;
			var result = await this.userCollectionService.IsCocktailInFavouritesAsync(userId, cocktailId);

			Assert.True(result);
		
		}

		[Test]
		public async Task IsCocktailInTried_ShouldReturnTrueWhenInCollection()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			var userId = Guid.Parse("d79def98-a398-4562-bdaf-656e891d95ca");
			var cocktailId = 1;

			var result = await this.userCollectionService.IsCocktailInTriedAsync(userId, cocktailId);

			Assert.True(result);
		}


		[Test]
		public async Task IsCocktailInWishlist_ShouldReturnFalseWhenNotInCollection()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			var userId = Guid.Parse("d79def98-a398-4562-bdaf-656e891d95ca");
			var cocktailId = 1;

			var result = await this.userCollectionService.IsCocktailInWishListAsync(userId, cocktailId);

			Assert.False(result);
		}

		[Test]
		public async Task RemoveFromFavouriteAsync_ShouldSetBooleanToNull()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			var userId = Guid.Parse("d79def98-a398-4562-bdaf-656e891d95ca");
			var cocktailId = 1;

			await this.userCollectionService.RemoveFromFavouriteAsync(userId, cocktailId);

			var collection = await this.repo.GetByIdAsync<UserCollection>(1);

			Assert.Null(collection.IsFavourite); 
		}

		[Test]
		public async Task RemoveFromTriedAsync_ShouldSetBooleanToNull()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			var userId = Guid.Parse("d79def98-a398-4562-bdaf-656e891d95ca");
			var cocktailId = 1;

			await this.userCollectionService.RemoveFromTriedAsync(userId, cocktailId);

			var collection = await this.repo.GetByIdAsync<UserCollection>(1);

			Assert.Null(collection.HasTried);
		}

		[Test]
		public async Task RemoveFromWishList_ShouldSetBooleanToNull()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			var userId = Guid.Parse("ee0a373c-0c2b-4ea9-a9f4-6e3dece011a5");
			var cocktailId = 2;

			await this.userCollectionService.RemoveFromWishListAsync(userId, cocktailId);

			var collection = await this.repo.GetByIdAsync<UserCollection>(3);

			Assert.Null(collection.WishList);
		}

		[TearDown]
		public void TearDown()
		{
			dbContext.Dispose();
		}


		private async void SeedTestData()
		{
			var Cocktails = new List<Cocktail>()
			{
				new Cocktail()
				{
					Id = 1,
					Name = "Mojito",
					Description = "Description",
					Instruction = "Instruction",
					Image = new Image()
					{
						ExternalURL = "MojitoImage"
					}
				},

				new Cocktail()
				{
					Id = 2,
					Name = "Bloody Mary",
					Description = "Description",
					Instruction = "Instruction",
					Image = new Image()
					{
						ExternalURL = "BloodyMaryImage"
					}
				}
			};

			await this.dbContext.AddRangeAsync(Cocktails);

			var userCollections = new List<UserCollection>()
			{
				new UserCollection
				{
					Id = 1,
					AddedByUserId = Guid.Parse("d79def98-a398-4562-bdaf-656e891d95ca"),
					CocktailId = 1,
					HasTried = true,
					IsFavourite = true,
				},
				new UserCollection
				{
					Id = 2,
					AddedByUserId = Guid.Parse("d79def98-a398-4562-bdaf-656e891d95ca"),
					CocktailId = 2,
				},
				
				new UserCollection
				{
					Id = 3,
					AddedByUserId = Guid.Parse("ee0a373c-0c2b-4ea9-a9f4-6e3dece011a5"),
					CocktailId = 2,
					IsFavourite = true,
					HasTried = true,
					WishList = true,
				},
			};

			await this.dbContext.AddRangeAsync(userCollections);
			await this.dbContext.SaveChangesAsync();
		}
	}
}

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
		[TestCase("ee0a373c-0c2b-4ea9-a9f4-6e3dece011a5", 2, 3)]
		public async Task AddToFavouriteAsync_ShouldChangeIsFavouriteToTrue(Guid userId, int cocktailId, int userCollectionId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			await this.userCollectionService.AddToFavouriteAsync(userId, cocktailId);

			var collection = await this.repo.GetByIdAsync<UserCollection>(userCollectionId);

			Assert.True(collection.IsFavourite);
		}

		[Test]
		[TestCase("dee00d59-8449-4e75-9594-b2950a0c9d37", 10, 4)]
		public async Task AddToFavouriteAsync_CreatesUserCollection_WhenNoneExists(Guid userId, int cocktailId, int usercollectionCount)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			await this.userCollectionService.AddToFavouriteAsync(userId, cocktailId);
			Assert.That(this.repo.AllReadonly<UserCollection>().Count(), Is.EqualTo(usercollectionCount));
		}

		[Test]
		[TestCase("dee00d59-8449-4e75-9594-b2950a0c9d37", 10, 4)]
		public async Task AddToTried_CreatesUserCollection_WhenNoneExists(Guid userId, int cocktailId, int usercollectionCount)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			await this.userCollectionService.AddToTriedAsync(userId, cocktailId);
			Assert.That(this.repo.AllReadonly<UserCollection>().Count(), Is.EqualTo(usercollectionCount));
		}

		[Test]
		[TestCase("dee00d59-8449-4e75-9594-b2950a0c9d37", 10, 4)]
		public async Task AddWishlist_CreatesUserCollection_WhenNoneExists(Guid userId, int cocktailId, int usercollectionCount)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			await this.userCollectionService.AddToWishListAsync(userId, cocktailId);
			Assert.That(this.repo.AllReadonly<UserCollection>().Count(), Is.EqualTo(usercollectionCount));
		}

		[Test]
		[TestCase("d79def98-a398-4562-bdaf-656e891d95ca", 2, 2)]
		public async Task AddToTriedAsync_ShouldChangeHasTriedToTrue(Guid userId, int cocktailId, int userCollectionId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			await this.userCollectionService.AddToTriedAsync(userId, cocktailId);

			var collection = await this.repo.GetByIdAsync<UserCollection>(userCollectionId);

			Assert.True(collection.HasTried);
		}

		[Test]
		[TestCase("ee0a373c-0c2b-4ea9-a9f4-6e3dece011a5", 2, 3)]
		public async Task AddToWishlist_ShouldChangeWishlistToTrue(Guid userId, int cocktailId, int userCollectionId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			await this.userCollectionService.AddToWishListAsync(userId, cocktailId);

			var collection = await this.repo.GetByIdAsync<UserCollection>(userCollectionId);

			Assert.True(collection.WishList);
		}

		[Test]
		[TestCase("d79def98-a398-4562-bdaf-656e891d95ca", 1)]
		[TestCase("ee0a373c-0c2b-4ea9-a9f4-6e3dece011a5", 1)]
		public async Task GetFavouriteCocktailsAsync_ShouldReturnCorrectCount(Guid userId, int cocktailCount)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			var favouriteCocktails = await this.userCollectionService.GetFavouriteCocktailsAsync(userId);

			Assert.That(favouriteCocktails.Count(), Is.EqualTo(cocktailCount));
		}

		[Test]
		[TestCase("d79def98-a398-4562-bdaf-656e891d95ca", 1)]
		[TestCase("ee0a373c-0c2b-4ea9-a9f4-6e3dece011a5", 1)]
		public async Task GetTriedCocktailsAsync_ShouldReturnCorrectCount(Guid userId, int cocktailCount)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			var favouriteCocktails = await this.userCollectionService.GetTriedCocktailsAsync(userId);

			Assert.That(favouriteCocktails.Count(), Is.EqualTo(cocktailCount));	
		}

		[Test]
		[TestCase("d79def98-a398-4562-bdaf-656e891d95ca", 0)]
		[TestCase("ee0a373c-0c2b-4ea9-a9f4-6e3dece011a5", 1)]
		public async Task GetWishlistCocktailsAsync_ShouldReturnCorrectCount(Guid userId, int cocktailCount)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			var favouriteCocktails = await this.userCollectionService.GetWishlistCocktailsAsync(userId);

			Assert.That(favouriteCocktails.Count(), Is.EqualTo(cocktailCount));
		}

		[Test]
		[TestCase("d79def98-a398-4562-bdaf-656e891d95ca", 1)]
		[TestCase("ee0a373c-0c2b-4ea9-a9f4-6e3dece011a5", 2)]
		public async Task IsCocktailInFavourites_ShouldReturnTrue_WhenInCollection(Guid userId, int cocktailId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			Assert.True(await this.userCollectionService.IsCocktailInFavouritesAsync(userId, cocktailId));
		}

		[Test]
		[TestCase("d79def98-a398-4562-bdaf-656e891d95ca", 1)]
		public async Task IsCocktailInTried_ShouldReturnTrue_WhenInCollection(Guid userId, int cocktailId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			Assert.True(await this.userCollectionService.IsCocktailInTriedAsync(userId, cocktailId));
		}

		[Test]
		[TestCase("ee0a373c-0c2b-4ea9-a9f4-6e3dece011a5", 2)]
		public async Task IsCocktailInWishlist_ShouldReturnTrue_WhenInCollection(Guid userId, int cocktailId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			Assert.True(await this.userCollectionService.IsCocktailInWishListAsync(userId, cocktailId));
		}

		[Test]
		[TestCase("d79def98-a398-4562-bdaf-656e891d95ca", 10)]
		[TestCase("ee0a373c-0c2b-4ea9-a9f4-6e3dece011a5", 3)]
		public async Task IsCocktailInTried_ShouldReturnFalse_WhenNotInCollection(Guid userId, int cocktailId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			Assert.False(await this.userCollectionService.IsCocktailInTriedAsync(userId, cocktailId));
		}

		[Test]
		[TestCase("d79def98-a398-4562-bdaf-656e891d95ca", 10)]
		[TestCase("ee0a373c-0c2b-4ea9-a9f4-6e3dece011a5", 1)]
		public async Task IsCocktailInFavourite_ShouldReturnFalse_WhenNotInCollection(Guid userId, int cocktailId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			Assert.False(await this.userCollectionService.IsCocktailInFavouritesAsync(userId, cocktailId));
		}

		[Test]
		[TestCase("d79def98-a398-4562-bdaf-656e891d95ca", 10)]
		[TestCase("ee0a373c-0c2b-4ea9-a9f4-6e3dece011a5", 1)]
		public async Task IsCocktailInWishlist_ShouldReturnFalse_WhenNotInCollection(Guid userId, int cocktailId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			Assert.False(await this.userCollectionService.IsCocktailInWishListAsync(userId, cocktailId));
		}

		[Test]
		[TestCase("d79def98-a398-4562-bdaf-656e891d95ca", 1, 1)]
		[TestCase("ee0a373c-0c2b-4ea9-a9f4-6e3dece011a5", 2, 3)]
		public async Task RemoveFromFavouriteAsync_ShouldSetBooleanToNull(Guid userId, int cocktailId, int userCollectionId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			await this.userCollectionService.RemoveFromFavouriteAsync(userId, cocktailId);

			var collection = await this.repo.GetByIdAsync<UserCollection>(userCollectionId);

			Assert.Null(collection.IsFavourite); 
		}

		[Test]
		[TestCase("dee00d59-8449-4e75-9594-b2950a0c9d37", 10)]
		public void RemoveFromFavouriteAsync_ThrowsAnError_WhenCollectionNonExistent(Guid userId, int cocktailId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);
			
			Assert.ThrowsAsync<ArgumentException>(async () => await this.userCollectionService.RemoveFromFavouriteAsync(userId, cocktailId));
		}

		[Test]
		[TestCase("dee00d59-8449-4e75-9594-b2950a0c9d37", 10)]
		public void RemoveFromTriedAsync_ThrowsAnError_WhenCollectionNonExistent(Guid userId, int cocktailId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			Assert.ThrowsAsync<ArgumentException>(async () => await this.userCollectionService.RemoveFromTriedAsync(userId, cocktailId));
		}

		[Test]
		[TestCase("dee00d59-8449-4e75-9594-b2950a0c9d37", 10)]
		public void RemoveFromWishlist_ThrowsAnError_WhenCollectionNonExistent(Guid userId, int cocktailId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			Assert.ThrowsAsync<ArgumentException>(async () => await this.userCollectionService.RemoveFromWishListAsync(userId, cocktailId));
		}

		[Test]
		[TestCase("d79def98-a398-4562-bdaf-656e891d95ca", 1, 1)]
		public async Task RemoveFromTriedAsync_ShouldSetBooleanToNull(Guid userId, int cocktailId, int userCollectionId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			await this.userCollectionService.RemoveFromTriedAsync(userId, cocktailId);

			var collection = await this.repo.GetByIdAsync<UserCollection>(userCollectionId);

			Assert.Null(collection.HasTried);
		}

		[Test]
		[TestCase("ee0a373c-0c2b-4ea9-a9f4-6e3dece011a5", 2, 3)]
		public async Task RemoveFromWishList_ShouldSetBooleanToNull(Guid userId, int cocktailId, int userCollectionId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.userCollectionService = new UserCollectionService(this.repo);

			await this.userCollectionService.RemoveFromWishListAsync(userId, cocktailId);

			var collection = await this.repo.GetByIdAsync<UserCollection>(userCollectionId);

			Assert.Null(collection.WishList);
		}

		[TearDown]
		public void TearDown()
		{
			dbContext.Dispose();
		}

		private async Task SeedTestData()
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

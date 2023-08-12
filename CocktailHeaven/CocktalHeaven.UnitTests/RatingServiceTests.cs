using CocktailHeaven.Core;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Infrastructure.Models;
using CocktailHeaven.Infrastructure.Models.Identity;

namespace CocktalHeaven.UnitTests
{
	public class RatingServiceTests
	{
		private IRepository repo;
		private IRatingService ratingService;
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
		public async Task DeleteRating_ShouldSetRatingIsDeletedToTrue()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.ratingService = new RatingService(this.repo);

			await this.ratingService.DeleteRating(3);
			var deletedRating = await this.repo.GetByIdAsync<Rating>(3);

			Assert.True(deletedRating.IsDeleted);
		}

		[Test]
		public async Task GetAllRatingsAsync_ShouldReturnCorrectCount()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.ratingService = new RatingService(this.repo);

			var rating = await this.repo.All<Rating>(r => r.Id == 1).FirstOrDefaultAsync();
			rating!.IsDeleted = true;
			await this.repo.SaveChangesAsync();

			var ratings = await this.ratingService.GetAllRatingsAsync();
			var expectedCount = 2;

			Assert.That(ratings.Count(), Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task RateAsync_ShouldChangeExistingRatingIfAny()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.ratingService = new RatingService(this.repo);

			var userId = Guid.Parse("6ca0143e-aa95-4956-a0c5-def1ec3394e2");
			var cocktaiId = 2;

			await this.ratingService.RateAsync(cocktaiId, userId, 4, "no comment");

			var rating = await this.repo.GetByIdAsync<Rating>(1);
			var expectedValue = 4;
			var expectedComment = "no comment";
			var expectedRatingCount = 3;

			Assert.That(rating.Value, Is.EqualTo(expectedValue));
			Assert.That(rating.Comment, Is.EqualTo(expectedComment));
			Assert.That(this.repo.AllReadonly<Rating>().Count, Is.EqualTo(expectedRatingCount));
		}

		[Test]
		public async Task RateAsync_ShouldCreateRatingIfNoneExists()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.ratingService = new RatingService(this.repo);

			var userId = Guid.Parse("6ca0143e-aa95-4956-a0c5-def1ec3394e2");
			var cocktaiId = 10;

			await this.ratingService.RateAsync(cocktaiId, userId, 1, "new Rating");

			var rating = await this.repo.GetByIdAsync<Rating>(4);
			var expectedValue = 1;
			var expectedComment = "new Rating";
			var expectedRatingCount = 4;

			Assert.That(rating.Value, Is.EqualTo(expectedValue));
			Assert.That(rating.Comment, Is.EqualTo(expectedComment));
			Assert.That(this.repo.AllReadonly<Rating>().Count, Is.EqualTo(expectedRatingCount));
		}

		[Test]
		public async Task RatingExistsAsync_ShouldReturnTrueWithValidId()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.ratingService = new RatingService(this.repo);

			var existingId = 1;
			var result = await this.ratingService.RatingExistsAsync(existingId);

			Assert.True(result);
		}

		[Test]
		public async Task RatingExistsAsync_ShouldReturnFalseWithInvalidId()
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.ratingService = new RatingService(this.repo);

			var nonExistentId = 5;
			var result = await this.ratingService.RatingExistsAsync(nonExistentId);

			Assert.False(result);
		}

		[TearDown]
		public void TearDown()
		{
			dbContext.Dispose();
		}


		private async void SeedTestData()
		{
			var ratings = new List<Rating>()
			{
				new Rating()
				{
					Id = 1,
					CocktailId = 2,
					Value = 5,
					AddedByUserId = Guid.Parse("6ca0143e-aa95-4956-a0c5-def1ec3394e2")
					
				},

				new Rating()
				{
					Id = 2,
					CocktailId = 3,
					Value = 4,
					AddedByUserId = Guid.Parse("6ca0143e-aa95-4956-a0c5-def1ec3394e2")
				},

				new Rating()
				{
					Id = 3,
					CocktailId = 1,
					Value = 2,
					AddedByUserId = Guid.Parse("6ca0143e-aa95-4956-a0c5-def1ec3394e2")
				},
			};

			await this.dbContext.AddRangeAsync(ratings);

			var applicationUser = new ApplicationUser()
			{
				Id = Guid.Parse("6ca0143e-aa95-4956-a0c5-def1ec3394e2"),
				Email = "Test@mail.com",
			};

			await this.dbContext.AddAsync(applicationUser);
			await this.dbContext.SaveChangesAsync();
		}
	}
}

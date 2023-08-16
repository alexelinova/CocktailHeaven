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

			var expectedCount = 2;

			Assert.That((await this.ratingService.GetAllRatingsAsync()).Count(), Is.EqualTo(expectedCount));
		}

		[Test]
		[TestCase(2, "6ca0143e-aa95-4956-a0c5-def1ec3394e2", 4, "no comment", 1, 3 )]
		[TestCase(1, "6ca0143e-aa95-4956-a0c5-def1ec3394e2", 5, "no comment", 3, 3)]
		public async Task RateAsync_ShouldChangeExistingRating_WhenAnyExists(int cocktailId, Guid userId, int value, string? comment, int ratingId, int ratingCount)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.ratingService = new RatingService(this.repo);

			await this.ratingService.RateAsync(cocktailId, userId, value, comment);
			var rating = await this.repo.GetByIdAsync<Rating>(ratingId);

			Assert.That(rating.Value, Is.EqualTo(value));
			Assert.That(rating.Comment, Is.EqualTo(comment));
			Assert.That(this.repo.AllReadonly<Rating>().Count, Is.EqualTo(ratingCount));
		}

		[Test]
		[TestCase(10, "6ca0143e-aa95-4956-a0c5-def1ec3394e2", 2, "Amazing", 4, 4)]
		[TestCase(9, "6ca0143e-aa95-4956-a0c5-def1ec3394e2", 5, "no comment", 4, 4)]
		public async Task RateAsync_ShouldCreateRating_WhenNoneExists(int cocktailId, Guid userId, int value, string? comment, int ratingId, int ratingCount)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.ratingService = new RatingService(this.repo);

			await this.ratingService.RateAsync(cocktailId, userId, value, comment);
			var rating = await this.repo.GetByIdAsync<Rating>(ratingId);

			Assert.That(rating.Value, Is.EqualTo(value));
			Assert.That(rating.Comment, Is.EqualTo(comment));
			Assert.That(this.repo.AllReadonly<Rating>().Count, Is.EqualTo(ratingCount));
		}

		[Test]
		[TestCase(1)]
		[TestCase(3)]
		public async Task RatingExistsAsync_ShouldReturnTrue_WhenIdIsValid(int ratingId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.ratingService = new RatingService(this.repo);

			Assert.True(await this.ratingService.RatingExistsAsync(ratingId));
		}

		[Test]
		[TestCase(100)]
		[TestCase(-5)]
		public async Task RatingExistsAsync_ShouldReturnFalse_WhenIdIsNotValid(int ratingId)
		{
			this.repo = new CocktailHeavenRepository(this.dbContext);
			this.ratingService = new RatingService(this.repo);

			Assert.False(await this.ratingService.RatingExistsAsync(ratingId));
		}

		[TearDown]
		public void TearDown()
		{
			dbContext.Dispose();
		}


		private async Task SeedTestData()
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

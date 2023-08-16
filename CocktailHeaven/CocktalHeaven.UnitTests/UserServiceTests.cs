using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CocktailHeaven.Core;
using CocktailHeaven.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Moq;
using CocktailHeaven.Infrastructure.Models;

namespace CocktalHeaven.UnitTests
{
	public class UserServiceTests
	{
		private IRepository repo;
		private IUserService userService;
		private Mock<UserManager<ApplicationUser>> mockUserManager;
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

			this.mockUserManager = new Mock<UserManager<ApplicationUser>>(
			   Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
		}

		[Test]
		public async Task DeleteUserAsync_ShouldSetIsDeletedToTrueAndEraseCustomerData()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.userService = new UserService(this.repo, mockUserManager.Object);

			var userId = Guid.Parse("f79430b5-8b18-41a4-a2de-ce6de80327e8");

			await this.userService.DeleteUserAsync(userId);

			var user = await this.repo.GetByIdAsync<ApplicationUser>(userId);

			Assert.True(user.IsDeleted);
			Assert.Null(user.NormalizedEmail);
			Assert.Null(user.Email);
			Assert.Null(user.PasswordHash);
			Assert.Null(user.SecurityStamp);
			Assert.Null(user.ConcurrencyStamp);
		}

		[Test]
		[TestCase("7e7e8760-9c39-406b-9900-5c4f5998815b")]
		[TestCase("6a727895-2b59-495f-8961-d29fa4c2e28c")]
		public void DeleteUserAsync_ShouldThrowAnError_WhenIdIsNotValid(Guid userId)
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.userService = new UserService(this.repo, mockUserManager.Object);

			Assert.ThrowsAsync<ArgumentException>(async () => await this.userService.DeleteUserAsync(userId));
		}

		[Test]
		public async Task GetAllUsers_ShouldReturnUsersCorrectCountAndOrder()
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.userService = new UserService(this.repo, mockUserManager.Object);

			var users = await this.userService.GetAllUsersAsync();
			var firstUser = users.First();
			var expectedUsersCount = 2;
			var expectedFirstUsername = "Aleks";

			Assert.That(users.Count(), Is.EqualTo(expectedUsersCount));
			Assert.That(firstUser.Username, Is.EqualTo(expectedFirstUsername));
		}

		[Test]
		[TestCase("f79430b5-8b18-41a4-a2de-ce6de80327e8")]
		[TestCase("af453050-445f-4568-8191-2959dbdfd0ab")]
		public async Task UserExistsAsync_ReturnsTrue_WhenDataIsValid(Guid userId)
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.userService = new UserService(this.repo, mockUserManager.Object);

			Assert.True(await this.userService.UserExistsAsync(userId));
		}

		[Test]
		[TestCase("4a890480-eb7c-4b19-aa0f-c444f2286df9")]
		[TestCase("9e495133-4a0a-44e8-8482-7249cb2e67bb")]
		public async Task UserExistsAsync_ReturnsFalse_WithIdIsInvalidOrWhenIsDeletedIsTrue(Guid userId)
		{
			this.repo = new CocktailHeavenRepository(dbContext);
			this.userService = new UserService(this.repo, mockUserManager.Object);

			Assert.False(await this.userService.UserExistsAsync(userId));
		}

		[TearDown]
		public void TearDown()
		{
			dbContext.Dispose();
		}

		private async Task SeedTestData()
		{
			var users = new List<ApplicationUser>()
		   {
			   new ApplicationUser()
			   {
				   Id = Guid.Parse("f79430b5-8b18-41a4-a2de-ce6de80327e8"),
				   UserName = "Test",
				   Email = "Test@mail.com",
			   },

			   new ApplicationUser()
			   {
				   Id = Guid.Parse("af453050-445f-4568-8191-2959dbdfd0ab"),
				   UserName = "Aleks",
				   Email = "Aleks@mail.com",
			   }, 

			   new ApplicationUser()
			   {
				   Id = Guid.Parse("4a890480-eb7c-4b19-aa0f-c444f2286df9"),
				   UserName = "Simon",
				   Email = "Simon@mail.com",
				   IsDeleted = true,
			   }
		   };

			await this.dbContext.AddRangeAsync(users);
			await this.dbContext.SaveChangesAsync();
		}
	}
}

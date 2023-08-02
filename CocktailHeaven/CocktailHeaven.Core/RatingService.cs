using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Models;
using CocktailHeaven.Infrastructure.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace CocktailHeaven.Core
{
	public class RatingService : IRatingService
	{
		private readonly IRepository repo;

		public RatingService(IRepository repo)
		{
			this.repo = repo;
		}

		public async Task RateAsync(int cocktailId, Guid userId, int ratingValue, string? comment)
		{
			var user = this.repo
				.All<ApplicationUser>(a => a.Id == userId)
				.Include(a => a.Ratings)
				.FirstOrDefault();

			var rating = user.Ratings
				.Where(r => r.CocktailId == cocktailId)
				.FirstOrDefault();

			if (rating != null)
			{
				rating.Value = ratingValue;
				rating.ModifiedOn = DateTime.UtcNow;
				rating.Comment = comment;
			}
			else
			{
				user.Ratings.Add(new Rating()
				{
					CocktailId = cocktailId,
					Value = ratingValue,
					Comment = comment,
					CreatedOn = DateTime.UtcNow
				});
			}
				await repo.SaveChangesAsync();
		}
	}
}

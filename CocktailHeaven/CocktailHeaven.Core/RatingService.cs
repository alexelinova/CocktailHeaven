using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Exceptions;
using CocktailHeaven.Core.Models.Rating;
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

        public async Task DeleteRating(int ratingId)
        {
			var rating = await this.repo.GetByIdAsync<Rating>(ratingId);

			if (rating != null)
			{
                rating.IsDeleted = true;
                await repo.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RatingAllViewModel>> GetAllRatingsAsync()
        {
            return await this.repo.AllReadonly<Rating>()
				.Where(r => r.IsDeleted == false)
				.Select(r => new RatingAllViewModel()
				{
					Id = r.Id,
					Comment = r.Comment,
					CreatedOn = r.CreatedOn,
					Value = r.Value,
					UserEmail = r.AddedByUser.Email,
				})
				.OrderByDescending(r => r.CreatedOn)
				.ToListAsync();
        }

        public async Task RateAsync(int cocktailId, Guid userId, int ratingValue, string? comment)
		{
			var user = await this.repo
				.All<ApplicationUser>(a => a.Id == userId)
				.Include(a => a.Ratings)
				.FirstOrDefaultAsync();

			var rating = user.Ratings
				.Where(r => r.CocktailId == cocktailId && r.IsDeleted == false)
				.FirstOrDefault();

			if (rating != null)
			{
				rating.Value = ratingValue;
				rating.CreatedOn = DateTime.UtcNow;
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

        public async Task<bool> RatingExistsAsync(int ratingId)
        {
			var rating = await this.repo
				.AllReadonly<Rating>(r => r.IsDeleted == false)
				.FirstOrDefaultAsync();

			return rating != null;
        }
    }
}

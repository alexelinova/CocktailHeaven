using CocktailHeaven.Core.Models.Rating;

namespace CocktailHeaven.Core.Contracts
{
	public interface IRatingService
	{
		Task RateAsync(int cocktailId, Guid userId, int ratingValue, string? comment);

		Task<IEnumerable<RatingAllViewModel>> GetAllRatingsAsync();

		Task DeleteRating(int ratingId);

		Task<bool> RatingExistsAsync(int ratingId);
	}
}

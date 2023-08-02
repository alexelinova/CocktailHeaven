namespace CocktailHeaven.Core.Contracts
{
	public interface IRatingService
	{
		Task RateAsync(int cocktailId, Guid userId, int ratingValue, string? comment);
	}
}

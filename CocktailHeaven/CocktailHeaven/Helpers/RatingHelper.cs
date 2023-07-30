namespace CocktailHeaven.Helpers
{
	public static class RatingHelper
	{
		public static string GetStarClass(int starNumber, double averageRating)
		{
			double roundedRating = Math.Floor(averageRating);
			double decimalPart = averageRating - roundedRating;

			if (starNumber <= roundedRating)
			{
				return "active";
			}
			else if (starNumber == roundedRating + 1 && decimalPart >= 0.5)
			{
				return "half";
			}
			else
			{
				return "";
			}
		}
	}
}

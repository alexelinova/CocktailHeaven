namespace CocktailHeaven.Core.Models.NewFolder
{
	public class RatingFormModel
	{
		public float Value { get; set; }

		public string Username { get; set; } = null!;

		public string? Comment { get; set; }

		public DateTime CreatedOn { get; set; }
	}
}

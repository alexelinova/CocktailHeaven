namespace CocktailHeaven.Core.Models.Cocktail
{
	public class CocktailDetailsModel
	{
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? Url { get; set; } = null!;
    }
}

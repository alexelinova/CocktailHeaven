namespace CocktailHeaven.Core.Models.Cocktail
{
    public class CocktailHomeModel
    {
        public int CocktailsCount { get; set; }

        public IEnumerable<CocktailCollectionModel> Cocktails { get; set; } = null!;
    }
}

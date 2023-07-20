namespace CocktailHeaven.Core.Models.Cocktail
{
	public class CocktailAllViewModel : PagingModel
	{
		public IEnumerable<CocktailDetailsModel> Cocktails { get; set; } = null!;
    }
}

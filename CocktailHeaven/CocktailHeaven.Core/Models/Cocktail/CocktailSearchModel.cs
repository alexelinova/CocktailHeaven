using CocktailHeaven.Core.Models.Ingredient;

namespace CocktailHeaven.Core.Models.Cocktail
{
	public class CocktailSearchModel : CocktailCollectionModel
	{
		public string Instructions { get; set; } = null!;
        public IEnumerable<IngredientFormModel>? Ingredients { get; set; }
    }
}

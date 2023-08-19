namespace CocktailHeaven.Core.Models.Ingredient
{
    public class IngredientCocktailModel
    {
        public string CocktailName { get; set; } = null!;

        public IEnumerable<IngredientFormModel> Ingredients { get; set; } = null!;
    }
}

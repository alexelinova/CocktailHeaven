using CocktailHeaven.Core.Models.Ingredient;

namespace CocktailHeaven.Core.Models.Cocktail
{
    public class CocktailFullModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Instructions { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string Image { get; set; } = null!;

        public IEnumerable<IngredientFormModel> Ingredients { get; set; } = new List<IngredientFormModel>();
    }
}

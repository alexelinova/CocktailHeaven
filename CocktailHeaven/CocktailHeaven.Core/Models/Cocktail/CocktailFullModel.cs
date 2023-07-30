using CocktailHeaven.Core.Models.Ingredient;
using CocktailHeaven.Core.Models.NewFolder;

namespace CocktailHeaven.Core.Models.Cocktail
{
    public class CocktailFullModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Instructions { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string? Garnish { get; set; }

        public string Image { get; set; } = null!;

        public IEnumerable<IngredientFormModel> Ingredients { get; set; } = new List<IngredientFormModel>();

        public IEnumerable<RatingFormModel>? Ratings { get; set; }
    }
}

﻿namespace CocktailHeaven.Core.Models.Cocktail
{
    public class RandomCocktailModel
    {
        public string Name { get; set; } = null!;

        public string Instructions { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string? Image { get; set; }

        public IEnumerable<IngredientFormModel> Ingredients { get; set; } = new List<IngredientFormModel>();
    }
}

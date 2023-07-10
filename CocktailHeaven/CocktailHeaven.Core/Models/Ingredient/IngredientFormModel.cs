using System.ComponentModel.DataAnnotations;
using static CocktailHeaven.Infrastructure.Models.DataConstants.IngredientConstants;
using static CocktailHeaven.Infrastructure.Models.DataConstants.CocktailIngredientConstants;

namespace CocktailHeaven.Core.Models.Ingredient
{
    public class IngredientFormModel
    {
        [Required]
        [StringLength(MaxIngredientLength, MinimumLength = MinIngredientLength)]
        [Display(Name = "Ingredient")]
        public string IngredientName { get; set; } = null!;

        [Required]
        [StringLength(MaxQuantityLength, MinimumLength = MinQuantityLength)]
        public string Quantity { get; set; } = null!;

        [StringLength(MaxNoteLength)]
        public string? Note { get; set; }
    }
}

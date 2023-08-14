using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CocktailHeaven.Infrastructure.Models.DataConstants.CocktailIngredientConstants;

namespace CocktailHeaven.Infrastructure.Models
{
	public class CocktailIngredient
    {
        [ForeignKey(nameof(Cocktail))]
        public int CocktailId { get; set; }

        public virtual Cocktail Cocktail { get; set; } = null!;

        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;

        [Required]
        [MaxLength(MaxQuantityLength)]
        public string Quantity { get; set; } = null!;

        [MaxLength(MaxNoteLength)]
        public string? Note { get; set; }

        public bool isDeleted { get; set; } = false;
    }
}
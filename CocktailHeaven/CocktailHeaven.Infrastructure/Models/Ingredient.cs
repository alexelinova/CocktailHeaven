using System.ComponentModel.DataAnnotations;
using static CocktailHeaven.Infrastructure.Models.DataConstants.IngredientConstants;

namespace CocktailHeaven.Infrastructure.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
            this.Cocktails = new HashSet<CocktailIngredient>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxIngredientLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<CocktailIngredient> Cocktails { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
using System.ComponentModel.DataAnnotations;
using static CocktailHeaven.Infrastructure.Models.DataConstants.CategoryConstants;

namespace CocktailHeaven.Infrastructure.Models
{
    public class Category
    {
        public Category()
        {
            this.Cocktails = new HashSet<Cocktail>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxCategoryLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Cocktail> Cocktails { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
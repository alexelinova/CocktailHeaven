using CocktailHeaven.Infrastructure.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CocktailHeaven.Infrastructure.Models.DataConstants.CocktailConstants;

namespace CocktailHeaven.Infrastructure.Models
{
	public class Cocktail
    {
        public Cocktail()
        {
            this.Ingredients = new HashSet<CocktailIngredient>();
            this.Ratings = new HashSet<Rating>();
            this.UserCollection = new HashSet<UserCollection>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(MaxInstructionLength)]
        public string Instruction { get; set; } = null!;

        [Required]
        [MaxLength(MaxInstructionLength)]
        public string Description { get; set; } = null!;

        [MaxLength(MaxGarnishLength)]
        public string? Garnish { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Required]
        public virtual Category Category { get; set; } = null!;

        public Guid? ImageId { get; set; }

        public Image? Image { get; set; } 

		public Guid AddedByUserId { get; set; }

		public virtual ApplicationUser AddedByUser { get; set; } = null!;

		public virtual ICollection<CocktailIngredient> Ingredients { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<UserCollection> UserCollection { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}

using CocktailHeaven.Infrastructure.Models.Identity;
using System.ComponentModel.DataAnnotations;
using static CocktailHeaven.Infrastructure.Models.DataConstants.RatingConstants;

namespace CocktailHeaven.Infrastructure.Models
{
	public class Rating
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(MaxRatingValue)]
        public float Value { get; set; }

        public Guid AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; } = null!;

        public int CocktailId { get; set; }

        public virtual Cocktail Cocktail { get; set; } = null!;

        public string? Comment { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
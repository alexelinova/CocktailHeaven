using System.ComponentModel.DataAnnotations;
using static CocktailHeaven.Infrastructure.Models.DataConstants.RatingConstants;

namespace CocktailHeaven.Core.Models.Rating
{
	public class RateCocktailModel
	{
        public int CocktailId { get; set; }

        [Range(MinRatingValue, MaxRatingValue)]
        public int Value { get; set; }

        [StringLength(MaxCommentLength)]
        public string? Comment { get; set; }
    }
}

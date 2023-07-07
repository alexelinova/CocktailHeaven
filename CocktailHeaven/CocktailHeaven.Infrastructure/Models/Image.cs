using System.ComponentModel.DataAnnotations;
using static CocktailHeaven.Infrastructure.Models.DataConstants.ImageConstants;

namespace CocktailHeaven.Infrastructure.Models
{
    public class Image
	{
        [Key]
        public Guid Id { get; set; }

        [MaxLength(MaxExtensionLength)]
        public string? Extension { get; set; }

        public string? ExternalURL { get; set; }

        public int CocktailId { get; set; }

		public Cocktail Cocktail { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;
    }
}
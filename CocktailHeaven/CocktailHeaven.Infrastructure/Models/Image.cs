using CocktailHeaven.Infrastructure.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CocktailHeaven.Infrastructure.Models.DataConstants.ImageConstants;

namespace CocktailHeaven.Infrastructure.Models
{
	public class Image
	{
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(MaxExtensionLength)]
        public string Extension { get; set; } = null!;

        public int CocktailId { get; set; }

		public Cocktail Cocktail { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;
    }
}
using CocktailHeaven.Infrastructure.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace CocktailHeaven.Infrastructure.Models
{
	public class UserCollection
    {
        [Key]
        public int Id { get; set; }

        public Guid AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; } = null!;

        public int CocktailId { get; set; }

        public virtual Cocktail Cocktail { get; set; } = null!;

        public bool? HasTried { get; set; }

        public bool? WishList { get; set; }

        public bool? IsFavourite { get; set; }
    }
}
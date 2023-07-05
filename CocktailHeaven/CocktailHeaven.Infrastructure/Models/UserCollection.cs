using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CocktailHeaven.Infrastructure.Models.Identity;

namespace CocktailHeaven.Infrastructure.Models
{
    public class UserCollection
    {
        [Key]
        public int Id { get; set; }

        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; } = null!;

        public int CocktailId { get; set; }

        public virtual Cocktail Cocktail { get; set; } = null!;

        public bool? HasTried { get; set; }

        public bool? WishList { get; set; }

        public bool? IsFavourite { get; set; }
    }
}
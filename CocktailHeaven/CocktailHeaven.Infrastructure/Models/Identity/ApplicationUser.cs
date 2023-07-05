using Microsoft.AspNetCore.Identity;

namespace CocktailHeaven.Infrastructure.Models.Identity
{
	public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Ratings = new HashSet<Rating>();
            this.UserCollection = new HashSet<UserCollection>();
        }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<UserCollection> UserCollection { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}

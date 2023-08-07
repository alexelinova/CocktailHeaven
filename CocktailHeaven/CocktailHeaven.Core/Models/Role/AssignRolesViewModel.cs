using CocktailHeaven.Core.Models.ApplicationUser;

namespace CocktailHeaven.Core.Models.Role
{
    public class AssignRolesViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; } = null!;

        public IEnumerable<string> Roles { get; set; } = null!;
    }
}

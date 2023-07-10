using System.ComponentModel.DataAnnotations;

namespace CocktailHeaven.Core.Models.ApplicationUser
{
    public class UserLoginModel
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}

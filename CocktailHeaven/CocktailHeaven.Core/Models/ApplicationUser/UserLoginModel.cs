using System.ComponentModel.DataAnnotations;

namespace CocktailHeaven.Core.Models.ApplicationUser
{
    public class UserLoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}

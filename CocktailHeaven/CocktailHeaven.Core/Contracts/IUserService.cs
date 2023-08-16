using CocktailHeaven.Core.Models.ApplicationUser;

namespace CocktailHeaven.Core.Contracts
{
    public interface IUserService
    {
        Task DeleteUserAsync(Guid userId);

        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();

        Task<bool> UserExistsAsync(Guid userId);
    }
}

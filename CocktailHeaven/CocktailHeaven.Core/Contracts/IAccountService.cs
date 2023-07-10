using Microsoft.AspNetCore.Identity;

namespace CocktailHeaven.Core.Contracts
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterUserAsync(string username, string email, string password);

        Task<SignInResult> SignInUserAsync(string email, string password);

        Task SignOutUserAsync();
    }
}

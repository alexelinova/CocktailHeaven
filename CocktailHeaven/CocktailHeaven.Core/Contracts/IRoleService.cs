using Microsoft.AspNetCore.Identity;

namespace CocktailHeaven.Core.Contracts
{
    public interface IRoleService
    {
        Task<IdentityResult> AssignRoleAsync(Guid userId, string roleName);

        Task<IdentityResult> RemoveUserFromRoleAsync(Guid userId, string roleName);

        Task<IEnumerable<string>> GetRolesAsync();

        Task<bool> RoleExists(string roleName);
    }
}

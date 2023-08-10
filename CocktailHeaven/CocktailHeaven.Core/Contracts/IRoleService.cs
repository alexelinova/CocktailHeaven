namespace CocktailHeaven.Core.Contracts
{
    public interface IRoleService
    {
        Task AssignRoleAsync(Guid userId, string roleName);

        Task RemoveUserFromRoleAsync(Guid userId, string roleName);

        Task<IEnumerable<string>> GetRolesAsync();

        Task<bool> RoleExists(string roleName);
    }
}

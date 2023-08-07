using CocktailHeaven.Core.Models.Role;
using Microsoft.AspNetCore.Mvc;
using CocktailHeaven.Core.Contracts;


namespace CocktailHeaven.Areas.Admin.Controllers
{
    public class RoleController : BaseAdminController
    {
        private readonly IRoleService roleService;

        private readonly IUserService userService;

        public RoleController(IRoleService roleService, IUserService userService)
        {
            this.roleService = roleService;
            this.userService = userService;
        }

        public async Task<IActionResult> All()
        {
            var users = await userService.GetAllUsersAsync();
            var roles = await roleService.GetRolesAsync();

            var viewModel = new AssignRolesViewModel()
            {
               Users = users,
               Roles = roles
            };

            return View(viewModel);

        }

        [HttpPost]
		public async Task<IActionResult> AssignRole(Guid id, string roleName)
		{
            await roleService.AssignRoleAsync(id, roleName);

            return RedirectToAction("All");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteRole(Guid id, string roleName)
		{
            await roleService.RemoveUserFromRoleAsync(id, roleName);
            
			return RedirectToAction("All");
		}
	}
}

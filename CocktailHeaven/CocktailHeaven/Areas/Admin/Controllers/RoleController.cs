using CocktailHeaven.Core.Models.Role;
using Microsoft.AspNetCore.Mvc;
using CocktailHeaven.Core.Contracts;
using static CocktailHeaven.Infrastructure.Models.DataConstants.MessageConstant;


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
            var users = await this.userService.GetAllUsersAsync();
            var roles = await this.roleService.GetRolesAsync();

            var viewModel = new AssignRolesViewModel()
            {
               Users = users,
               Roles = roles
            };

            return this.View(viewModel);

        }

        [HttpPost]
		public async Task<IActionResult> AssignRole(Guid id, string roleName)
		{
            if (await this.userService.UserExistsAsync(id) == false)
            {
                TempData[ErrorMessage] = ErrorMessageUser;
                return this.RedirectToAction(nameof(All));
            }

            if (await this.roleService.RoleExists(roleName) == false)
            {
                TempData[ErrorMessage] = ErrorMessageRole;
				return this.RedirectToAction(nameof(All));
			}

            if(await this.userService.UserIsInRoleAsync(id, roleName))
            {
				TempData[ErrorMessage] = ErrorMessageUserInRole;
				return this.RedirectToAction(nameof(All));
			}

            await this.roleService.AssignRoleAsync(id, roleName);

            return this.RedirectToAction(nameof(All));
		}

		[HttpPost]
		public async Task<IActionResult> DeleteRole(Guid id, string roleName)
		{
			if (await this.userService.UserExistsAsync(id) == false)
			{
				TempData[ErrorMessage] = ErrorMessageUser;
				return this.RedirectToAction(nameof(All));
			}

			if (await this.roleService.RoleExists(roleName) == false)
			{
				TempData[ErrorMessage] = ErrorMessageRole;
				return this.RedirectToAction(nameof(All));
			}

			if (!await this.userService.UserIsInRoleAsync(id, roleName))
			{
				TempData[ErrorMessage] = ErrorMessageUserNotInRole;
				return this.RedirectToAction(nameof(All));
			}

			await this.roleService.RemoveUserFromRoleAsync(id, roleName);
            
			return this.RedirectToAction(nameof(All));
		}
	}
}

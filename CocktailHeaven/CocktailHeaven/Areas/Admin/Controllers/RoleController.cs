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
			if (!await this.roleService.RoleExists(roleName))
			{
				this.TempData[ErrorMessage] = ErrorMessageRole;
				return this.RedirectToAction(nameof(All));
			}

			try
			{
				var result = await this.roleService.AssignRoleAsync(id, roleName);

				if (!result.Succeeded)
				{
					this.TempData[ErrorMessage] = string.Join(", ", result.Errors.Select(e => e.Description));
				}
				else
				{
                    this.TempData[SuccessMessage] = string.Format(SuccessMessageUserToRole, roleName);
                }
			}
			catch (ArgumentException ex)
			{

				this.TempData[ErrorMessage] = ex.Message;
			}
			
			return RedirectToAction(nameof(All));
		}

		[HttpPost]
		public async Task<IActionResult> DeleteRole(Guid id, string roleName)
		{
			if (!await this.roleService.RoleExists(roleName))
			{
				this.TempData[ErrorMessage] = ErrorMessageRole;
				return this.RedirectToAction(nameof(All));
			}

			try
			{
				var result = await this.roleService.RemoveUserFromRoleAsync(id, roleName);

				if (!result.Succeeded)
				{
					this.TempData[ErrorMessage] = string.Join(", ", result.Errors.Select(e => e.Description));
				}
                else
                {
                    this.TempData[SuccessMessage] = string.Format(SuccessMessageUserRemovedFromRole, roleName);
                }
            }
			catch (ArgumentException ex)
			{

				this.TempData[ErrorMessage] = ex.Message;
			}
			
			return this.RedirectToAction(nameof(All));
		}
	}
}

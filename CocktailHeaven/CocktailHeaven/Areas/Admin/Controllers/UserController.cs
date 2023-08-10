using CocktailHeaven.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using static CocktailHeaven.Infrastructure.Models.DataConstants.MessageConstant;

namespace CocktailHeaven.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService userSerice;

        public UserController(IUserService userSerice)
        {
            this.userSerice = userSerice;
        }

        public async Task<IActionResult> All()
        {
            var allUsers = await this.userSerice.GetAllUsersAsync();

            return this.View(allUsers);
        }

        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (await this.userSerice.UserExistsAsync(id) == false)
            {
                this.TempData[ErrorMessage] = ErrorMessageUser;
            }

            await this.userSerice.DeleteUserAsync(id);

            return this.RedirectToAction(nameof(All));
        }
    }
}

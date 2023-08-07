using CocktailHeaven.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

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
            var allUsers = await userSerice.GetAllUsersAsync();

            return View(allUsers);
        }

        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (await this.userSerice.UserExistsAsync(id) == false)
            {
                return NotFound();
            }

            await this.userSerice.DeleteUserAsync(id);

            return RedirectToAction("All");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace CocktailHeaven.Areas.Admin.Controllers
{
	public class HomeController : BaseAdminController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

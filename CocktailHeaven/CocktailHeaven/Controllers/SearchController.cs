using Microsoft.AspNetCore.Mvc;

namespace CocktailHeaven.Controllers
{
	public class SearchController : BaseController
	{
		public  IActionResult Index()
		{
			return View();
		}
	}
}

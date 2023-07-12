using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CocktailHeaven.Controllers
{
	public class UserCollectionController : BaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

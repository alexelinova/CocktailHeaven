using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailHeaven.Controllers
{
	[Authorize]
	public class BaseController : Controller
	{
	}
}

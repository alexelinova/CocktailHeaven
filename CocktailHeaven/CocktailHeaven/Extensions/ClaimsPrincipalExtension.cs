using System.Security.Claims;

namespace CocktailHeaven.Extensions
{
	public static class ClaimsPrincipalExtension
	{
		public static Guid Id(this ClaimsPrincipal user)
		{
			var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

			return Guid.Parse(userId);
		}
	}
}

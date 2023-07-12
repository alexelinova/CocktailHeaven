using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core;
using CocktailHeaven.Infrastructure.Data.Common;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class CocktailHeavenServiceCollectionExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IRepository, CocktailHeavenRepository>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<ICocktailService, CocktailService>();
			services.AddScoped<IAccountService, AccountService>();

			return services;
		}
	}
}

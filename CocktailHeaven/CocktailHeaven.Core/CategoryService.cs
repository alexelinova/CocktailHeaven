using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.Category;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CocktailHeaven.Core
{
	public class CategoryService : ICategoryService
	{
		private readonly IRepository repo;

        public CategoryService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
		{
			return await repo.AllReadonly<Category>()
				.Select(x => new CategoryViewModel()
				{
					Id = x.Id,
					Name = x.Name,
				})
				.ToListAsync();
		}
	}
}

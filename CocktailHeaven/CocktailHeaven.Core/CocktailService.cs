using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.Cocktail;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Models;

namespace CocktailHeaven.Core
{
	public class CocktailService : ICocktailService
	{
		private readonly IRepository repo;

        public CocktailService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task CreateCocktailAsync(AddCocktailFormModel model, Guid userId)
		{
			var cocktail = new Cocktail()
			{
				Name = model.Name,
				Instruction = model.Instruction,
				Description = model.Description,
				Garnish = model.Garnish,
				CategoryId = model.CategoryId,
				CreatedOn = DateTime.UtcNow,
				AddedByUserId = userId,
				Image = new Image()
				{
					ExternalURL = model.ImageURL
				}
			};

			foreach (var input in model.Ingredients)
			{
				var ingredient = repo.All<Ingredient>().FirstOrDefault(x => x.Name == input.IngredientName);

				if (ingredient == null) 
				{
					ingredient = new Ingredient()
					{
						Name = input.IngredientName,
					};
				}

				cocktail.Ingredients.Add(new CocktailIngredient()
				{
					Ingredient = ingredient,
					Quantity = input.Quantity,
					Note = input.Note,
				});
			}
				
			await repo.AddAsync(cocktail);
			await repo.SaveChangesAsync();
		}
	}
}

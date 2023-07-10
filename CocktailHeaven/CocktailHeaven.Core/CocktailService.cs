using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Core.Models.Cocktail;
using CocktailHeaven.Core.Models.Ingredient;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<RandomCocktailModel> GetRandomCocktailAsync()
        {
            var rand = new Random();
            int skipper = rand.Next(0, repo.AllReadonly<Cocktail>().Count());

            var randomCocktail = await repo.AllReadonly<Cocktail>()
				.Include(c => c.Image)
                .Include(c => c.Ingredients)
                .ThenInclude(i => i.Ingredient)
                .OrderBy(c => Guid.NewGuid())
                .Skip(skipper)
                .FirstOrDefaultAsync();

            return new RandomCocktailModel()
            {
                Name = randomCocktail.Name,
                Description = randomCocktail.Description,
                Instructions = randomCocktail.Instruction,
                Image = randomCocktail.Image != null ? randomCocktail.Image.ExternalURL : null,
                Ingredients = randomCocktail.Ingredients.Select(i => new IngredientFormModel()
                {
                    IngredientName = i.Ingredient.Name,
                    Quantity = i.Quantity,
                    Note = i.Note,
                }).ToList()
            };
        }
    }
}

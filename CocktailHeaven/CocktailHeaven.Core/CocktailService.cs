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

		public async Task<int> CocktailCountAsync()
		{
			return await this.repo.AllReadonly<Cocktail>().CountAsync();
		}

		public async Task CreateCocktailAsync(CocktailFormModel model, Guid userId)
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

				ingredient ??= new Ingredient()
				{
					Name = input.IngredientName,
				};

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

		public async Task Edit(CocktailEditModel model, int cocktailId)
		{
			var cocktail = await repo.All<Cocktail>(c => c.Id == cocktailId)
				.Include(c => c.Image)
				.Include(c => c.Ingredients)
				.ThenInclude(i => i.Ingredient)
				.FirstOrDefaultAsync();

			cocktail.Name = model.Name;
			cocktail.Description = model.Description;
			cocktail.CategoryId = model.CategoryId;
			cocktail.Instruction = model.Instruction;
			cocktail.Image.ExternalURL = model.ImageURL;
			cocktail.Garnish = model.Garnish;
			cocktail.ModifiedOn = DateTime.UtcNow;

			var ingredientsToRemove = cocktail.Ingredients
				.Where(ingredient => !model.Ingredients.Any(x => x.IngredientName == ingredient.Ingredient.Name))
				.ToList();

			foreach (var ingredientToRemove in ingredientsToRemove)
			{
				cocktail.Ingredients.Remove(ingredientToRemove);
			}

			foreach (var input in model.Ingredients)
			{
				var ingredient = repo.All<Ingredient>()
					.FirstOrDefault(x => x.Name == input.IngredientName);

				if (ingredient == null)
				{
					ingredient = new Ingredient()
					{
						Name = input.IngredientName,
					};
				}

				var currentCocktailIngredient = cocktail.Ingredients
					.FirstOrDefault(ci => ci.Ingredient.Name == input.IngredientName);

				if (currentCocktailIngredient != null)
				{
					currentCocktailIngredient.Quantity = input.Quantity;
					currentCocktailIngredient.Note = input.Note;
				}
				else
				{
					var newCocktailIngredient = new CocktailIngredient()
					{
						Ingredient = ingredient,
						Quantity = input.Quantity,
						Note = input.Note,
					};

					cocktail.Ingredients.Add(newCocktailIngredient);
				}
			}

			await repo.SaveChangesAsync();
		}

		public async Task<CocktailFullModel> GetCocktailByIdAsync(int id)
		{
			var cocktail = await this.repo.All<Cocktail>()
				.Where(c => c.Id == id && c.IsDeleted == false)
				.Select(c => new CocktailFullModel()
				{
					Id = c.Id,
					Name = c.Name,
					Description = c.Description,
					Instructions = c.Instruction,
					Image = c.Image.ExternalURL ?? string.Empty,
					Garnish = c.Garnish,
					CategoryName = c.Category.Name,
					Ingredients = c.Ingredients.Select(i => new IngredientFormModel()
					{
						IngredientName = i.Ingredient.Name,
						Quantity = i.Quantity,
						Note = i.Note
					}).ToList()
				}).FirstOrDefaultAsync();

			if (cocktail == null)
			{
				throw new ArgumentException("Cocktail not found!");
			}

			return cocktail;
		}

		public async Task<int> GetCocktailCategoryAsync(int cocktailId)
		{
			return (await repo.GetByIdAsync<Cocktail>(cocktailId)).CategoryId;
		}

		public async Task<IEnumerable<CocktailDetailsModel>> GetCocktailDetailsAsync(int page, int itemsPerPage = 6)
		{
			return await this.repo
				 .AllReadonly<Cocktail>(c => c.IsDeleted == false)
				 .OrderBy(c => c.Name)
				 .Skip((page - 1) * itemsPerPage)
				 .Take(itemsPerPage)
				 .Select(c => new CocktailDetailsModel()
				 {
					 Id = c.Id,
					 Name = c.Name,
					 Description = c.Description,
					 Url = c.Image.ExternalURL ?? string.Empty,
					 AverageRatingValue = c.Ratings.Any() ? c.Ratings.Average(x => x.Value) : 0
				 })
				 .ToListAsync();
		}

		public async Task<CocktailFullModel> GetRandomCocktailAsync()
		{
			var rand = new Random();
			int skipper = rand.Next(0, repo.AllReadonly<Cocktail>().Count());

			var randomCocktail = await repo.AllReadonly<Cocktail>()
				.Include(c => c.Image)
				.Include(c => c.Ingredients)
				.ThenInclude(i => i.Ingredient)
				.Where(c => !c.IsDeleted)
				.OrderBy(c => Guid.NewGuid())
				.Skip(skipper)
				.FirstOrDefaultAsync();

			return new CocktailFullModel()
			{
				Id = randomCocktail.Id,
				Name = randomCocktail.Name,
				Description = randomCocktail.Description,
				Instructions = randomCocktail.Instruction,
				Image = randomCocktail.Image.ExternalURL ?? string.Empty,
				Ingredients = randomCocktail.Ingredients.Select(i => new IngredientFormModel()
				{
					IngredientName = i.Ingredient.Name,
					Quantity = i.Quantity,
					Note = i.Note,
				})
				.ToList()
			};
		}

		public async Task<IEnumerable<CocktailCollectionModel>> GetTopRatedCocktailsAsync()
		{
			return await this.repo.AllReadonly<Cocktail>()
				.Where(c => c.IsDeleted == false)
				.OrderByDescending(c => c.Ratings.Any() ? c.Ratings.Average(r => r.Value) : 0)
				.Select(c => new CocktailCollectionModel()
				{
					Id = c.Id,
					Name = c.Name,
					ImageUrl = c.Image.ExternalURL ?? string.Empty,
				})
				.Take(3)
				.ToListAsync();
		}
	}
}

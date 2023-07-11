using CocktailHeaven.Core.Models.Category;
using CocktailHeaven.Core.Models.Ingredient;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static CocktailHeaven.Infrastructure.Models.DataConstants.CocktailConstants;

namespace CocktailHeaven.Core.Models.Cocktail
{
	public class CocktailFormModel
	{
		[Required]
		[StringLength(MaxNameLength, MinimumLength = MinNameLength)]
		public string Name { get; set; } = null!;

		[Required]
		[StringLength(MaxDescriptionLength, MinimumLength = MinDescriptionLength)]
		public string Description { get; set; } = null!;

		[Required]
		[StringLength(MaxInstructionLength, MinimumLength = MinInstructionLength)]
		[DisplayName("Instructions")]
		public string Instruction { get; set; } = null!;

		[StringLength(MaxGarnishLength, MinimumLength = MinGarnishLength)]
		public string? Garnish { get; set; }

		[Required]
		[Display(Name = "Category")]
		public int CategoryId { get; set; }

		[Range(0, 20)]
        public int CountOfIngredients { get; set; }

		[Url]
        public string? ImageURL { get; set; }

		public List<IngredientFormModel> Ingredients { get; set; } = null!;

		public IEnumerable<CategoryViewModel>? Categories { get; set; }
	}
}


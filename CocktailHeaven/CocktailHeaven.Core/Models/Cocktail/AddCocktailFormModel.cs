using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using static CocktailHeaven.Infrastructure.Models.DataConstants.CocktailConstants;
using CocktailHeaven.Core.Models.Category;

namespace CocktailHeaven.Core.Models.Cocktail
{
	public class AddCocktailFormModel
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

        public int CountOfIngredients { get; set; }

        public string? ImageURL { get; set; }
        public List<IngredientFormModel> Ingredients { get; set; }

		public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
	}
}


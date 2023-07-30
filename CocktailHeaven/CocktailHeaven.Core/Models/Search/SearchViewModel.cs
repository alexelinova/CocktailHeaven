using CocktailHeaven.Core.Models.Category;
using CocktailHeaven.Core.Models.Cocktail;
using System.Security.Policy;

namespace CocktailHeaven.Core.Models.Search
{
	public class SearchViewModel
	{
		public string? Category { get; set; }

		public SearchCriteria? SearchCriteria { get; set; }

		public string? SearchQuery { get; set; }

		public IEnumerable<CocktailSearchModel>? Cocktails { get; set; }

		public IEnumerable<string> Categories { get; set; } = null!;
	}
}

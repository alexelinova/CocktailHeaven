using CocktailHeaven.Core.Models.Cocktail;

namespace CocktailHeaven.Core.Models.Search
{
	public class SearchViewModel : PagingModel
	{
		public string? Category { get; set; }

		public SearchCriteria? SearchCriteria { get; set; }

		public string? SearchQuery { get; set; }

		public IEnumerable<CocktailSearchModel>? Cocktails { get; set; }

		public IEnumerable<string> Categories { get; set; } = null!;
	}
}

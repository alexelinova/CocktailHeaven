namespace CocktailHeaven.Core.Models
{
	public class PagingModel
	{
		public int PageNumber { get; set; }

		public bool HasPreviousPage => this.PageNumber > 1;

		public int PreviousPage => this.PageNumber - 1;

		public bool HasNexPage => this.PageNumber < this.PagesCount;

		public int NextPage => this.PageNumber + 1;

		public int PagesCount => (int)Math.Ceiling((double)this.CocktailsCount / this.CocktailsPerPage);

        public int CocktailsCount { get; set; }

		public int CocktailsPerPage { get; set; } = 6;
	}
}

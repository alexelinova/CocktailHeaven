namespace CocktailHeaven.Core.Models.Cocktail
{
	public class CocktailShowDetailsModel
	{
        public CocktailFullModel Cocktail { get; set; } = null!;

        public bool isInWishList { get; set; }

        public bool isInTried { get; set; }

        public bool isInFavourites { get; set; }
    }
}

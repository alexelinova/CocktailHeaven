namespace CocktailHeaven.Infrastructure.Models
{
    public class DataConstants
    {
        public class CocktailConstants
        {
			public const int MinNameLength = 5;
			public const int MaxNameLength = 50;

            public const int MinInstructionLength = 50;
			public const int MaxInstructionLength = 1000;

            public const int MinGarnishLength = 0;
            public const int MaxGarnishLength = 250;

			public const int MinDescriptionLength = 50;
			public const int MaxDescriptionLength = 500;
		}

        public class CategoryConstants
        {
            public const int MinCategoryLength = 3;
            public const int MaxCategoryLength = 50;
        }

        public class CocktailIngredientConstants
        {
            public const int MinQuantityLength = 3;
            public const int MaxQuantityLength = 50;

            public const int MinNoteLength = 0;
            public const int MaxNoteLength = 100;
        }
        public class IngredientConstants
        {
            public const int MinIngredientLength = 3;
            public const int MaxIngredientLength = 50;
        }

        public class RatingConstants
        {
            public const int MaxRatingValue = 5;
        }

		public class ImageConstants
		{
			public const int MaxExtensionLength = 3;
		}
	}
}

namespace CocktailHeaven.Infrastructure.Models
{
    public class DataConstants
    {
        public class CocktailConstants
        {
			public const int MinNameLength = 3;
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
			public const int MinRatingValue = 1;
			public const int MaxRatingValue = 5;

            public const int MaxCommentLength = 200;
        }

		public class ImageConstants
		{
			public const int MaxExtensionLength = 3;
		}

        public class UserLoginModel
        {
			public const int MinUserNameLength = 5;
			public const int MaxUserNameLength = 20;

            public const int MinEmailLength = 10;
			public const int MaxEmailLength = 60;

			public const int MinPasswordLength = 6;
			public const int MaxPasswordLength = 20;
		}

		public static class MessageConstant
		{
			public const string ErrorMessage = "ErrorMessage";
			public const string WarningMessage = "WarningMessage";
			public const string SuccessMessage = "SuccessMessage";

            public const string ErrorMessageCocktail = "Cocktail does not exist";
            public const string ErrorMessageCocktailExists = "A cocktail with the same name already exists. The cocktail cannot be saved";
            public const string ErrorMessageRandomCocktail = "Could not find a cocktail";
            public const string ErrorMessageRating = "Rating does not exist";
			public const string ErrorMessageUser = "User does not exist";
            public const string ErrorMessageRole = "Role does not exist";
            public const string ErrorMessageUserInRole = "The user is already in this role";
			public const string ErrorMessageUserNotInRole = "The user is not in this role";
            public const string ErrorMessageUserCollection = "You don't have this cocktail in your collections";

            public const string SuccessMessageUserToRole = "User successfully added to {0} role";
            public const string SuccessMessageUserRemovedFromRole = "User successfully removed from {0} role";

        }
	}
}

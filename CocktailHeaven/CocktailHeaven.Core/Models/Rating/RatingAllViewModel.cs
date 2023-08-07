namespace CocktailHeaven.Core.Models.Rating
{
    public class RatingAllViewModel
    {
        public int Id { get; set; }

        public float Value { get; set; }

        public string UserEmail { get; set; } = null!;

        public string? Comment { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}

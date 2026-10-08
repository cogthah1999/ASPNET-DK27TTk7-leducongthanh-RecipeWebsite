namespace RecipeWebsite.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public required string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public int RecipeId { get; set; }

        public User? User { get; set; }

        public Recipe? Recipe { get; set; }
    }
}
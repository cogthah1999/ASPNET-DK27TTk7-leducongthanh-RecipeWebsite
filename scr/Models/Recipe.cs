using System.ComponentModel.DataAnnotations;

namespace RecipeWebsite.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên món không được để trống.")]
        public required string Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Hướng dẫn không được để trống.")]
        public required string Instructions { get; set; }

        [Range(1, 1440, ErrorMessage = "Thời gian nấu phải từ 1 đến 1440 phút.")]
        public int CookingTime { get; set; }

        [Range(1, 100, ErrorMessage = "Số người ăn phải từ 1 đến 100.")]
        public int Servings { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        // Foreign Key
        public int UserId { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public List<RecipeIngredient> RecipeIngredients { get; set; } = new();

        public List<Comment> Comments { get; set; } = new();
    }
}
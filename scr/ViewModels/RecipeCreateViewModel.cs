using RecipeWebsite.Models;
using System.ComponentModel.DataAnnotations;


namespace RecipeWebsite.ViewModels
{
    public class RecipeIngredientInput
    {
        public int IngredientId { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Số lượng nguyên liệu phải lớn hơn 0.")]
        public decimal Quantity { get; set; }

        public string? Unit { get; set; }
    }
    public class RecipeCreateViewModel
    {
        public Recipe Recipe { get; set; } = new Recipe
        {
            Title = "",
            Instructions = ""
        };

        public List<Category> Categories { get; set; } = new();

        public List<Ingredient> Ingredients { get; set; } = new();

        public List<RecipeIngredientInput> RecipeIngredients { get; set; } = new();
    }
}
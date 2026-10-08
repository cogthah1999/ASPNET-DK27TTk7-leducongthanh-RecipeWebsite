using RecipeWebsite.Models;
using Microsoft.AspNetCore.Identity;

namespace RecipeWebsite.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Nếu đã có dữ liệu thì không seed lại
            if (context.Categories.Any())
            {
                return;
            }

            // =========================
            // 1. USERS
            // =========================

            var admin = new User
            {
                FullName = "Admin",
                Email = "admin@gmail.com",
                PasswordHash = new PasswordHasher<User>()
                    .HashPassword(null!, "123456"),
                Role = "Admin",
                CreatedAt = DateTime.Now
            };

            var user = new User
            {
                FullName = "Nguyen Van A",
                Email = "user@gmail.com",
                PasswordHash = new PasswordHasher<User>()
                    .HashPassword(null!, "123456"),
                Role = "User",
                CreatedAt = DateTime.Now
            };

            context.Users.AddRange(admin, user);
            context.SaveChanges();


            // =========================
            // 2. CATEGORIES
            // =========================

            var vietnamese = new Category
            {
                Name = "Món Việt",
                Description = "Các món ăn truyền thống Việt Nam"
            };

            var japanese = new Category
            {
                Name = "Món Nhật",
                Description = "Các món ăn phổ biến của Nhật Bản"
            };

            var european = new Category
            {
                Name = "Món Âu",
                Description = "Các món ăn phong cách châu Âu"
            };

            context.Categories.AddRange(
                vietnamese,
                japanese,
                european
            );

            context.SaveChanges();


            // =========================
            // 3. INGREDIENTS
            // =========================

            var chicken = new Ingredient
            {
                Name = "Thịt gà"
            };

            var egg = new Ingredient
            {
                Name = "Trứng"
            };

            var tomato = new Ingredient
            {
                Name = "Cà chua"
            };

            var fishSauce = new Ingredient
            {
                Name = "Nước mắm"
            };

            context.Ingredients.AddRange(
                chicken,
                egg,
                tomato,
                fishSauce
            );

            context.SaveChanges();


            // =========================
            // 4. RECIPES
            // =========================

            var recipe1 = new Recipe
            {
                Title = "Gà chiên nước mắm",
                Description = "Món gà chiên giòn kết hợp với nước mắm đậm đà.",
                Instructions = "Chiên gà vàng giòn. Sau đó pha nước mắm với gia vị và đảo đều với gà.",
                CookingTime = 30,
                Servings = 2,
                ImageUrl = "/images/ga-chien-nuoc-mam.jpg",
                CreatedAt = DateTime.Now,
                UserId = user.Id,
                CategoryId = vietnamese.Id
            };

            var recipe2 = new Recipe
            {
                Title = "Trứng chiên cà chua",
                Description = "Món ăn đơn giản, dễ làm và phù hợp cho bữa cơm gia đình.",
                Instructions = "Đánh trứng. Xào cà chua rồi cho trứng vào chiên đến khi chín.",
                CookingTime = 15,
                Servings = 2,
                ImageUrl = "/images/trung-chien-ca-chua.jpg",
                CreatedAt = DateTime.Now,
                UserId = user.Id,
                CategoryId = vietnamese.Id
            };

            context.Recipes.AddRange(recipe1, recipe2);
            context.SaveChanges();


            // =========================
            // 5. RECIPE INGREDIENTS
            // =========================

            var recipeIngredient1 = new RecipeIngredient
            {
                RecipeId = recipe1.Id,
                IngredientId = chicken.Id,
                Quantity = 500,
                Unit = "gram"
            };

            var recipeIngredient2 = new RecipeIngredient
            {
                RecipeId = recipe1.Id,
                IngredientId = fishSauce.Id,
                Quantity = 3,
                Unit = "muỗng"
            };

            var recipeIngredient3 = new RecipeIngredient
            {
                RecipeId = recipe2.Id,
                IngredientId = egg.Id,
                Quantity = 3,
                Unit = "quả"
            };

            var recipeIngredient4 = new RecipeIngredient
            {
                RecipeId = recipe2.Id,
                IngredientId = tomato.Id,
                Quantity = 2,
                Unit = "quả"
            };

            context.RecipeIngredients.AddRange(
                recipeIngredient1,
                recipeIngredient2,
                recipeIngredient3,
                recipeIngredient4
            );

            context.SaveChanges();


            // =========================
            // 6. COMMENTS
            // =========================

            var comment = new Comment
            {
                Content = "Món ăn rất ngon và dễ làm!",
                CreatedAt = DateTime.Now,
                UserId = user.Id,
                RecipeId = recipe1.Id
            };

            context.Comments.Add(comment);

            context.SaveChanges();
        }
    }
}
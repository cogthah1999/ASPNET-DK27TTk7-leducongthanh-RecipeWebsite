using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RecipeWebsite.Data;
using RecipeWebsite.ViewModels;
using RecipeWebsite.Models;

namespace RecipeWebsite.Controllers
{
    public class RecipeController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public RecipeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search, int? categoryId)
        {
            var recipes = _context.Recipes.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                recipes = recipes.Where(r =>
                    r.Title.Contains(search));
            }

            if (categoryId.HasValue)
            {
                recipes = recipes.Where(r =>
                    r.CategoryId == categoryId.Value);
            }

            ViewBag.Categories = _context.Categories.ToList();

            return View(recipes.ToList());
        }
        public IActionResult Details(int id)
        {
            var recipe = _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefault(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }
        public IActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var recipe = _context.Recipes
                .Include(r => r.RecipeIngredients)
                .FirstOrDefault(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin" && recipe.UserId != userId.Value)
            {
                return RedirectToAction("Index");
            }

            var viewModel = new RecipeCreateViewModel
            {
                Recipe = recipe,
                Categories = _context.Categories.ToList(),
                Ingredients = _context.Ingredients.ToList()
            };

            foreach (var item in recipe.RecipeIngredients)
            {
                viewModel.RecipeIngredients.Add(
                    new RecipeIngredientInput
                    {
                        IngredientId = item.IngredientId,
                        Quantity = item.Quantity,
                        Unit = item.Unit
                    }
                );
            }

            return View(viewModel);
        }
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var viewModel = new RecipeCreateViewModel
            {
                Categories = _context.Categories.ToList(),
                Ingredients = _context.Ingredients.ToList(),

                RecipeIngredients = new List<RecipeIngredientInput>
        {
            new RecipeIngredientInput()
        }
            };

            return View(viewModel);
        } //GET Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RecipeCreateViewModel viewModel)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                viewModel.Categories = _context.Categories.ToList();
                viewModel.Ingredients = _context.Ingredients.ToList();

                return View(viewModel);
            }

            var categoryExists = _context.Categories
                .Any(c => c.Id == viewModel.Recipe.CategoryId);

            if (!categoryExists)
            {
                ModelState.AddModelError(
                    "Recipe.CategoryId",
                    "Danh mục không tồn tại."
                );

                viewModel.Categories = _context.Categories.ToList();
                viewModel.Ingredients = _context.Ingredients.ToList();

                return View(viewModel);
            }

            var recipe = viewModel.Recipe;
            recipe.CreatedAt = DateTime.Now;

            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            recipe.UserId = userId.Value;

            _context.Recipes.Add(recipe);
            _context.SaveChanges();

            foreach (var input in viewModel.RecipeIngredients)
            {
                if (input.IngredientId <= 0)
                {
                    continue;
                }

                var recipeIngredient = new RecipeIngredient
                {
                    RecipeId = recipe.Id,
                    IngredientId = input.IngredientId,
                    Quantity = input.Quantity,
                    Unit = input.Unit
                };

                _context.RecipeIngredients.Add(recipeIngredient);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        } //POST Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RecipeCreateViewModel viewModel)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                viewModel.Categories = _context.Categories.ToList();
                viewModel.Ingredients = _context.Ingredients.ToList();

                return View(viewModel);
            }

            var recipe = _context.Recipes
                .FirstOrDefault(r => r.Id == viewModel.Recipe.Id);

            if (recipe == null)
            {
                return NotFound();
            }

            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin" && recipe.UserId != userId.Value)
            {
                return RedirectToAction("Index");
            }

            recipe.Title = viewModel.Recipe.Title;
            recipe.Description = viewModel.Recipe.Description;
            recipe.CookingTime = viewModel.Recipe.CookingTime;
            recipe.Servings = viewModel.Recipe.Servings;
            recipe.CategoryId = viewModel.Recipe.CategoryId;
            recipe.ImageUrl = viewModel.Recipe.ImageUrl;
            recipe.Instructions = viewModel.Recipe.Instructions;

            var oldIngredients = _context.RecipeIngredients
                .Where(x => x.RecipeId == recipe.Id)
                .ToList();

            _context.RecipeIngredients.RemoveRange(oldIngredients);

            foreach (var input in viewModel.RecipeIngredients)
            {
                if (input.IngredientId <= 0)
                {
                    continue;
                }

                var recipeIngredient = new RecipeIngredient
                {
                    RecipeId = recipe.Id,
                    IngredientId = input.IngredientId,
                    Quantity = input.Quantity,
                    Unit = input.Unit
                };

                _context.RecipeIngredients.Add(recipeIngredient);
            }

            _context.SaveChanges();

            return RedirectToAction("Details", new { id = recipe.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var recipe = _context.Recipes
                .FirstOrDefault(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin" && recipe.UserId != userId.Value)
            {
                return RedirectToAction("Index");
            }

            var ingredients = _context.RecipeIngredients
                .Where(x => x.RecipeId == id)
                .ToList();

            _context.RecipeIngredients.RemoveRange(ingredients);

            _context.Recipes.Remove(recipe);

            _context.SaveChanges();

            if (role == "Admin")
            {
                return RedirectToAction("Recipes", "Admin");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(int recipeId, string content)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                return RedirectToAction("Details", new { id = recipeId });
            }

            var recipe = _context.Recipes
                .FirstOrDefault(r => r.Id == recipeId);

            if (recipe == null)
            {
                return NotFound();
            }

            var comment = new Comment
            {
                Content = content,
                CreatedAt = DateTime.Now,
                UserId = userId.Value,
                RecipeId = recipeId
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = recipeId });
        }
    }
}
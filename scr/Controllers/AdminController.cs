using Microsoft.AspNetCore.Mvc;
using RecipeWebsite.Data;
using Microsoft.EntityFrameworkCore;
using RecipeWebsite.Models;

namespace RecipeWebsite.Controllers
{
    public class AdminController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Recipe");
            }

            ViewBag.UserCount = _context.Users.Count();
            ViewBag.RecipeCount = _context.Recipes.Count();
            ViewBag.CategoryCount = _context.Categories.Count();
            ViewBag.CommentCount = _context.Comments.Count();

            return View();
        }

        public IActionResult Recipes()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Recipe");
            }

            var recipes = _context.Recipes
                .Include(r => r.Category)
                .ToList();

            return View(recipes);
        }

        public IActionResult Categories()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Recipe");
            }

            var categories = _context.Categories.ToList();

            return View(categories);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Recipe");
            }

            return View();
        }   //GET CreateCategory

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(Category category)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Recipe");
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Categories");
        }   //POST CreateCategory

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Recipe");
            }

            var category = _context.Categories
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        } //GET EditCategory

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(Category category)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Recipe");
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var existingCategory = _context.Categories
                .FirstOrDefault(c => c.Id == category.Id);

            if (existingCategory == null)
            {
                return NotFound();
            }

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;

            _context.SaveChanges();

            return RedirectToAction("Categories");
        } //POST EditCategory

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Recipe");
            }

            var category = _context.Categories
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            var hasRecipes = _context.Recipes
                .Any(r => r.CategoryId == id);

            if (hasRecipes)
            {
                TempData["Error"] =
                    "Không thể xóa danh mục vì đang có công thức sử dụng danh mục này.";

                return RedirectToAction("Categories");
            }

            _context.Categories.Remove(category);

            _context.SaveChanges();

            return RedirectToAction("Categories");
        }

        public IActionResult Users()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Recipe");
            }

            var users = _context.Users
                .ToList();

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUser(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Recipe");
            }

            var user = _context.Users
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            // Không cho Admin tự xóa chính mình
            var currentUserId = HttpContext.Session.GetInt32("UserId");

            if (user.Id == currentUserId)
            {
                TempData["Error"] = "Bạn không thể tự xóa tài khoản Admin đang đăng nhập.";

                return RedirectToAction("Users");
            }

            var recipes = _context.Recipes
    .Where(r => r.UserId == user.Id)
    .ToList();

            foreach (var recipe in recipes)
            {
                var recipeIngredients = _context.RecipeIngredients
                    .Where(ri => ri.RecipeId == recipe.Id)
                    .ToList();

                _context.RecipeIngredients.RemoveRange(recipeIngredients);

                var comments = _context.Comments
                    .Where(c => c.RecipeId == recipe.Id)
                    .ToList();

                _context.Comments.RemoveRange(comments);
            }

            _context.Recipes.RemoveRange(recipes);

            var userComments = _context.Comments
                .Where(c => c.UserId == user.Id)
                .ToList();

            _context.Comments.RemoveRange(userComments);

            _context.Users.Remove(user);

            _context.SaveChanges();

            return RedirectToAction("Users");
        }

        public IActionResult Comments()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Recipe");
            }

            var comments = _context.Comments
                .Include(c => c.User)
                .Include(c => c.Recipe)
                .ToList();

            return View(comments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteComment(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Recipe");
            }

            var comment = _context.Comments
                .FirstOrDefault(c => c.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);

            _context.SaveChanges();

            return RedirectToAction("Comments");
        }
    }
}
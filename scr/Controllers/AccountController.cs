using Microsoft.AspNetCore.Mvc;
using RecipeWebsite.Data;
using RecipeWebsite.Models;
using RecipeWebsite.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace RecipeWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingUser = _context.Users
                .FirstOrDefault(u => u.Email == model.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError(
                    "Email",
                    "Email đã được sử dụng.");

                return View(model);
            }


            var passwordHasher = new PasswordHasher<User>();

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                PasswordHash = passwordHasher.HashPassword(null!, model.Password),
                Role = "User",
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.Users
    .FirstOrDefault(u => u.Email == model.Email);

            if (user == null)
            {
                ModelState.AddModelError(
                    "",
                    "Email hoặc mật khẩu không đúng.");

                return View(model);
            }

            var passwordHasher = new PasswordHasher<User>();

            var result = passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                model.Password
            );

            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(
                    "",
                    "Email hoặc mật khẩu không đúng.");

                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Index", "Recipe");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Recipe");
        }
    }
}
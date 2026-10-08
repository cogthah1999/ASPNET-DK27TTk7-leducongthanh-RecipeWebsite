using Microsoft.AspNetCore.Mvc;

namespace RecipeWebsite.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsAdmin()
        {
            var role = HttpContext.Session.GetString("Role");

            return role == "Admin";
        }

        protected bool IsLoggedIn()
        {
            return HttpContext.Session.GetInt32("UserId") != null;
        }
    }
}
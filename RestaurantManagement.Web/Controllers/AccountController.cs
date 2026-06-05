using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Entities;
using RestaurantManagement.Repos;

namespace RestaurantManagement.Web.Controllers
{
    public class AccountController(UserRepo userRepo)
        : Controller
    {
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserID") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var result = userRepo.Login(email, password);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            var role = result.Data.Role?.Trim();

            if (string.IsNullOrEmpty(role))
            {
                role = "Customer";
            }

           
            HttpContext.Session.SetString("UserRole", role.ToLower());

            HttpContext.Session.SetInt32("UserID", result.Data.ID);
            HttpContext.Session.SetString("Username", result.Data.Username);

            if (role.ToLower() == "admin")
            {
                return RedirectToAction("Index", "AdminManagement");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User model)
        {
            ModelState.Remove("ID");
            ModelState.Remove("Role");
            ModelState.Remove("Active");
            ModelState.Remove("Created_at");
            ModelState.Remove("Updated_at");
            ModelState.Remove("Updated_by");
            ModelState.Remove("Gender");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

           
            model.Role = "Customer";

            var result = userRepo.Register(model);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(model);
            }

            TempData["Success"] = "Registration Successful";

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }
    }
}
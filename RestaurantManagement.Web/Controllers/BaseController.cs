using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.Web.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsLoggedIn()
        {
            return HttpContext.Session.GetInt32("UserID") != null;
        }

        protected IActionResult CheckLogin()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            return null;
        }
    }
}
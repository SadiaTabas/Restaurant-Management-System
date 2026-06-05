using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Helpers;

namespace RestaurantManagement.Web.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = SessionCart.Get(HttpContext.Session);
            return View(cart);
        }

        public IActionResult Plus(int id)
        {
            var cart = SessionCart.Get(HttpContext.Session);

            var item = cart.FirstOrDefault(x => x.MenuItemId == id);
            if (item != null) item.Quantity++;

            SessionCart.Save(HttpContext.Session, cart);
            return RedirectToAction("Index");
        }

        public IActionResult Minus(int id)
        {
            var cart = SessionCart.Get(HttpContext.Session);

            var item = cart.FirstOrDefault(x => x.MenuItemId == id);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                    cart.Remove(item);
            }

            SessionCart.Save(HttpContext.Session, cart);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var cart = SessionCart.Get(HttpContext.Session);

            var item = cart.FirstOrDefault(x => x.MenuItemId == id);
            if (item != null) cart.Remove(item);

            SessionCart.Save(HttpContext.Session, cart);
            return RedirectToAction("Index");
        }
    }
}
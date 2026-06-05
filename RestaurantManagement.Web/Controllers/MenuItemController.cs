using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantManagement.Entities;
using RestaurantManagement.Repos;
using RestaurantManagement.Helpers;
namespace RestaurantManagement.Web.Controllers
{
    public class MenuItemController(MenuItemRepo menuItemRepo, CategoriesRepo categoriesRepo)
        : Controller
    {
        public IActionResult Index()
        {
            var result = menuItemRepo.GetAll();
            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(new List<MenuItem>());
            }
            return View(result.Data);
        }

        public IActionResult AddToCart(int id)
        {
            var item = menuItemRepo.GetById(id);

            if (item.HasError || item.Data == null)
                return RedirectToAction("Index");

            var cart = SessionCart.Get(HttpContext.Session);

            var existing = cart.FirstOrDefault(x => x.MenuItemId == id);

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    MenuItemId = item.Data.ID,
                    Name = item.Data.Name,
                    Price = item.Data.Price,
                    Quantity = 1
                });
            }

            SessionCart.Save(HttpContext.Session, cart);

            return RedirectToAction("Index");
        }
        public IActionResult Detail(int dataId)
        {
            var resultcategories = categoriesRepo.GetAll();
            if (resultcategories.HasError)
            {
                TempData["Error"] = resultcategories.Message;
                return RedirectToAction("Index");
            }

            ViewBag.CategoriesList = resultcategories.Data.Select
                (e => new SelectListItem()
                {
                    Text = e.Name,
                    Value = e.ID.ToString()
                }
                );


            if (dataId == -1)
            {
                return View(new MenuItem());
            }
            var result = menuItemRepo.GetById(dataId);
            if (result.HasError)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }
            return View(result.Data);
        }
        [HttpPost]
        public IActionResult Detail(MenuItem model)
        {
            var resultcategories = categoriesRepo.GetAll();
            if (resultcategories.HasError)
            {
                TempData["Error"] = resultcategories.Message;
                return RedirectToAction("Index");
            }
            ViewBag.CategoriesList = resultcategories.Data.Select
                (e => new SelectListItem()
                {
                    Text = e.Name,
                    Value = e.ID.ToString()
                }
                );
            ModelState.Remove("Updated_at");
            ModelState.Remove("Updated_by");
            ModelState.Remove("Categories");

            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            var result = menuItemRepo.Save(model);
            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(model);
            }
            else
            {
                if (result.Data != null)
                {
                    TempData["Success"] =
                        $"Data# {result.Data.ID} saved successfully";
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int dataId)
        {
            var result = menuItemRepo.Delete(dataId);
            if (result.HasError)
            {
                TempData["Error"] = result.Message;

                return View(result);
            }
            else
            {
                TempData["Success"] =
                    $"Menu Item {dataId} deleted successfully";
            }
            return RedirectToAction("Index");
        }


    }
}
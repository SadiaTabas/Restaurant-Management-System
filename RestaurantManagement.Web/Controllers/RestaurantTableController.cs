using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Entities;
using RestaurantManagement.Repos;

namespace RestaurantManagement.Web.Controllers
{
    public class RestaurantTableController(RestaurantTableRepo restaurantTableRepo)
        : Controller
    {
        public IActionResult Index()
        {
            var result = restaurantTableRepo.GetAll();

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(new List<RestaurantTable>());
            }

            return View(result.Data);
        }

        public IActionResult Detail(int dataId)
        {
            if (dataId == -1)
            {
                return View(new RestaurantTable());
            }

            var result = restaurantTableRepo.GetById(dataId);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Detail(RestaurantTable model)
        {
            ModelState.Remove("Created_at");
            ModelState.Remove("Updated_at");

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var result = restaurantTableRepo.Save(model);

            if (result.HasError)
            {
                ViewBag.error = result.Message;
                return View(model);
            }
            else
            {
                if (result.Data != null)
                    TempData["Success"] =
                        $"Table# {result.Data.ID} saved successfully";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int dataId)
        {
            var result = restaurantTableRepo.Delete(dataId);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;
            }
            else
            {
                TempData["Success"] =
                    $"Table {dataId} deleted successfully";
            }

            return RedirectToAction("Index");
        }
    }
}
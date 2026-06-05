using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Entities;
using RestaurantManagement.Repos;

namespace RestaurantManagement.Web.Controllers
{
    public class CategoriesController(CategoriesRepo categoriesRepo)
        : Controller
    {
        public IActionResult Index()
        {
            var result = categoriesRepo.GetAll();

            if (result.HasError)
            {
                ViewBag.Error = result.Message;

                return View(new List<Categories>());
            }

            return View(result.Data);
        }

        public IActionResult Detail(int dataId)
        {
            if (dataId == -1)
            {
                return View(new Categories());
            }

            var result = categoriesRepo.GetById(dataId);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;

                return RedirectToAction("Index");
            }

            return View(result.Data);
        }
        [HttpPost]
        public IActionResult Detail(Categories model)
        {
            ModelState.Remove("Created_at");
            ModelState.Remove("Updated_at");
            

            if (ModelState.IsValid == false)
            {
                return View(model); }

                var result = categoriesRepo.Save(model);
            if (result.HasError)
            {
                ViewBag.error = result.Message;
                return View(model);
            }
            else
            {
                if (result.Data != null) TempData["Success"] =
                     $"Data# {result.Data.ID} saved successfully";
            }
                    return RedirectToAction("Index");
                }
            
            
           
        


        public IActionResult Delete(int dataId)
        {
            var result = categoriesRepo.Delete(dataId);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;
            }
            else
            {
                TempData["Success"] =
                    $"Category {dataId} deleted successfully";
            }

            return RedirectToAction("Index");
        }
    }
}
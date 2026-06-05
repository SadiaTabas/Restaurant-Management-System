using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Entities;
using RestaurantManagement.Repos;

namespace RestaurantManagement.Web.Controllers
{
    public class AdminManagementController(AdminRepo adminRepo) : Controller
    {
        public IActionResult Index()
        {
            var result = adminRepo.GetAll();

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(new List<Admin>());
            }

            return View(result.Data);
        }

        public IActionResult Detail(int dataId)
        {
            if (dataId == -1)
            {
                return View(new Admin
                {
                    Joined_At = DateTime.Now
                });
            }

            var result = adminRepo.GetById(dataId);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Detail(Admin model)
        {
            ModelState.Remove("ID");

            if (!ModelState.IsValid)
                return View(model);

            var result = adminRepo.Save(model);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(model);
            }

            TempData["Success"] = "Saved successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int dataId)
        {
            var result = adminRepo.Delete(dataId);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;
            }
            else
            {
                TempData["Success"] = "Deleted successfully";
            }

            return RedirectToAction("Index");
        }
    }
}
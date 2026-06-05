using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Entities;
using RestaurantManagement.Repos;

namespace RestaurantManagement.Web.Controllers
{
    public class CustomerController(CustomerRepo customerRepo)
        : Controller
    {
        public IActionResult Index()
        {
            var result = customerRepo.GetAll();

            if (result.HasError)
            {
                ViewBag.Error = result.Message;

                return View(new List<Customer>());
            }

            return View(result.Data);
        }

        public IActionResult Detail(int dataId)
        {
            if (dataId == -1)
            {
                return View(new Customer());
            }

            var result = customerRepo.GetById(dataId);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;

                return RedirectToAction("Index");
            }

            return View(result.Data);
        }
        [HttpPost]
        public IActionResult Detail(Customer model)
        {
             ModelState.Remove("CreatedAt");
            ModelState.Remove("UpdatedAt");

            if (ModelState.IsValid == false)
            {
                return View(model); 
            }

            
            var result = customerRepo.Save(model);
            if (result.HasError)
            {
                ViewBag.error = result.Message;
                return View(model);
            }
            else
            {
                if (result.Data != null) 

                    TempData["Success"] = $"Data# {result.Data.Id} saved successfully";
            }
                    return RedirectToAction("Index");
                }
            
            
           
        


        public IActionResult Delete(int dataId)
        {
            var result = customerRepo.Delete(dataId);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;
            }
            else
            {
                TempData["Success"] =
                    $"Customer {dataId} deleted successfully";
            }

            return RedirectToAction("Index");
        }
    }
}
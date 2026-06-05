using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Entities;
using RestaurantManagement.Repos;

namespace RestaurantManagement.Web.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly FeedbackRepo repo;

        public FeedbackController(FeedbackRepo repo)
        {
            this.repo = repo;
        }

     
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Feedback());
        }

      
        [HttpPost]
        public IActionResult Create(Feedback model)
        {
            model.Customer_ID = HttpContext.Session.GetInt32("UserID") ?? 0;

            var result = repo.Add(model);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(model);
            }

            TempData["Success"] = "Feedback submitted successfully";
            return RedirectToAction("MyFeedback");
        }

    
        public IActionResult MyFeedback()
        {
            int customerId = HttpContext.Session.GetInt32("UserID") ?? 0;

            var result = repo.GetByCustomer(customerId);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(new List<Feedback>());
            }

            return View(result.Data);
        }
    }
}
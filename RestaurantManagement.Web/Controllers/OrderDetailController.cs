using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Repos;

namespace RestaurantManagement.Web.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly OrderDetailRepo repo;

        public OrderDetailController(OrderDetailRepo repo)
        {
            this.repo = repo;
        }

    
        public IActionResult Index(int orderId)
        {
            var result = repo.GetByOrderId(orderId);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(new List<RestaurantManagement.Entities.OrderDetail>());
            }

            ViewBag.OrderId = orderId;
            return View(result.Data);
        }
    }
}
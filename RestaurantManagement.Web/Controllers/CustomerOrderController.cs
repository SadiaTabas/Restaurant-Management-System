using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Helpers;
using RestaurantManagement.Entities;
using RestaurantManagement.Repos;

namespace RestaurantManagement.Web.Controllers
{
    public class CustomerOrderController : Controller
    {
        private readonly TableOrderRepo repo;
        private readonly RestaurantTableRepo tableRepo;

        public CustomerOrderController(
            TableOrderRepo repo,
            RestaurantTableRepo tableRepo)
        {
            this.repo = repo;
            this.tableRepo = tableRepo;
        }

       
        public IActionResult PlaceOrder()
        {
            return View(new TableOrder());
        }

        [HttpPost]
        public IActionResult PlaceOrder(TableOrder model)
        {
            model.Customer_ID = HttpContext.Session.GetInt32("UserID") ?? 0;

            var result = repo.PlaceOrder(model);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(model);
            }

            TempData["Success"] = "Order Placed Successfully";
            return RedirectToAction("MyOrders");
        }

        
        public IActionResult MyOrders()
        {
            int customerId = HttpContext.Session.GetInt32("UserID") ?? 0;

            var result = repo.GetByCustomer(customerId);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(new List<TableOrder>());
            }

            return View(result.Data);
        }
 

        public IActionResult Checkout()
        {
            var cart = SessionCart.Get(HttpContext.Session);

            if (cart == null || !cart.Any())
            {
                TempData["Error"] = "Cart is empty";
                return RedirectToAction("Index", "Cart");
            }

            ViewBag.Tables = tableRepo.GetAll().Data;  

            var model = new TableOrder
            {
                Total_Amount = cart.Sum(x => x.Price * x.Quantity)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Checkout(TableOrder model)
        {
            var cart = SessionCart.Get(HttpContext.Session);

            if (cart == null || !cart.Any())
            {
                TempData["Error"] = "Cart is empty";
                return RedirectToAction("Index", "Cart");
            }

            model.Customer_ID = HttpContext.Session.GetInt32("UserID") ?? 0;

            
            var tableExists = tableRepo.GetAll().Data?
                .Any(t => t.ID == model.Table_ID) ?? false;

            if (!tableExists || model.Table_ID == 0)
            {
                ViewBag.Error = "Please select a valid table.";

                ViewBag.Tables = tableRepo.GetAll().Data;
                model.Total_Amount = cart.Sum(x => x.Price * x.Quantity);

                return View(model);
            }

            var result = repo.Checkout(model, cart);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                ViewBag.Tables = tableRepo.GetAll().Data;

                return View(model);
            }

            SessionCart.Clear(HttpContext.Session);

            TempData["Success"] = "Order placed successfully";

            return RedirectToAction("Bill", new { dataId = result.Data.ID });
        }

        
        public IActionResult Bill(int dataId)
        {
            var result = repo.GetById(dataId);

            if (result.HasError || result.Data == null)
            {
                TempData["Error"] = "Order not found";
                return RedirectToAction("MyOrders");
            }

            var items = repo.GetOrderItems(dataId);

            ViewBag.Items = items;

            return View(result.Data);
        }


    }
}
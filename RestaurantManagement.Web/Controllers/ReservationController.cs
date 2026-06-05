using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantManagement.Entities;
using RestaurantManagement.Repos;

namespace RestaurantManagement.Web.Controllers
{
    public class ReservationController(
        ReservationRepo reservationRepo,
        CustomerRepo customerRepo,
        RestaurantTableRepo restaurantTableRepo)
        : BaseController
    {
        public IActionResult Index()
        {
            var check = CheckLogin();
            if (check != null) return check;


            var result = reservationRepo.GetAll();

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(new List<Reservation>());
            }

            return View(result.Data);
        }

        public IActionResult Detail(int dataId)
        {
            var check = CheckLogin();
            if (check != null) return check;
            var resultCustomer = customerRepo.GetAll();
            if (resultCustomer.HasError)
            {
                TempData["Error"] = resultCustomer.Message;
                return RedirectToAction("Index");
            }

            ViewBag.CustomerList = resultCustomer.Data.Select(e => new SelectListItem()
            {
                Text = e.Address,
                Value = e.Id.ToString()
            });

            var resultTable = restaurantTableRepo.GetAll();
            if (resultTable.HasError)
            {
                TempData["Error"] = resultTable.Message;
                return RedirectToAction("Index");
            }

            ViewBag.TableList = resultTable.Data.Select(e => new SelectListItem()
            {
                Text = e.Table_number.ToString(),
                Value = e.ID.ToString()
            });

            if (dataId == -1)
            {
                return View(new Reservation
                {
                    Reservation_time = DateTime.Now,
                    Status = "Pending"
                });
            }

            var result = reservationRepo.GetById(dataId);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Detail(Reservation model)
        {
            var check = CheckLogin();
            if (check != null) return check;

            var resultCustomer = customerRepo.GetAll();
            if (resultCustomer.HasError)
            {
                TempData["Error"] = resultCustomer.Message;
                return RedirectToAction("Index");
            }

            ViewBag.CustomerList = resultCustomer.Data.Select(e => new SelectListItem()
            {
                Text = e.Address,
                Value = e.Id.ToString()
            });

            var resultTable = restaurantTableRepo.GetAll();
            if (resultTable.HasError)
            {
                TempData["Error"] = resultTable.Message;
                return RedirectToAction("Index");
            }

            ViewBag.TableList = resultTable.Data.Select(e => new SelectListItem()
            {
                Text = e.Table_number.ToString(),
                Value = e.ID.ToString()
            });

            ModelState.Remove("Updated_at");
            ModelState.Remove("Updated_by");
            ModelState.Remove("Customer");
            ModelState.Remove("RestaurantTable");

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var result = reservationRepo.Save(model);

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
            var check = CheckLogin();
            if (check != null) return check;
            var result = reservationRepo.Delete(dataId);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;
                return View(result);
            }
            else
            {
                TempData["Success"] =
                    $"Reservation {dataId} deleted successfully";
            }

            return RedirectToAction("Index");
        }
    }
}
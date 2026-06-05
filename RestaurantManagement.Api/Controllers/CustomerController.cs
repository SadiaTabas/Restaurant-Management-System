using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Entities;
using RestaurantManagement.Repos;

namespace RestaurantManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(CustomerRepo repo) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = repo.GetAll();
            if (result.HasError)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
        [HttpGet("GetByID/{id}")]
        public IActionResult GetById(int id)
        {
            var result = repo.GetById(id);
            if (result.HasError)
            {
                return BadRequest(result.Message);
            }
            if (result.Data == null)
            {
                return NotFound();
            }
            return Ok(result.Data);
        }
        [HttpPost]
        public IActionResult Save(Customer model)
        {
            var result = repo.Save(model);
            if (result.HasError)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = repo.Delete(id);
            if (result.HasError)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
    }
}

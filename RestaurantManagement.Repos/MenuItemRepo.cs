using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Data;
using RestaurantManagement.Entities;
using RestaurantManagement.Shared;
namespace RestaurantManagement.Repos
{
    public class MenuItemRepo(RmDbContext context)
    {
        public RestaurantResult<List<MenuItem>> GetAll()
        {
            var result = new RestaurantResult<List<MenuItem>>();
            try
            {
                result.Data = context.MenuItems
                    .Include(m => m.Category)
                    .ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }
        public RestaurantResult<MenuItem> GetById(int id)
        {
            var result = new RestaurantResult<MenuItem>();
            try
            {
                result.Data = context.MenuItems
                    .Include(m => m.Category)
                    .FirstOrDefault(m => m.ID == id);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }
        public RestaurantResult<MenuItem> Save(MenuItem model)
        {
            var result = new RestaurantResult<MenuItem>();
            try
            {
                if (context.MenuItems.Any(m => m.Name.ToLower() == model.Name.ToLower()
                    && m.ID != model.ID))
                {
                    result.HasError = true;
                    result.Message = "MenuItem with the same name already exists.";
                    return result;
                }
                var objToSave = context.MenuItems.Find(model.ID);
                if (objToSave == null)
                {
                    objToSave = new MenuItem();
                    context.MenuItems.Add(objToSave);
                    objToSave.Created_at = DateTime.Now;
                }
                objToSave.Categories_ID = model.Categories_ID;
                objToSave.Name = model.Name;
                objToSave.Price = model.Price;
                objToSave.Description = model.Description;
                objToSave.Active = model.Active;
                objToSave.Quantity = model.Quantity;
                objToSave.Image_URL = model.Image_URL;
                objToSave.Updated_at = DateTime.Now;
                objToSave.Updated_by = 1;
                context.SaveChanges();
                result.Data = objToSave;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }
        public RestaurantResult<MenuItem> Delete(int id)
        {
            var result = new RestaurantResult<MenuItem>();
            try
            {
                var obj = context.MenuItems.Find(id);
                if (obj == null)
                {
                    result.HasError = true;
                    result.Message = "MenuItem not found";
                    return result;
                }
                context.MenuItems.Remove(obj);
                context.SaveChanges();
                result.Data = obj;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }

    }
}
using RestaurantManagement.Data;
using RestaurantManagement.Entities;
using RestaurantManagement.Shared;

namespace RestaurantManagement.Repos
{
    public class CategoriesRepo(RmDbContext context)
    {
        public RestaurantResult<List<Categories>> GetAll()
        {
            var result = new RestaurantResult<List<Categories>>();

            try
            {
                result.Data = context.Categories.ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }

        public RestaurantResult<Categories> GetById(int id)
        {
            var result = new RestaurantResult<Categories>();

            try
            {
                result.Data = context.Categories.Find(id);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }

        public RestaurantResult<Categories> Save(Categories model)
        {
            var result = new RestaurantResult<Categories>();

            try
            {
                if (context.Categories.Any(c => c.Name.ToLower() == model.Name.ToLower()
                    && c.ID != model.ID))
                {
                    result.HasError = true;
                    result.Message = "Categories with same address already exists.";
                    return result;
                }
                var objToSave = context.Categories.Find(model.ID);

                if (objToSave == null)
                {
                    objToSave = new Categories();

                    context.Categories.Add(objToSave);

                    objToSave.Created_at = DateTime.Now;
                }

                objToSave.Name = model.Name;
                objToSave.Description = model.Description;
                objToSave.Active = model.Active;
                objToSave.Updated_at = DateTime.Now;
                objToSave.Updated_by= 1;

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

        public RestaurantResult<Categories> Delete(int id)
        {
            var result = new RestaurantResult<Categories>();

            try
            {
                var obj = context.Categories.Find(id);

                if (obj == null)
                {
                    result.HasError = true;
                    result.Message = "Categories not found";

                    return result;
                }

                context.Categories.Remove(obj);

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

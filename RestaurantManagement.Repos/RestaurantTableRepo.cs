using RestaurantManagement.Data;
using RestaurantManagement.Entities;
using RestaurantManagement.Shared;

namespace RestaurantManagement.Repos
{
    public class RestaurantTableRepo(RmDbContext context)
    {
        public RestaurantResult<List<RestaurantTable>> GetAll()
        {
            var result = new RestaurantResult<List<RestaurantTable>>();
            try
            {
                result.Data = context.RestaurantTables.ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }

        public RestaurantResult<RestaurantTable> GetById(int id)
        {
            var result = new RestaurantResult<RestaurantTable>();
            try
            {
                result.Data = context.RestaurantTables.Find(id);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }

        public RestaurantResult<RestaurantTable> Save(RestaurantTable model)
        {
            var result = new RestaurantResult<RestaurantTable>();
            try
            {
                if (context.RestaurantTables.Any(c =>
                    c.Table_number == model.Table_number &&
                    c.ID != model.ID))
                {
                    result.HasError = true;
                    result.Message = "Table already exists.";
                    return result;
                }

                var objToSave = context.RestaurantTables.Find(model.ID);

                if (objToSave == null)
                {
                    objToSave = new RestaurantTable();
                    context.RestaurantTables.Add(objToSave);
                    objToSave.Created_at = DateTime.Now;
                }

                objToSave.Table_number = model.Table_number;
                objToSave.Capacity = model.Capacity;
                objToSave.Status = model.Status;
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

        public RestaurantResult<RestaurantTable> Delete(int id)
        {
            var result = new RestaurantResult<RestaurantTable>();
            try
            {
                var obj = context.RestaurantTables.Find(id);

                if (obj == null)
                {
                    result.HasError = true;
                    result.Message = "Table not found";
                    return result;
                }

                context.RestaurantTables.Remove(obj);
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

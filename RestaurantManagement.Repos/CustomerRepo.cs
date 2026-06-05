using RestaurantManagement.Data;
using RestaurantManagement.Entities;
using RestaurantManagement.Shared;

namespace RestaurantManagement.Repos
{
    public class CustomerRepo(RmDbContext context)
    {
        public RestaurantResult<List<Customer>> GetAll()
        {
            var result = new RestaurantResult<List<Customer>>();

            try
            {
                result.Data = context.Customers.ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }

        public RestaurantResult<Customer> GetById(int id)
        {
            var result = new RestaurantResult<Customer>();

            try
            {
                result.Data = context.Customers.Find(id);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }

        public RestaurantResult<Customer> Save(Customer model)
        {
            var result = new RestaurantResult<Customer>();

            try
            {
                if (context.Customers.Any(c => c.Address.ToLower() == model.Address.ToLower()
                    && c.Id != model.Id))
                {
                    result.HasError = true;
                    result.Message = "Customer with same address already exists.";
                    return result;
                }
                var objToSave = context.Customers.Find(model.Id);

                if (objToSave == null)
                {
                    objToSave = new Customer();

                    context.Customers.Add(objToSave);

                    objToSave.CreatedAt = DateTime.Now;
                }

                objToSave.Address = model.Address;
                objToSave.TotalOrders = model.TotalOrders;
                objToSave.UpdatedAt = DateTime.Now;

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

        public RestaurantResult<Customer> Delete(int id)
        {
            var result = new RestaurantResult<Customer>();

            try
            {
                var obj = context.Customers.Find(id);

                if (obj == null)
                {
                    result.HasError = true;
                    result.Message = "Customer not found";

                    return result;
                }

                context.Customers.Remove(obj);

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

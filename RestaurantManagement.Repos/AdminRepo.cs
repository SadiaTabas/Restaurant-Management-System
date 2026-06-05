using RestaurantManagement.Data;
using RestaurantManagement.Entities;
using RestaurantManagement.Shared;

namespace RestaurantManagement.Repos
{
    public class AdminRepo(RmDbContext context)
    {
        public RestaurantResult<List<Admin>> GetAll()
        {
            var result = new RestaurantResult<List<Admin>>();

            try
            {
                result.Data = context.Admins.ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }

        public RestaurantResult<Admin> GetById(int id)
        {
            var result = new RestaurantResult<Admin>();

            try
            {
                result.Data = context.Admins.Find(id);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }

        public RestaurantResult<Admin> Save(Admin model)
        {
            var result = new RestaurantResult<Admin>();

            try
            {
                var obj = context.Admins.Find(model.ID);

                if (obj == null)
                {
                    obj = new Admin();
                    context.Admins.Add(obj);
                }

                obj.Joined_At = model.Joined_At;
                obj.Employees_Assigned = model.Employees_Assigned;
                obj.NID_Number = model.NID_Number;
                obj.Qualifications = model.Qualifications;

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

        public RestaurantResult<Admin> Delete(int id)
        {
            var result = new RestaurantResult<Admin>();

            try
            {
                var obj = context.Admins.Find(id);

                if (obj == null)
                {
                    result.HasError = true;
                    result.Message = "Admin not found";
                    return result;
                }

                context.Admins.Remove(obj);
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
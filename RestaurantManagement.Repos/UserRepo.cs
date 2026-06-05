using RestaurantManagement.Data;
using RestaurantManagement.Entities;
using RestaurantManagement.Shared;

namespace RestaurantManagement.Repos
{
    public class UserRepo(RmDbContext context)
    {
        public RestaurantResult<User> Register(User model)
        {
            var result = new RestaurantResult<User>();

            try
            {
                if (context.Users.Any(u =>
                    u.Email.ToLower() == model.Email.ToLower()))
                {
                    result.HasError = true;
                    result.Message = "Email already exists";
                    return result;
                }

                if (context.Users.Any(u =>
                    u.Username.ToLower() == model.Username.ToLower()))
                {
                    result.HasError = true;
                    result.Message = "Username already exists";
                    return result;
                }

               
                model.Role = string.IsNullOrWhiteSpace(model.Role) ? "Customer" : model.Role;
                model.Gender = string.IsNullOrWhiteSpace(model.Gender) ? "Not Specified" : model.Gender;
                model.Active = true;
                model.Created_at = DateTime.Now;
                model.Updated_at = DateTime.Now;
                model.Updated_by = 0;

                context.Users.Add(model);
                context.SaveChanges();

                result.Data = model;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                
                result.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return result;
        }

        public RestaurantResult<User> Login(
            string email,
            string password)
        {
            var result = new RestaurantResult<User>();

            try
            {
                var user = context.Users.FirstOrDefault(u =>
                    u.Email == email &&
                    u.Password == password &&
                    u.Active == true);

                if (user == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Email or Password";
                    return result;
                }

                result.Data = user;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return result;
        }
    }
}
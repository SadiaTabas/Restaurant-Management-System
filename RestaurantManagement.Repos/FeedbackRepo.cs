using RestaurantManagement.Data;
using RestaurantManagement.Entities;
using RestaurantManagement.Shared;

namespace RestaurantManagement.Repos
{
    public class FeedbackRepo
    {
        private readonly RmDbContext context;

        public FeedbackRepo(RmDbContext context)
        {
            this.context = context;
        }

        
        public RestaurantResult<Feedback> Add(Feedback model)
        {
            var result = new RestaurantResult<Feedback>();

            try
            {
                model.Created_at = DateTime.Now;

                context.Feedbacks.Add(model);
                context.SaveChanges();

                result.Data = model;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

         
        public RestaurantResult<List<Feedback>> GetByCustomer(int customerId)
        {
            var result = new RestaurantResult<List<Feedback>>();

            try
            {
                result.Data = context.Feedbacks
                    .Where(x => x.Customer_ID == customerId)
                    .OrderByDescending(x => x.ID)
                    .ToList();
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
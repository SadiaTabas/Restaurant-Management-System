using RestaurantManagement.Data;
using RestaurantManagement.Entities;
using RestaurantManagement.Shared;

namespace RestaurantManagement.Repos
{
    public class OrderDetailRepo
    {
        private readonly RmDbContext context;

        public OrderDetailRepo(RmDbContext context)
        {
            this.context = context;
        }

       
        public RestaurantResult<List<OrderDetail>> GetByOrderId(int orderId)
        {
            var result = new RestaurantResult<List<OrderDetail>>();

            try
            {
                result.Data = context.OrderDetails
                    .Where(x => x.Order_ID == orderId)
                    .ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }

       
        public RestaurantResult<OrderDetail> Add(OrderDetail model)
        {
            var result = new RestaurantResult<OrderDetail>();

            try
            {
                context.OrderDetails.Add(model);
                context.SaveChanges();

                result.Data = model;
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
using RestaurantManagement.Data;
using RestaurantManagement.Entities;
using RestaurantManagement.Shared;

namespace RestaurantManagement.Repos
{
    public class TableOrderRepo
    {
        private readonly RmDbContext context;

        public TableOrderRepo(RmDbContext context)
        {
            this.context = context;
        }

      
        public RestaurantResult<TableOrder> PlaceOrder(TableOrder model)
        {
            var result = new RestaurantResult<TableOrder>();

            try
            {
               
                var tableExists = context.RestaurantTables.Any(t => t.ID == model.Table_ID);

                if (!tableExists)
                {
                    result.HasError = true;
                    result.Message = "Invalid Table Selected.";
                    return result;
                }

                model.Status = "Pending";
                model.Created_at = DateTime.Now;
                model.Updated_at = DateTime.Now;

                model.Waiter_ID = null;  
                model.Update_by = model.Customer_ID;

                context.TableOrders.Add(model);
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

        
        public RestaurantResult<List<TableOrder>> GetByCustomer(int customerId)
        {
            var result = new RestaurantResult<List<TableOrder>>();

            try
            {
                result.Data = context.TableOrders
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

        
        public RestaurantResult<TableOrder> GetById(int id)
        {
            var result = new RestaurantResult<TableOrder>();

            try
            {
                result.Data = context.TableOrders
                    .FirstOrDefault(x => x.ID == id);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }

        
        public List<OrderDetail> GetOrderItems(int orderId)
        {
            return context.OrderDetails
                .Where(x => x.Order_ID == orderId)
                .ToList();
        }

      
        public RestaurantResult<TableOrder> Checkout(TableOrder model, List<CartItem> cart)
        {
            var result = new RestaurantResult<TableOrder>();

            try
            {
                 
                model.Status = "Pending";
                model.Created_at = DateTime.Now;
                model.Updated_at = DateTime.Now;

                model.Waiter_ID = 0;
                model.Update_by = model.Customer_ID;

                context.TableOrders.Add(model);
                context.SaveChanges();
 
                foreach (var item in cart)
                {
                    var detail = new OrderDetail
                    {
                        Order_ID = model.ID,
                        MenuItem_ID = item.MenuItemId,
                        Item_Name = item.Name,
                        Price = item.Price,
                        Quantity = item.Quantity
                    };

                    context.OrderDetails.Add(detail);
                }

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
        
    }
}
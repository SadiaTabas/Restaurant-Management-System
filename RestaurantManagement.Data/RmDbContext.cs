using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Entities;

namespace RestaurantManagement.Data
{
    public class RmDbContext(DbContextOptions<RmDbContext> options)
        : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<RestaurantTable> RestaurantTables { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Admin> Admins
        {
            get; set;
        }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderDetail> OrderDetails{ get; set; }
        public DbSet<TableOrder> TableOrders { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Entities
{
    [Table("Order_Details")]
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; }

        public int Order_ID { get; set; }

        public int MenuItem_ID { get; set; }

        public string Item_Name { get; set; } = null!;

        public int Price { get; set; }

        public int Quantity { get; set; }

        public int Total => Price * Quantity;
    }
}
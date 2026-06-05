using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Entities
{
    [Table("CartItem")]
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuItemId { get; set; }
        [StringLength(250)]
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Quantity { get; set; }

        public int Total => Price * Quantity;
    }
}
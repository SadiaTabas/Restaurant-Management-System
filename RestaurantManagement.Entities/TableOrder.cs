using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Entities
{
    [Table("Table_Order")]
    public class TableOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int Table_ID { get; set; }

        [Required]
        public int Customer_ID { get; set; }

        public int? Waiter_ID { get; set; }    


        [Required]
        [StringLength(50)]
        public string Status { get; set; } = null!;

        [Required]
        public int Total_Amount { get; set; }

        [Required]
        public DateTime Created_at { get; set; }

        [Required]
        public DateTime Updated_at { get; set; }

        public int? Update_by { get; set; }    
    }
}
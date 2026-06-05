using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Entities
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID { get; set; }

         
        public int Customer_ID { get; set; }
        public int Table_ID { get; set; }
        [Display(Name = "Reservation Time")]
        public DateTime Reservation_time{ get; set; }
        [Display(Name = "Number of People")]
        public int Number_Of_people { get; set; }

        [Required]
        [StringLength(50)]
        public string? Status { get; set; }

        

        public DateTime Created_at { get; set; }

        public DateTime Updated_at { get; set; }

        public int Updated_by { get; set; }

         
        [ForeignKey("Customer_ID ")]
        public virtual Customer? Customer { get; set; }

        [ForeignKey("Table_ID")]
        public virtual RestaurantTable? RestaurantTable { get; set; }


    }
}
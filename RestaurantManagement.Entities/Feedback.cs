using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Entities
{
    [Table("Feedback")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Ratings { get; set; }

        [Required]
        [StringLength(500)]
        public string Comments { get; set; } = string.Empty;

        [Required]
        public DateTime Created_at { get; set; }

        [Required]
        public int Customer_ID { get; set; }
    }
}
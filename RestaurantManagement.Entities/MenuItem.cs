using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Entities
{
    [Table("MenuItems")]
    public class MenuItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int Categories_ID { get; set; }

        [ForeignKey("Categories_ID")]
        public virtual Categories? Category { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; } = null!;

        [Required]
        public int Price { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        public bool Active { get; set; } = true;

        [Required]
        public int Quantity { get; set; }


        [Display(Name = "Picture")]
        [StringLength(1000)]
        public string? Image_URL { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime Updated_at { get; set; }

        public int Updated_by { get; set; }
    }
}
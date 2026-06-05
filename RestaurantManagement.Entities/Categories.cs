using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Entities
{
    [Table("Categories")]
    public class Categories
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name{ get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;
        public bool Active { get; set; } = true;

        public DateTime Created_at { get; set; }

        public DateTime Updated_at { get; set; }
        public int Updated_by { get; set; }
    }
}

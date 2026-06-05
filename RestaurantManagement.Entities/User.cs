using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "Customer";

         
        public int? Phone { get; set; }

        public int? Age { get; set; }

        [StringLength(50)]
        public string? Gender { get; set; }

        public bool Active { get; set; } = true;

        public DateTime Created_at { get; set; } = DateTime.Now;

        public DateTime? Updated_at { get; set; }

        public int? Updated_by { get; set; }
    }
}
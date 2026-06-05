using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Entities
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime Joined_At { get; set; }

        public int Employees_Assigned { get; set; }

        public int NID_Number { get; set; }

        [StringLength(500)]
        public string Qualifications { get; set; } = null!;
    }
}
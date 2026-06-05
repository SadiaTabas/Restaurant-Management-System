using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Entities
{
    [Table("Restaurant_Table")]
    public class RestaurantTable
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name="Table No.")]
        public int Table_number { get; set; }
        public int Capacity { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = null!;

        public DateTime Created_at { get; set; }

        public DateTime Updated_at { get; set; }
        public int Updated_by { get; set; }
    }
}

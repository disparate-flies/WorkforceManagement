using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkforceManagement.Models
{
    public class Computer
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string Make { get; set; }

        //[Required]
        [DataType(DataType.Date)]
        [Display(Name = "Decommission Date")]
        public DateTime DecommissionDate { get; set; }

        //[Required]
        public string Condition { get; set; }
    }
}

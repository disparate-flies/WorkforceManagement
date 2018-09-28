using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkforceManagement.Models
{
    public class Employees
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Is Supervisor")]
        public bool IsSupervisor { get; set; }

        [Required]
        [Display(Name = "Department Id")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }


        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Currently Assigned Computer")]
        public int ComputerId { get; set; }
        public Computer Computer { get; set; }


        public List<Training> TrainingPrograms { get; set; } = new List<Training>();
    }
}

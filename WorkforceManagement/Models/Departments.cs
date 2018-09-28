using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkforceManagement.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        public string DeptName { get; set; }

        [Required]
        [Display(Name = "Department Budget")]
        public double ExpenseBudget { get; set; }

        public List<Employee> EmployeeList { get; set; } = new List<Employee>();
    }
}

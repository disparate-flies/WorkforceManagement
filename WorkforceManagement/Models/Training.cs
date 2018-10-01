using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WorkforceManagement.Models
{
    public class Training
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Program Name")]
        public string ProgName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Max Attendees")]
        public int MaxAttendees { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

    }

}
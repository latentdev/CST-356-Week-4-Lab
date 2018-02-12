using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CST_356_Week_4_Lab.Models
{
    public class ClassViewModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int CRN { get; set; }
        [Required]
        [Display(Name = "Class")]
        public string ClassName { get; set; }
        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [Required]
        public String Instructor { get; set; }

        public int UserID { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CST_356_Week_4_Lab.Models
{
    public class UserViewModel
    {
        [Key]
        public int ID { get; set; }

        [Required()]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required()]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required()]
        public string Email { get; set; }

        [Required()]
        [Display(Name = "Years In School")]
        public int YearsInSchool { get; set; }

    }
}
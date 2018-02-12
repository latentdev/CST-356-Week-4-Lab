using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CST_356_Week_4_Lab.Data.Entities
{
    public class Class
    {
        [Key]
        public int ID { get; set; }

        public int CRN { get; set; }
        public string ClassName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public String Instructor { get; set; }
        public int UserID { get; set; }
    }
}
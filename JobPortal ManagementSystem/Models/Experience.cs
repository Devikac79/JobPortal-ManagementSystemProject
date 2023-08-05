using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal_ManagementSystem.Models
{
    public class Experience
    {
        public int experienceId { get; set; }
        public int userId { get; set; }
        public string company { get; set; }
        public string position { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }

       
    }

}
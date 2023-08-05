using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal_ManagementSystem.Models
{
    public class Education
    {
        public int educationId { get; set; }
        public int userId { get; set; }
        public string degree { get; set; }
        public string institute { get; set; }
        public int yearOfPassing { get; set; }

        // You can also include navigation property to the Signup model, if needed
        // public Signup User { get; set; }
    }
}
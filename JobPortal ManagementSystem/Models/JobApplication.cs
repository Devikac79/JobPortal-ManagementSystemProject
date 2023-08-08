using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal_ManagementSystem.Models
{
    public class JobApplication
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int userId { get; set; }
        public int jobPostId { get; set; }
        public DateTime applicationDate { get; set; }

        // Add navigation properties for the related entities (if using Entity Framework).
        // public virtual User User { get; set; }
        // public virtual JobPost JobPost { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace JobPortal_ManagementSystem.Models
{
    public class JobApplication
    {
        public int Id { get; set; }
        [DisplayName("Username")]
        public string UserName { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("User id")]
        public int userId { get; set; }
        [DisplayName("Post id")]
        public int jobPostId { get; set; }
        [DisplayName("Application date")]
        public DateTime applicationDate { get; set; }
        

        // Add navigation properties for the related entities (if using Entity Framework).
        // public virtual User User { get; set; }
        // public virtual JobPost JobPost { get; set; }
    }

}
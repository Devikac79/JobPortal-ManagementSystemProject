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
        [DisplayName(" Highest Qualification")]
       
        public bool IsScheduled { get; set; }
       
    }

}
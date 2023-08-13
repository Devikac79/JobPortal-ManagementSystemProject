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
        [DisplayName(" Job Title")]
        public string title { get; set; }
        [DisplayName(" Company Name")]


        public string companyName { get; set; }
        [DisplayName("Skills")]
        public string skills { get; set; }
        [DisplayName("Resume")]
        public byte[] resume { get; set; }

        public bool IsScheduled { get; set; }
        public bool IsApplied { get; set; } // New field to track whether the user has applied

    }

}
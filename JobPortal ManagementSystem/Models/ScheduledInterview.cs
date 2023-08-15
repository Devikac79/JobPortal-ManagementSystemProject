using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace JobPortal_ManagementSystem.Models
{
    public class ScheduledInterview
    {
        public int InterviewId { get; set; }
        public int ApplicationId { get; set; } // Added ApplicationId property
        public int UserId { get; set; }
        public int JobPostId { get; set; }

        [DisplayName("Interview date")]
        public DateTime InterviewDate { get; set; }

        [DisplayName("Interview location")]
        public string Location { get; set; }
        [DisplayName("Job title")]
        public string title { get; set; }

        [DisplayName("company name")]
        public string companyName { get; set; }

      



    }
}
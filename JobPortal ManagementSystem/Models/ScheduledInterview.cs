using System;
using System.Collections.Generic;
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
        public DateTime InterviewDate { get; set; }
        public string Location { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal_ManagementSystem.Models
{
   
        // User.cs
        public class User
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }

            // Education Information (nullable, applicable during edit time)
            public string TenthPercentageOrGrade { get; set; }
            public string TwelfthPercentageOrGrade { get; set; }
            public string GraduationGradeOrPercentage { get; set; }
            public string PostGraduationGradeOrPercentage { get; set; }
        public string ResumePath { get; set; }
        public string ImagePath { get; set; }
        public byte[] image { get; set; }
        public byte[] resume { get; set; }




    }
}
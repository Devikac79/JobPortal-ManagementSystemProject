using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortalManagementSystem.Models
{

    using System;
    using System.ComponentModel.DataAnnotations;

    public class JobPost
    {

        public int Id { get; set; }

        [Required]
        public string title { get; set; }

        [StringLength(50)]
        public string location { get; set; }

        public int minSalary { get; set; }

        public int maxSalary { get; set; }

      
        [Required]
        [DataType(DataType.Date)]
        public DateTime postDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }

        
        public string description { get; set; }

        [Required]

        public string jobCategory { get; set; }

        [Required]
        public string jobNature { get; set; }
        public int categoryId { get; set; }

        public byte[] imageData { get; set; }
        public string companyName { get; set; }

    }


}
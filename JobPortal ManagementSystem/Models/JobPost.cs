using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortalManagementSystem.Models
{

    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class JobPost
    {

        public int Id { get; set; }

        [Required]
        [DisplayName("Title")]
        public string title { get; set; }

        [StringLength(50)]
        [DisplayName("Location")]
        public string location { get; set; }
        [DisplayName("Minimum salary")]
        public int minSalary { get; set; }
        [DisplayName("Maximum salary")]
        public int maxSalary { get; set; }
     

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Post date")]
        public DateTime postDate { get; set; }
      
        [DataType(DataType.Date)]
        [DisplayName("End date")]
        public DateTime endDate { get; set; }


        [DisplayName("Description")]
        public string description { get; set; }
  
        [Required]
        [DisplayName("Job category")]
        public string jobCategory { get; set; }

        [Required]
        [DisplayName("Job Nature")]
        public string jobNature { get; set; }
       
        public int categoryId { get; set; }
        [DisplayName("Poster upload")]
        public byte[] imageData { get; set; }
        [DisplayName("Company name")]
        public string companyName { get; set; }

    }


}
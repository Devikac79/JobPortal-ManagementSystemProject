using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobPortalManagementSystem.Models
{
    public class JobCategory
    {

      
            public int Id { get; set; }

            [Required]
            [StringLength(50)]
            public string title { get; set; }

            [StringLength(50)]
            public string location { get; set; }

            public int minSalary { get; set; }

            public int maxSalary { get; set; }

            [Required]
        public SelectList categories { get; set; }
        public int SelectedCategoryId { get; set; }
        public SelectList nature { get; set; }
        public int SelectedNatureId { get; set; }

        //  [Required]
        //  public int JobNatureId { get; set; }

        [Required]
            [DataType(DataType.Date)]
            public DateTime postDate { get; set; }

            [DataType(DataType.Date)]
            public DateTime endDate { get; set; }

          
            public string description { get; set; }
        

    }
}
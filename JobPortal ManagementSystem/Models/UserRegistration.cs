using JobPortalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal_ManagementSystem.Models
{
    public class UserRegistration
    {

        public int UserId { get; set; }
      
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }

      


    }
}
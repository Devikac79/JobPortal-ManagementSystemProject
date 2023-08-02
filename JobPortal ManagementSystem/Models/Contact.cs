using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortalManagementSystem.Models
{
    public class Contact
    {
        public int contactId { get; set; }

        public string name { get; set; }

       

        public string email { get; set; }

        public string subject { get; set; }

        public string message { get; set; }
    }
}
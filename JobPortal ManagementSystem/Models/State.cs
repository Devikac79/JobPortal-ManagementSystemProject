using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortalManagementSystem.Models
{
    public class State
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
    }
}
/*using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace JobPortalManagementSystem.Models
{
    public class Signup
    {
        internal int id;

        public int Id { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Please enter your first name.")]
        public string firstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Please enter your last name.")]
        public string lastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter your date of birth.")]
        public DateTime dateOfBirth { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please enter your gender.")]
        public String gender { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Required(ErrorMessage = "Please enter your email address.")]
        public string email { get; set; }

        [Display(Name = "Phone")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [Required(ErrorMessage = "Please enter your phone number.")]
        public string phone { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please enter your address.")]
        public string address { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "Please enter your state.")]
        public string state { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please enter your city.")]
        public string city { get; set; }

        [Display(Name = "Pincode")]
        [Required(ErrorMessage = "Please enter your pincode.")]
        public int pincode { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Please enter your country.")]
        public string country { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter your username.")]
        public string username { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your password.")]
        public string password { get; set; }

        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please confirm your password.")]
        public string confirmPassword { get; set; }
    }

}
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPortalManagementSystem.Models
{
    public class Signup
    {
        public int Id { get; set; }
        [DisplayName("First name")]
        public string firstName { get; set; }
        [DisplayName("Last name")]
        public string lastName { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter your date of birth.")]
        public DateTime dateOfBirth { get; set; }
        [DisplayName("Gender")]
        [Required(ErrorMessage = "Gender is required")]
        public string gender { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DisplayName("Email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [DisplayName("Phone")]
        public string phone { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [DisplayName("Address")]
        public string address { get; set; }
        [Required(ErrorMessage = "Select the city")]
        [DisplayName("City")]
        public string city { get; set; }
        [Required(ErrorMessage = "Select the state")]
        [DisplayName("State")]
        public string state { get; set; }
        [Required(ErrorMessage = "Pincode is required")]
        [DisplayName("Pincode")]
        public int pincode { get; set; }
        [Required(ErrorMessage = "Select the country")]
        [DisplayName("Country")]
        public string country { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [DisplayName("Username")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DisplayName("Password")]
        public string password { get; set; }
        [Required(ErrorMessage = "Re-enter the password")]
        [DisplayName("Confirm password")]
        public string confirmPassword { get; set; }
        public string Role { get; set; }



    }

}

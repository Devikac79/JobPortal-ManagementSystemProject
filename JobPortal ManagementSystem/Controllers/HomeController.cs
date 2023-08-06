using JobPortal_ManagementSystem.Models;
using JobPortal_ManagementSystem.Repository;
using JobPortalManagementSystem.Models;
using JobPortalManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace JobPortalManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Homepage()
        {

            return View();
        }
        public ActionResult HomeLayoutpage()
        {

            return View();
        }


        /// <summary>
        /// Control view record page
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSignupDetails()
        {
            SignupRepository signupRepository = new SignupRepository();
            ModelState.Clear();
            return View(signupRepository.GetSignupDetails());
        }

        private SignupRepository signupRepository = new SignupRepository();

        /*   public ActionResult AddSignupDetails()
           {
               ViewBag.Countries = signupRepository.GetCountries();
               ViewBag.States = signupRepository.GetStates();
               ViewBag.Cities = signupRepository.GetCities();

               Signup signup = new Signup();
               return View(signup);
           }


           [HttpPost]
           public ActionResult AddSignupDetails(Signup signup)
           {
               try
               {
                   if (ModelState.IsValid)
                   {
                       if (signupRepository.AddSignupDetails(signup))
                       {
                           ViewBag.Message = "User Registration Successful";
                           return RedirectToAction("Signin"); // Redirect to login page after successful registration
                       }
                   }
                   ViewBag.Countries = signupRepository.GetCountries();
                   ViewBag.States = signupRepository.GetStates();
                   ViewBag.Cities = signupRepository.GetCities();

                   return View(signup);
               }
               catch
               {
                   return View();
               }
           }*/

        // GET: /Signup/AddSignupDetails


      /*  public ActionResult AddSignupDetails()
        {
            ViewBag.Countries = signupRepository.GetCountries();
            ViewBag.States = new SelectList(new List<State>(), "stateId", "stateName"); // Empty initial list for States
            ViewBag.Cities = new SelectList(new List<City>(), "cityId", "cityName"); // Empty initial list for Cities

            return View(new Signup());
        }

        // POST: /Signup/AddSignupDetails
        [HttpPost]
        public ActionResult AddSignupDetails(Signup signup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Perform any additional validation or processing here
                    // Save the signup details to the database using the repository method
                    if (signupRepository.AddSignupDetails(signup))
                    {
                        ViewBag.Message = "User Registration Successful";
                        return RedirectToAction("Signin"); // Redirect to login page after successful registration
                    }
                    else
                    {
                        ViewBag.Message = "User Registration Failed";
                    }
                }

                // If ModelState is invalid or registration failed, reload the dropdown data
                ViewBag.Countries = signupRepository.GetCountries();
                ViewBag.States = new SelectList(new List<State>(), "stateId", "stateName"); // Empty initial list for States
                ViewBag.Cities = new SelectList(new List<City>(), "cityId", "cityName"); // Empty initial list for Cities

                return View(signup);
            }
            catch
            {
                ViewBag.Message = "An error occurred while processing your request.";
                return View(signup);
            }
        }

        // Action to fetch states based on the selected country using AJAX
        public JsonResult GetStatesByCountry(int countryId)
        {
            var states = signupRepository.GetStatesByCountry(countryId);
            return Json(states, JsonRequestBehavior.AllowGet);
        }

        // Action to fetch cities based on the selected state using AJAX
        public JsonResult GetCitiesByState(int stateId)
        {
            var cities = signupRepository.GetCitiesByState(stateId);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
      */
        /// <summary>
        /// Get method to view Creating  a record
        /// </summary>
        /// <returns></returns>
        public ActionResult AddSignupDetails()
        {
            return View();
        }
        /// <summary>
        /// Post method to assign created value to database
        /// </summary>
        /// <param name="signup"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddSignupDetails(Signup signup)
        {
            try
            {

                if (ModelState.IsValid)
                {
                   SignupRepository signupRepository = new SignupRepository();
                    if (signupRepository.AddSignupDetails(signup))
                    {
                        ViewBag.Message = "User Registration Successful";
                        return RedirectToAction("Signin"); // Redirect to login page after successful registration
                    }
                }
                return View(signup);
            }
            catch
            {
                return View();
            }
        }










        /// View the details of a selected signup record for editing.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult EditSignupDetails(int? Id)
        {
            SignupRepository signupRepository = new SignupRepository();
            return View(signupRepository.GetSignupDetails().Find(signup => signup.Id == Id));
        }

        /// <summary>
        /// Edit the signup record.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="signup"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditSignupDetails(int? Id, Signup signup)
        {
            try
            {
                SignupRepository signupRepository = new SignupRepository();
                signupRepository.EditSignupDetails(signup);
                return RedirectToAction("GetSignupDetails");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// Deleting the record
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="signup"></param>
        /// <returns></returns>
        public ActionResult DeleteSignupDetails(int Id, Signup signup)
        {
            try
            {
                SignupRepository signupRepository = new SignupRepository();
                if (signupRepository.DeleteSignupDetails(Id))
                {
                    ViewBag.AlertMessage("User details deleted successfully");
                }
                return RedirectToAction("GetDetails");
            }
            catch
            {
                return View();
            }
        }





        // GET: Login
        public ActionResult Signin()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Signin(Signin signin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SignupRepository signupRepository = new SignupRepository();
                    Signup userSignup = signupRepository.GetSignupDetailsByUsernameAndPassword(signin.username, signin.password);

                    if (userSignup != null)
                    {
                        string role = signupRepository.GetUserRole(signin.username, signin.password);

                        // Store user ID and role in session
                        Session["UserId"] = userSignup.Id;
                        Session["UserRole"] = role;

                        if (role == "user")
                        {
                            // Redirect to UserProfileDetails action in UserController
                            return RedirectToAction("UserHomepage", "User");
                        }
                        else if (role == "admin")
                        {
                            // Redirect to AdminHomepage action in AdminController
                            return RedirectToAction("AdminHomepage", "Admin");
                        }
                        else
                        {
                            ViewBag.Message = "Invalid role for the user.";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Invalid username or password";
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during authentication or redirection
                // Logging, error handling, or custom error messages can be implemented here
                ViewBag.Message = "An error occurred: " + ex.Message;
                return View();
            }
        }
        public ActionResult UserProfileDetails(int Id)
        {
            try
            {
                SignupRepository signupRepository = new SignupRepository();
                Signup userSignup = signupRepository.GetSignupById(Id);

                if (userSignup != null)
                {
                    return View(userSignup);
                }
                else
                {
                    ViewBag.Message = "User not found";
                    return View("UserNotFound");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during fetching the user's profile details
                // Logging, error handling, or custom error messages can be implemented here
                ViewBag.Message = "An error occurred: " + ex.Message;
                return View("UserNotFound");
            }
        }



        /// <summary>
        /// contact us page
        /// </summary>
        /// <returns></returns>

        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }


        public ActionResult About()
        {
            return View();
        }
        public ActionResult GetContact()
        {
            ContactRepository contactRepository = new ContactRepository();
            ModelState.Clear();
            return View(contactRepository.GetContact());
        }
        public ActionResult AddContact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddContact(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactRepository signupRepository = new ContactRepository();
                    if (signupRepository.AddContact(contact))
                    {
                        ViewBag.Message = "User Details Added Successfully";

                    }
                }
                return RedirectToAction("AddContact","Home");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult GetImage(string filePath)
        {
            // Read the image file into a byte array
            byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);

            // Determine the content type of the image based on its file extension
            string contentType = "image/jpeg"; // You may need to adjust this based on the actual file type

            // Return the image as a FileResult with the appropriate content type
            return File(imageBytes, contentType);
        }

        public ActionResult GetDetails()
        {
            UserRepository userRepository = new UserRepository();
            ModelState.Clear();
            return View(userRepository.GetDetails());


        }
        /// <summary>
        /// Get method to view Creating  a record
        /// </summary>
        /// <returns></returns>
       /*    public ActionResult AddDetails()
           {
               return View();
           }

        /*  [HttpPost]
          public ActionResult AddDetails(User user)
          {
              try
              {

                  if (ModelState.IsValid)
                  {
                     UserRepository userRepository = new UserRepository();
                      if (userRepository.AddDetails(user))
                      {
                          ViewBag.Message = "User Registration Successful";
                          return RedirectToAction("Homepage"); // Redirect to login page after successful registration
                      }
                  }
                  return View(user);
              }
              catch
              {
                  return View();
              }
          }*/

        public ActionResult AddDetails(User user, HttpPostedFileBase ResumeFile, HttpPostedFileBase ImageFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ResumeFile != null)
                    {
                        // Save the uploaded resume file to a folder on the server
                        string ResumeFileName = Path.GetFileName(ResumeFile.FileName);
                        string ResumeFilePath = Path.Combine(Server.MapPath("~/Resumes/"), ResumeFileName);
                        ResumeFile.SaveAs(ResumeFilePath);

                        user.ResumePath = ResumeFilePath;
                    }
                    if (ImageFile != null)
                    {
                        string imageFileName = Path.GetFileName(ImageFile.FileName);
                        string imageFilePath = Path.Combine(Server.MapPath("~/Images/"), imageFileName);
                        ImageFile.SaveAs(imageFilePath);

                        user.ImagePath = imageFilePath;
                    }

                    UserRepository userRepository = new UserRepository();
                    if (userRepository.AddDetails(user))
                    {
                        ViewBag.Message = "User Registration Successful";
                        return RedirectToAction("Homepage"); // Redirect to login page after successful registration
                    }
                }
                return View(user);
            }
            catch
            {
                return View();
            }
        }


        /*     public ActionResult EditDetails(int? Id)
             {
                 UserRepository userRepository = new UserRepository();
                 return View(userRepository.GetDetails().Find(user => user.Id == Id));
             }

             /// <summary>
             /// Edit the signup record.
             /// </summary>
             /// <param name="Id"></param>
             /// <param name="signup"></param>
             /// <returns></returns>
             [HttpPost]
             public ActionResult EditDetails(int? Id, User user)
             {
                 try
                 {
                     UserRepository userRepository = new UserRepository();
                     userRepository.EditDetails(user);
                     return RedirectToAction("GetDetails");
                 }
                 catch
                 {
                     return View();
                 }
             }
        */


        [HttpGet]
        public ActionResult EditDetails(int? Id)
        {
            UserRepository userRepository = new UserRepository();
            User user = userRepository.GetDetails().Find(u => u.Id == Id);

            // Make sure the ResumeFile property is null to avoid overwriting the existing resume
         //  user.ResumePath = null;

            return View(user);
        }

        [HttpPost]
        public ActionResult EditDetails(int? Id, User user, HttpPostedFileBase ResumeFile, HttpPostedFileBase ImageFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Retrieve the existing user data from the database
                    UserRepository userRepository = new UserRepository();
                    User existingUser = userRepository.GetDetails().Find(u => u.Id == Id);

                    // Update only the relevant fields (except ResumePath)
                    existingUser.FirstName = user.FirstName;
                    existingUser.LastName = user.LastName;
                    existingUser.Email = user.Email;
                    existingUser.Password = user.Password;
                    existingUser.Phone = user.Phone;
                    existingUser.Address = user.Address;
                    existingUser.TenthPercentageOrGrade = user.TenthPercentageOrGrade;
                    existingUser.TwelfthPercentageOrGrade = user.TwelfthPercentageOrGrade;
                    existingUser.GraduationGradeOrPercentage = user.GraduationGradeOrPercentage;
                    existingUser.PostGraduationGradeOrPercentage =user.PostGraduationGradeOrPercentage;
                   // existingUser.ResumePath = user.ResumePath;

                    // Check if a new resume file is uploaded, and update the ResumePath accordingly
                    if (ResumeFile != null)
                    {
                        // Save the uploaded resume file to a folder on the server
                        string ResumeFileName = Path.GetFileName(ResumeFile.FileName);
                        string ResumeFilePath = Path.Combine(Server.MapPath("~/Resumes/"), ResumeFileName);
                        ResumeFile.SaveAs(ResumeFilePath);

                        existingUser.ResumePath = ResumeFilePath;
                    }

                    // Check if a new image file is uploaded, and update the ImagePath accordingly
                    if (ImageFile != null )
                    {
                        string imageFileName = Path.GetFileName(ImageFile.FileName);
                        string imageFilePath = Path.Combine(Server.MapPath("~/Images/"), imageFileName);
                        ImageFile.SaveAs(imageFilePath);

                        existingUser.ImagePath = imageFilePath;
                    }




                    // Save the changes to the database
                    userRepository.EditDetails(existingUser);

                    ViewBag.Message = "User details updated successfully.";
                    return RedirectToAction("GetDetails");
                }

                // If ModelState is not valid, return to the Edit view with the existing user data
                return View(user);
            }
            catch
            {
                return View();
            }
        }


    }
}
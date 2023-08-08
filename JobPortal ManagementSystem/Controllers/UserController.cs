using JobPortal_ManagementSystem.Models;
using JobPortal_ManagementSystem.Repository;
using JobPortalManagementSystem.Models;
using JobPortalManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobPortalManagementSystem.Controllers
{
    public class UserController : Controller
    {


        // GET: User
        public ActionResult UserHomepage()
        {
            return View();
        }

        private readonly JobPostRepository jobPostRepository;
        private readonly SignupRepository signupRepository;
        private readonly JobApplicationRepository jobApplicationRepository;
        //  private readonly JobApplicationRepository jobApplicationRepository;
        public UserController()
        {
            jobPostRepository = new JobPostRepository();
            signupRepository = new SignupRepository();
            jobApplicationRepository = new JobApplicationRepository();
        }





        public ActionResult Signin()
        {
            return View();
        }




        // GET: UserProfileDetails
        public ActionResult UserProfileDetails()
        {
            try
            {
                // Retrieve the user ID and role from the session
                int? userId = Session["UserId"] as int?;
                string userRole = Session["UserRole"] as string;
                ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
                if (userId.HasValue && userId.Value > 0 && userRole == "user")
                {
                    SignupRepository signupRepository = new SignupRepository();
                    Signup userSignup = signupRepository.GetSignupById(userId.Value);

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
                else
                {
                    ViewBag.Message = "Access denied. Please log in with a valid user account.";
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




        // GET: EditUserProfile
        /*  public ActionResult EditUserProfile()
          {
              try
              {
                  // Retrieve the user ID from the session
                  int? userId = Session["UserId"] as int?;

                  if (userId.HasValue && userId.Value > 0)
                  {
                      SignupRepository signupRepository = new SignupRepository();
                      Signup userSignup = signupRepository.GetSignupById(userId.Value);

                      if (userSignup != null)
                      {
                          // Retrieve user's education and experience
                       //   userSignup.Educations = signupRepository.GetEducationsByUserId(userId.Value);
                        //  userSignup.Experiences = signupRepository.GetExperiencesByUserId(userId.Value);

                          return View(userSignup);
                      }
                      else
                      {
                          ViewBag.Message = "User not found";
                          return View("UserNotFound");
                      }
                  }
                  else
                  {
                      ViewBag.Message = "Access denied. Please log in with a valid user account.";
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
          }*/

        /*    public ActionResult EditUserProfile()
            {
                try
                {
                    // Retrieve the user ID from the session
                    int? userId = Session["UserId"] as int?;

                    if (userId.HasValue && userId.Value > 0)
                    {
                        SignupRepository signupRepository = new SignupRepository();
                        Signup userSignup = signupRepository.GetSignupById(userId.Value);

                        if (userSignup != null)
                        {
                            return View(userSignup);
                            //TempData["SuccessMessage"] = "User profile updated successfully";

                        }

                        else
                        {
                            ViewBag.Message = "User not found";
                            return View("UserNotFound");
                        }

                    }

                    else
                    {
                        ViewBag.Message = "Access denied. Please log in with a valid user account.";
                        return View("UserNotFound");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "An error occurred: " + ex.Message;
                    return View("UserNotFound");
                }
            }
        */
        // POST: EditUserProfile

        public ActionResult EditUserProfile()
        {
            try
            {
                // Retrieve the user ID from the session
                int? userId = Session["UserId"] as int?;

                if (userId.HasValue && userId.Value > 0)
                {
                    SignupRepository signupRepository = new SignupRepository();
                    Signup userSignup = signupRepository.GetSignupById(userId.Value);

                    if (userSignup != null)
                    {
                        return View(userSignup);
                        //TempData["SuccessMessage"] = "User profile updated successfully";

                    }

                    else
                    {
                        ViewBag.Message = "User not found";
                        return View("UserNotFound");
                    }

                }

                else
                {
                    ViewBag.Message = "Access denied. Please log in with a valid user account.";
                    return View("UserNotFound");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occurred: " + ex.Message;
                return View("UserNotFound");
            }
        }

        // POST: EditUserProfile
        [HttpPost]
        public ActionResult EditUserProfile(Signup signup)
        {
            try
            {


                if (ModelState.IsValid)
                {

                    SignupRepository signupRepository = new SignupRepository();
                    if (signupRepository.EditSignupDetails(signup))
                    {
                        // Set TempData message for successful update
                        TempData["SuccessMessage"] = "User profile updated successfully";
                       // return RedirectToAction("UserProfileDetails", "User");
                    }
                    return RedirectToAction("UserProfileDetails", "User");
                }
                return View(signup);

            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during editing the user's profile
                // Logging, error handling, or custom error messages can be implemented here
                ViewBag.Message = "An error occurred: " + ex.Message;
                return View(signup);
            }
        }


        /*   [HttpPost]
           public ActionResult EditUserProfile(Signup signup, HttpPostedFileBase profileImage, HttpPostedFileBase resumeFile)
           {
               try
               {
                   if (ModelState.IsValid)
                   {
                       byte[] profileImageBytes = null;
                       byte[] resumeFileBytes = null;

                       // Check if a profile image file was uploaded
                       if (profileImage != null && profileImage.ContentLength > 0)
                       {
                           using (var binaryReader = new BinaryReader(profileImage.InputStream))
                           {
                               profileImageBytes = binaryReader.ReadBytes(profileImage.ContentLength);
                           }
                       }

                       // Check if a resume file was uploaded
                       if (resumeFile != null && resumeFile.ContentLength > 0)
                       {
                           using (var binaryReader = new BinaryReader(resumeFile.InputStream))
                           {
                               resumeFileBytes = binaryReader.ReadBytes(resumeFile.ContentLength);
                           }
                       }


                    //   SignupRepository signupRepository = new SignupRepository();
                     //  signup.profileImage = profileImageBytes;
                     //  signup.resumeFile = resumeFileBytes;

                       SignupRepository signupRepository = new SignupRepository();
                       if (signupRepository.EditSignupDetails(signup, profileImageBytes, resumeFileBytes))
                       {
                           // Set TempData message for successful update
                           TempData["SuccessMessage"] = "User profile updated successfully";
                           return RedirectToAction("UserProfileDetails", "User");
                       }
                   }
                   return View(signup);
               }
               catch (Exception ex)
               {
                   // Handle any exceptions that occur during editing the user's profile
                   // Logging, error handling, or custom error messages can be implemented here
                   ViewBag.Message = "An error occurred: " + ex.Message;
                   return View(signup);
               }
           }
        */












        public ActionResult About()
        {
            return View();
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
                return RedirectToAction("AddContact", "Home");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult GetAllPosts()
        {
            List<JobPost> allPosts = jobPostRepository.GetAllPosts();
            return View("GetAllPosts", allPosts);
        }
        [HttpGet]
        public ActionResult Details(int Id)
        {
            // Fetch the job post details by id from the repository
            JobPost post = jobPostRepository.GetJobPostById(Id);

            // Return the view with the job post details
            return View(post);
        }




      //  private readonly JobApplicationRepository jobApplicationRepository;
        //   private readonly UserRepository userProfileRepository;

        //  private readonly JobPostRepository jobPostRepository;

        /*  public UserController(JobApplicationRepository jobApplicationRepository, JobPostRepository jobPostRepository, SignupRepository signupRepository)
          {
              this.jobApplicationRepository = jobApplicationRepository;
              this.jobPostRepository = jobPostRepository;
              this.signupRepository = signupRepository;
          }

          // Action to display the job application form
          [HttpGet]
          public ActionResult ApplyForJob(int jobId)
          {
              if (Session["UserId"] == null)
              {
                  // If the user is not logged in, redirect to the login page or show an error message.
                  return RedirectToAction("Login", "Account");
              }

              int userId = Convert.ToInt32(Session["UserId"]);
              string userName = Session["firstName"]?.ToString();
              string email = Session["email"]?.ToString();

              JobPost jobPost = jobPostRepository.GetJobPostById(jobId);
              Signup userProfile = jobApplicationRepository.GetUserProfileById(userId);


              JobApplication application = new JobApplication
              {
                  userId = userId,
                  jobPostId = jobId,
                  userName = userProfile.firstName,
                  email = userProfile.email,
                  // You can set other fields here as well.
              };

              ViewBag.JobTitle = jobPost.title; // Passing the job title to the view using ViewBag.

              return View(application);
          }

          // Action to handle the form submission
          [HttpPost]
          public ActionResult ApplyForJob(JobApplication application)
          {
              try
              {
                  if (ModelState.IsValid)
                  {
                      // Save the job application using the repository.
                      jobApplicationRepository.SaveJobApplication(application);
                      // You can redirect to a success page or show a success message here.
                      return RedirectToAction("UserHomepage");
                  }

                  // If the model is not valid, return to the view with the validation errors.
                  return View(application);
              }
              catch (SqlException ex)
              {
                  // Handle SQL exceptions (e.g., database connection issues) here.
                  // Log the exception or perform any other error handling as needed.
                  ModelState.AddModelError("", "An error occurred while saving the job application. Please try again later.");
                  return View(application);
              }
              catch (Exception ex)
              {
                  // Handle other exceptions here.
                  // Log the exception or perform any other error handling as needed.
                  ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                  return View(application);
              }
          }
        */
    

        // Action to display the job application form
        /*      [HttpGet]
              public ActionResult ApplyForJob(int jobId)
              {
                  if (Session["UserId"] == null)
                  {
                      // If the user is not logged in, redirect to the login page or show an error message.
                      return RedirectToAction("Login", "Account");
                  }

                  int userId = Convert.ToInt32(Session["UserId"]);
                  string userName = Session["firstName"]?.ToString();
                  string email = Session["email"]?.ToString();

                  JobPost jobPost = jobPostRepository.GetJobPostById(jobId);
                  Signup userProfile = jobApplicationRepository.GetUserProfileById(userId);


                  JobApplication application = new JobApplication
                  {
                      userId = userId,
                      jobPostId = jobId,
                      userName = userProfile.firstName,
                      email = userProfile.email,
                      // You can set other fields here as well.
                  };

                  ViewBag.JobTitle = jobPost.title; // Passing the job title to the view using ViewBag.

                  return View(application);
              }

              // Action to handle the form submission
              [HttpPost]
              public ActionResult ApplyForJob(JobApplication application)
              {
                  try
                  {
                      if (ModelState.IsValid)
                      {
                          // Save the job application using the repository.
                          jobApplicationRepository.SaveJobApplication(application);
                          // You can redirect to a success page or show a success message here.
                          return RedirectToAction("UserHomepage");
                      }

                      // If the model is not valid, return to the view with the validation errors.
                      return View(application);
                  }
                  catch (SqlException ex)
                  {
                      // Handle SQL exceptions (e.g., database connection issues) here.
                      // Log the exception or perform any other error handling as needed.
                      ModelState.AddModelError("", "An error occurred while saving the job application. Please try again later.");
                      return View(application);
                  }
                  catch (Exception ex)
                  {
                      // Handle other exceptions here.
                      // Log the exception or perform any other error handling as needed.
                      ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                      return View(application);
                  }
              }
        */
        [HttpGet]
        public ActionResult ApplyForJob(int jobId)
        {
            if (Session["UserId"] == null)
            {
                // If the user is not logged in, redirect to the login page or show an error message.
                return RedirectToAction("Signin", "Home");
            }

            try
            {
                int userId = Convert.ToInt32(Session["UserId"]);
                string userName = Session["firstName"]?.ToString();
                string email = Session["email"]?.ToString();

                JobPost jobPost = jobPostRepository.GetJobPostById(jobId);
                Signup userProfile = signupRepository.GetUserProfileById(userId);

                JobApplication application = new JobApplication
                {
                    userId = userProfile.Id,
                    jobPostId = jobId,
                    UserName = userProfile.firstName,
                    Email = userProfile.email,
                    // You can set other fields here as well.
                };

                ViewBag.JobTitle = jobPost.title; // Passing the job title to the view using ViewBag.

                return View(application);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., conversion errors, repository exceptions) here.
                // Log the exception or perform any other error handling as needed.
                return RedirectToAction("UserHomepage");
            }
        }

        /* [HttpPost]
         public ActionResult ApplyForJob(JobApplication application)
         {
             try
             {
                 if (ModelState.IsValid)
                 {
                     // Save the job application using the repository.
                     jobApplicationRepository.SaveJobApplication(application);
                     // You can redirect to a success page or show a success message here.
                     return RedirectToAction("UserHomepage");
                 }

                 // If the model is not valid, return to the view with the validation errors.
                 return View(application);
             }
             catch (SqlException ex)
             {
                 // Handle SQL exceptions (e.g., database connection issues) here.
                 // Log the exception or perform any other error handling as needed.
                 ModelState.AddModelError("", "An error occurred while saving the job application. Please try again later.");
                 return View(application);
             }
             catch (Exception ex)
             {
                 // Handle other exceptions here.
                 // Log the exception or perform any other error handling as needed.
                 ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                 return View(application);
             }
         }
        */






        [HttpPost]
        public ActionResult ApplyForJob(JobApplication application)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   // bool success = jobApplicationRepository.SaveJobApplication(application);
                  JobApplicationRepository jobApplicationRepository=new JobApplicationRepository();
                    if (jobApplicationRepository.SaveJobApplication(application))
                    {
                        // Successful insertion, redirect to success page.
                        return RedirectToAction("UserHomepage");
                    }
                    else
                    {
                        // Insertion failed, show an error message to the user.
                        ModelState.AddModelError("", "An error occurred while saving the job application. Please try again later.");
                        return View(application);
                    }
                }

                // If the model is not valid, return to the view with the validation errors.
                return View(application);
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., database connection issues) here.
                // Log the exception or perform any other error handling as needed.
                ModelState.AddModelError("", "An error occurred while saving the job application. Please try again later.");
                return View(application);
            }
            catch (Exception ex)
            {
                // Handle other exceptions here.
                // Log the exception or perform any other error handling as needed.
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                return View(application);
            }
        }


        public ActionResult Logout()
        {
            // Clear session data
            Session.Clear();

            // Redirect the user to the desired page after logout (e.g., homepage)
            return RedirectToAction("Homepage", "Home");
        }


























        /*    public ActionResult UserProfile()
            {
                int userId = GetLoggedInUserId(); // Get the logged-in user's ID;
                Signup user = signupRepository.GetSignupDetailsById(userId);
                return View(user);
            }

            [HttpGet]
            public ActionResult EditUserProfile()
            {
                int userId = GetLoggedInUserId(); // Get the logged-in user's ID;
                Signup user = signupRepository.GetSignupDetailsById(userId);

                return View(user);
            }
            [HttpPost]
            public ActionResult EditUserProfile(Signup user)
            {
                if (ModelState.IsValid)
                {
                    SignupRepository signupRepository = new SignupRepository();
                    bool isUpdated = signupRepository.UpdateUserProfile(user);
                    if (isUpdated)
                    {
                        return RedirectToAction("UserProfile");
                    }
                    // Handle update failure
                }
                // Handle invalid model state

                return View(user);
            }
    /*
            [HttpPost]
            public ActionResult AddEducation(Education education)
            {
                int userId = // Get the logged-in user's ID;
                SignupRepository signupRepository = new SignupRepository();
                bool isAdded = signupRepository.AddEducationDetails(userId, education);
                return Json(new { success = isAdded });
            }

            [HttpPost]
            public ActionResult AddExperience(Experience experience)
            {
                int userId = // Get the logged-in user's ID;
                SignupRepository signupRepository = new SignupRepository();
                bool isAdded = signupRepository.AddExperienceDetails(userId, experience);
                return Json(new { success = isAdded });
            }

            private int GetLoggedInUserId()
            {
                // Assuming you are using ASP.NET Identity for authentication
                var userId = User.Identity.GetUserId();

                // Convert the string userId to int and return
                return int.Parse(userId);
            }*/
    }
}




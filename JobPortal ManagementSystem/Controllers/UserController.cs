using JobPortal_ManagementSystem.Models;
using JobPortal_ManagementSystem.Repository;
using JobPortalManagementSystem.Models;
using JobPortalManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
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


        /// <summary>
        /// Get user profile details
        /// </summary>
        /// <returns></returns>

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
                ViewBag.Message = "An error occurred: " + ex.Message;
                return View("UserNotFound");
            }
        }

        /// <summary>
        /// Decrypt code
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        private string Decrypt(string cipherText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipherText;
        }
     
        /// <summary>
        /// Edit user profile get method
        /// </summary>
        /// <returns></returns>
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
                        userSignup.password = Decrypt(userSignup.password);
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
        /// <summary>
        /// post get user profile 
        /// </summary>
        /// <param name="signup"></param>
        /// <param name="imageFile"></param>
        /// <param name="resumeFile"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult EditUserProfile(Signup signup, HttpPostedFileBase imageFile, HttpPostedFileBase resumeFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(imageFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(imageFile.ContentLength);
                    }
                    signup.image = imageData;
                }
                if (resumeFile != null && resumeFile.ContentLength > 0)
                {
                    byte[] resumeData;
                    using (var binaryReader = new BinaryReader(resumeFile.InputStream))
                    {
                        resumeData = binaryReader.ReadBytes(resumeFile.ContentLength);
                    }
                    signup.resume = resumeData;
                }
                signupRepository.EditSignupDetails(signup);
                return RedirectToAction("UserProfileDetails");
            }


            return View(signup);
        }




        /// <summary>
        /// About
        /// </summary>
        /// <returns></returns>
         public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Add contact
        /// </summary>
        /// <returns></returns>
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

                    // Retrieve user's name and email from session (assuming you've stored them previously)
                 

                    // Set the name and email from session to the Contact object
                  

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

        /// <summary>
        /// Get all posts
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllPosts()
        {
            List<JobPost> allPosts = jobPostRepository.GetAllPosts();
            return View("GetAllPosts", allPosts);
        }



        /// <summary>
        /// Get details of job post
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(int Id)
        {
            // Fetch the job post details by id from the repository
            JobPost post = jobPostRepository.GetJobPostById(Id);

            // Return the view with the job post details
            return View(post);
        }


        /// <summary>
        /// Apply job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>

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

                if (jobApplicationRepository.CheckIfUserAppliedForJob(userId, jobId))
                {
                    // User has already applied for this job, show a message or redirect.
                    ViewBag.AlreadyAppliedMessage = "You have already applied for this job.";
                    return RedirectToAction("UserHomepage");
                }

                JobApplication application = new JobApplication
                {
                    userId = userProfile.Id,
                    jobPostId = jobId,
                    UserName = userProfile.firstName,
                    Email = userProfile.email,
                    companyName = jobPost.companyName,
                    title = jobPost.title
                    // You can set other fields here as well.
                };

                ViewBag.JobTitle = jobPost.title; // Passing the job title to the view using ViewBag.

                return View(application);
            }
            catch
            {
                // Handle exceptions (e.g., conversion errors, repository exceptions) here.
                // Log the exception or perform any other error handling as needed.
                return RedirectToAction("UserHomepage");
            }
        }
        /// <summary>
        /// Apply job post method
        /// </summary>
        /// <param name="application"></param>
        /// <param name="resumeFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ApplyForJob(JobApplication application, HttpPostedFileBase resumeFile)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    int userId = Convert.ToInt32(Session["UserId"]);
                    int jobId = application.jobPostId;

                    if (jobApplicationRepository.CheckIfUserAppliedForJob(userId, jobId))
                    {
                        // User has already applied for this job, show a message or redirect.
                        ViewBag.AlreadyAppliedMessage = "You have already applied for this job.";
                        return RedirectToAction("ApplyForJob");
                    }

                    if (resumeFile != null && resumeFile.ContentLength > 0)
                    {
                        byte[] resumeData;
                        using (var binaryReader = new BinaryReader(resumeFile.InputStream))
                        {
                            resumeData = binaryReader.ReadBytes(resumeFile.ContentLength);
                        }
                        application.resume = resumeData;
                    }

                    if (jobApplicationRepository.SaveJobApplication(application))
                    {
                        // Successful insertion, update IsApplied and redirect to success page.
                        jobApplicationRepository.UpdateIsApplied(userId, jobId, true);
                        TempData["SuccessMessage"] = "Your application has been submitted successfully.";
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




     /// <summary>
     /// GEt interview details
     /// </summary>
     /// <returns></returns>

        public ActionResult AllInterviews()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            if (userId > 0)
            {
                List<ScheduledInterview> userInterviews =jobApplicationRepository.GetInterviewsByUserId(userId);
                return View(userInterviews);
            }
            else
            {
                // Redirect to login or show an error message
                return RedirectToAction("Login", "User");
            }
        }
    /// <summary>
    /// Get Applied Job details
    /// </summary>
    /// <returns></returns>

        public ActionResult AppliedJobs()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Signin", "Home");
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            List<JobApplication> appliedJobs = jobApplicationRepository.GetAppliedJobsForUser(userId);

            return View(appliedJobs);
        }
    }
}




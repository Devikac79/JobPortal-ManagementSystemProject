using JobPortal_ManagementSystem.Models;
using JobPortalManagementSystem.Models;
using JobPortalManagementSystem.Repository;
using System;
using System.Collections.Generic;
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

        public UserController()
        {
            jobPostRepository = new JobPostRepository();
            signupRepository = new SignupRepository();
        }


        // Action to display the user homepage
        /* public ActionResult ViewJobPost()
         {
             var jobPosts = repository.GetJobPostDetails();
             return View(jobPosts);
         }*/
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


        public ActionResult UserProfile()
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



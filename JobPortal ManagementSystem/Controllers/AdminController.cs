
using JobPortal_ManagementSystem.Models;
using JobPortal_ManagementSystem.Repository;
using JobPortalManagementSystem.Models;
using JobPortalManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace JobPortalManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly JobPostRepository repository;
        private readonly CategoryRepository categoryRepository;
        private readonly JobApplicationRepository jobApplicationRepository;
        private readonly SignupRepository signupRepository;

        public AdminController()
        {
            repository = new JobPostRepository();
            jobApplicationRepository =new JobApplicationRepository();
            signupRepository=new SignupRepository();
        }

        // GET: Admin
        public ActionResult AdminHomepage()
        {
            return View();
        }

        public ActionResult AdminLayoutpage()
        {
            return View();
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Category> categories = repository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "categoryId", "category");
            return View();
        }

        /* [HttpPost]
         public ActionResult GetPosts(int categoryId)
         {
             List<Post> posts = repository.GetPostsByCategoryId(categoryId);
             return Json(posts);
         }
       */

        /*   [HttpPost]
           public ActionResult AddPost(JobPost jobPost)
           {
               if (ModelState.IsValid)
               {
                   repository.AddPost(jobPost);
                   return RedirectToAction("GetAllPosts");
               }

               List<Category> categories = repository.GetAllCategories();
               ViewBag.Categories = new SelectList(categories, "categoryId", "category");
               return View(jobPost);
           }
        */

        /// <summary>
        /// Add Job Post
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult AddPost()
        {
            List<Category> categories = repository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "categoryId", "category");

            return View();
        }
        /// <summary>
        /// Add post post method
        /// </summary>
        /// <param name="post"></param>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddPost(JobPost post, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {




                    // Check if the uploaded file is an image by validating the file extension
                    string fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" }; // Add more extensions as needed
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("imageFile", "Only image files (JPG, JPEG, PNG, GIF) are allowed.");
                        return View(post);
                    }

                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(imageFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(imageFile.ContentLength);
                    }
                    post.imageData = imageData;
                }

                repository.AddPost(post);
                return RedirectToAction("GetAllPosts");
            }

             List<Category> categories = repository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "categoryId", "category");
            return View(post);
        }


        /// <summary>
        /// Get all posts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAllPosts()
        {
            List<JobPost> allPosts = repository.GetAllPosts();
            Dictionary<int, string> categoryNames = GetCategoryNames(allPosts);
            ViewBag.CategoryNames = categoryNames;
            return View(allPosts);
        }



        private Dictionary<int, string> GetCategoryNames(List<JobPost> posts)
        {
            List<int> categoryIds = posts.Select(p => p.categoryId).Distinct().ToList();
            List<Category> categories = repository.GetCategoriesByIds(categoryIds);
            return categories.ToDictionary(c => c.categoryId, c => c.category);
        }


        /// <summary>
        /// Edit Job Post
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditJobPost(int Id)
        {
            JobPost post = repository.GetJobPostById(Id);
            if (post == null)
            {
                return HttpNotFound();
            }

            List<Category> categories = repository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "categoryId", "category");
            return View(post);
        }
       

        /// <summary>
        /// Edit Job Post
        /// </summary>
        /// <param name="post"></param>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditJobPost(JobPost post, HttpPostedFileBase imageFile)
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
                    post.imageData = imageData;
                }

                repository.UpdateJobPost(post);
                return RedirectToAction("GetAllPosts");
            }

            List<Category> categories = repository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "categoryId", "category");
            return View(post);
        }

        /// <summary>
        /// Get contact
        /// </summary>
        /// <returns></returns>
        public ActionResult GetContact()
        {
            ContactRepository contactRepository = new ContactRepository();
            ModelState.Clear();
            return View(contactRepository.GetContact());
        }


        
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CategoryRepository categoryRepository = new CategoryRepository();
                    if (categoryRepository.AddCategory(category))
                    {
                        ViewBag.Message = "category added Added Successfully";

                    }
                }
                return RedirectToAction("AdminHomepage", "Admin");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// GEt all categories
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllCategories()
        {
            List<Category> allCategories = repository.GetAllCategories();
            return View(allCategories);
        }

        /// <summary>
        /// Delete Job Post
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        //[HttpGet]
        //public ActionResult DeleteJobPost(int Id)
        //{
        //    // Get the job post by its ID from the repository
        //    JobPost jobPost = repository.GetJobPostById(Id);

        //    // If the job post doesn't exist, return a "Not Found" response
        //    if (jobPost == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(jobPost); // Pass the job post to the view for confirmation
        //}
        ///// <summary>
        ///// DeleteJob Post
        ///// </summary>
        ///// <param name="Id"></param>
        ///// <param name="jobPost"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult DeleteJobPost(int Id,JobPost jobPost)
        //{
        //    try
        //    {
        //        JobPostRepository jobPostRepository = new JobPostRepository();
        //        if (jobPostRepository.DeleteJobPost(Id))
        //        {
        //            ViewBag.AlertMessage = "Job post deleted successfully";
        //        }
        //        return RedirectToAction("GetAllPosts");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public ActionResult DeleteJobPost(int Id)
        {
            repository.DeleteJobPost(Id);
            return RedirectToAction("GetAllPosts");
        }

        /// <summary>
        /// Get SIgnup Details
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSignupDetails()
        {
            SignupRepository signupRepository = new SignupRepository();
            ModelState.Clear();
            return View(signupRepository.GetSignupDetails());
        }
        /// <summary>
        /// Logout 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            // Clear session data
            Session.Clear();

            // Redirect the user to the desired page after logout (e.g., homepage)
            return RedirectToAction("Homepage", "Home");
        }



        /// <summary>
        /// GEt all applied jobs
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewAppliedJobs()
        {
            var appliedJobs = jobApplicationRepository.GetAllAppliedJobs();
            return View(appliedJobs);
        }

        /// <summary>
        /// Schedule interview
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult ScheduleInterview(int Id)
        {
            JobApplication application = jobApplicationRepository.GetJobApplicationById(Id);

            if (application == null)
            {
                // Handle the case when the application is not found
                // You can redirect or show an error message
                return RedirectToAction("AdminHomepage", "Admin"); // Redirect to the home page for example
            }

            ScheduledInterview interview = new ScheduledInterview
            {
                ApplicationId = application.Id,
                UserId = application.userId,
                JobPostId = application.jobPostId,
                title = application.title,
                companyName = application.companyName

            };

            return View(interview);
        }
        /// <summary>
        /// Schedule interview
        /// </summary>
        /// <param name="interview"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult ScheduleInterview(ScheduledInterview interview)
        {
            if (ModelState.IsValid)
            {
                // Save the scheduled interview using the repository
                jobApplicationRepository.SaveScheduledInterview(interview);

                // Update the IsScheduled property for the corresponding job application
                jobApplicationRepository.UpdateIsScheduled(interview.ApplicationId, true);

                // You can redirect to a success page or show a success message here
                return RedirectToAction("AdminHomepage", "Admin"); // Redirect to home page for example
            }

            // If the model is not valid, return to the view with the validation errors
            return View(interview);
        }
        /// <summary>
        /// Reject application
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult RejectApplication(int Id)
        {
            jobApplicationRepository.RejectApplication(Id);
            return RedirectToAction("ViewAppliedJobs");
        }







        public ActionResult DeleteUser(int Id)
        {
            signupRepository.DeleteUser(Id);
            return RedirectToAction("GetSignupDetails");
        }




        public ActionResult DeleteDetails(int Id) // Signup signup
        {

            SignupRepository signupRepository = new SignupRepository();
            return View(signupRepository.GetSignupDetails().Find(signup => signup.Id == Id));
        }
        [HttpPost]
        public ActionResult DeleteDetails(int Id, Signup signup) // Signup signup
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

            //SignupRepository signupRepository = new SignupRepository();
            //return View(signupRepository.GetDetails().Find(signup => signup.Id == Id));
        }







      




       
    
    
    
    }
}



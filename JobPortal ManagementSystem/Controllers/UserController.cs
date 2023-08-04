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
        private readonly JobPostRepository repository;

        public UserController()
        {
            repository = new JobPostRepository();
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
            List<JobPost> allPosts = repository.GetAllPosts();
            return View("GetAllPosts", allPosts);
        }
        [HttpGet]
        public ActionResult Details(int Id)
        {
            // Fetch the job post details by id from the repository
            JobPost post = repository.GetJobPostById(Id);

            // Return the view with the job post details
            return View(post);
        }

    }
}

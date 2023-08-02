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
        public ActionResult ViewJobPost()
        {
            var jobPosts = repository.GetJobPostDetails();
            return View(jobPosts);
        }

    }
}

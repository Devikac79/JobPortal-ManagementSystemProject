
using JobPortalManagementSystem.Models;
using JobPortalManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace JobPortalManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly JobPostRepository repository;

        public AdminController()
        {
            repository = new JobPostRepository();
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

        public ActionResult GetJobPostDetails()
        {
             ModelState.Clear();
            return View(repository.GetJobPostDetails());
        }

     



        public ActionResult AddJobPost()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddJobPost(JobPost jobPost)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    JobPostRepository jobPostRepository = new JobPostRepository();
                    if (jobPostRepository.AddJobPost(jobPost))
                    {
                        ViewBag.Message = "job posted Successful";
                        return RedirectToAction("AdminHomepage"); // Redirect to login page after successful registration
                    }
                }
                return View(jobPost);
            }
            catch
            {
                return View();
            }

        }
    }
}


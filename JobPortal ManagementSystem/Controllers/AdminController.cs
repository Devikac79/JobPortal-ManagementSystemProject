
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

        /*  public ActionResult GetJobPostDetails()
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

          }*/


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

        [HttpGet]
        public ActionResult AddPost()
        {
            List<Category> categories = repository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "categoryId", "category");
            return View();
        }

        [HttpPost]
        public ActionResult AddPost(JobPost post, HttpPostedFileBase imageFile)
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

                repository.AddPost(post);
                return RedirectToAction("GetAllPosts");
            }

            List<Category> categories = repository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "categoryId", "category");
            return View(post);
        }
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
                        ViewBag.Message = "User Details Added Successfully";

                    }
                }
                return RedirectToAction("AdminHomepage", "Admin");
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult DeleteSignupDetails(int Id, JobPost jobPost)
        {
            try
            {
                JobPostRepository jobPostRepository = new JobPostRepository();
                if (jobPostRepository.DeleteJobPost(Id))
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
      

    }


}



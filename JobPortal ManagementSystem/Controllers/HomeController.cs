using JobPortal_ManagementSystem.Models;
using JobPortal_ManagementSystem.Repository;
using JobPortalManagementSystem.Models;
using JobPortalManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using JobPortal_ManagementSystem.Error;

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
        private readonly JobPostRepository jobPostRepository;


        private readonly SignupRepository signupRepository;
        private readonly UserRepository userRepository; 
      //  private readonly UserRepository _locationRepository = new UserRepository();
        public HomeController()
        {
            userRepository = new UserRepository();
            signupRepository = new SignupRepository();
            jobPostRepository = new JobPostRepository();
        }

        /// <summary>
        /// Control view record page
        /// </summary>
        /// <returns></returns>
       
        [HttpGet]
        public ActionResult GetSignupDetails()
        {
            List<Signup> allsignup = signupRepository.GetSignupDetails();
      
            return View(allsignup);
        }


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
        public ActionResult AddSignupDetails(Signup signup, HttpPostedFileBase imageFile, HttpPostedFileBase resumeFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    signupRepository.AddSignupDetails(signup);
                    return RedirectToAction("Homepage");
                }

                return View(signup);
            }
            catch (Exception ex)
            {
                // Log the error using the error handling mechanism
                ErrHandler.WriteError(ex.Message);

                // Optionally, you can handle the exception or return an error view
                return View("Error");
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
            catch (Exception ex)
            {
                // Log the error using the error handling mechanism
                ErrHandler.WriteError(ex.Message);

                // Optionally, you can handle the exception or return an error view
                return View("Error");
            }
        }
        /// <summary>
        /// Edit sign up details
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpGet]
        public ActionResult EditSignupDetails(int Id)
        {
            try
            {


                Signup signup = signupRepository.GetSignUpById(Id);
                if (signup == null)
                {
                    return HttpNotFound();
                }
                return View(signup);
            }
            catch (Exception ex)
            {
                // Log the error using the error handling mechanism
                ErrHandler.WriteError(ex.Message);

                // Optionally, you can handle the exception or return an error view
                return View("Error");
            }
        }

        /// <summary>
        /// Edit Sign up details post
        /// </summary>
        /// <param name="signup"></param>
        /// <param name="imageFile"></param>
        /// <param name="resumeFile"></param>
        /// <returns></returns>
        
        [HttpPost]
        public ActionResult EditSignupDetails(Signup signup, HttpPostedFileBase imageFile, HttpPostedFileBase resumeFile)
        {
            try
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
                    return RedirectToAction("GetSignupDetails");
                }


                return View(signup);
            }
            catch (Exception ex)
            {
                // Log the error using the error handling mechanism
                ErrHandler.WriteError(ex.Message);

                // Optionally, you can handle the exception or return an error view
                return View("Error");
            }
        }



        // GET: Login
        public ActionResult Signin()
        {
            return View();
        }


        /// <summary>
        /// Signin 
        /// </summary>
        /// <param name="signin"></param>
        /// <returns></returns>
           [HttpPost]
           public ActionResult Signin(Signin signin)
           {
               try
               {
                   if (ModelState.IsValid)
                   {
                       SignupRepository signupRepository = new SignupRepository();

                    string role = signupRepository.GetUserRole(signin.username, signin.password);

                    Signup userSignup = signupRepository.GetSignupDetailsByUsernameAndPassword(signin.username);

                       if (userSignup != null)
                       {
                          // string role = signupRepository.GetUserRole(signin.username, signin.password);


                        // Store user ID and role in session
                        Session["UserId"] = userSignup.Id;
                        Session["Username"] =userSignup.firstName;
                        Session["UserRole"] = role;

                        if (!string.IsNullOrEmpty(role))
                        {
                            if (role == "user")
                            {
                                return Json(new { success = true, role = "user" });
                            }
                            else if (role == "admin")
                            {
                                return Json(new { success = true, role = "admin" });
                            }
                        }

                        return Json(new { success = false }); // Invalid username or password
                    }

                    //   if (role == "user")
                    //   {

                    //    TempData["AlertMessage"] = "Login successful as user.";

                    //    // Redirect to UserProfileDetails action in UserController
                    //    return RedirectToAction("UserHomepage", "User");
                    //   }

                    //else if (role == "admin")
                    //{
                    //    return RedirectToAction("AdminHomepage", "Admin");
                    //   // ViewBag.Message = "Invalid role for the user.";
                    //   }
                    //}

                
                       else
                    {
                       return RedirectToAction("Homepage", "Admin");
                    }
                   }

                   return View();
               }
                 catch (Exception ex)
                {
                 // Log the error using the error handling mechanism
                 ErrHandler.WriteError(ex.Message);

                // Optionally, you can handle the exception or return an error view
                return View("Error");
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
                // Log the error using the error handling mechanism
                ErrHandler.WriteError(ex.Message);

                // Optionally, you can handle the exception or return an error view
                return View("Error");
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
        /// <summary>
        /// GEt contact method
        /// </summary>
        /// <returns></returns>
        public ActionResult GetContact()
        {
            try
            {
                ContactRepository contactRepository = new ContactRepository();
                ModelState.Clear();
                return View(contactRepository.GetContact());
            }
            catch (Exception ex)
            {
                // Log the error using the error handling mechanism
                ErrHandler.WriteError(ex.Message);

                // Optionally, you can handle the exception or return an error view
                return View("Error");
            }

        }
        /// <summary>
        /// Add contact
        /// </summary>
        /// <returns></returns>
        public ActionResult AddContact()
        {

            return View();
        }
        /// <summary>
        /// Add contact post
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
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
            catch (Exception ex)
            {
                // Log the error using the error handling mechanism
                ErrHandler.WriteError(ex.Message);

                // Optionally, you can handle the exception or return an error view
                return View("Error");
            }
        }




     


        /// <summary>
        /// Add details of user
        /// </summary>
        /// <returns></returns>

        public ActionResult AddDetails()
        {
            return View();
        }

        /// <summary>
        /// Add details of user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ResumeFile"></param>
        /// <param name="ImageFile"></param>
        /// <param name="Image"></param>
        /// <param name="Resume"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddDetails(User user,HttpPostedFileBase ResumeFile,HttpPostedFileBase ImageFile,HttpPostedFileBase Image,HttpPostedFileBase Resume)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ResumeFile != null)
                    {
                        string resumeFileName = Path.GetFileName(ResumeFile.FileName);
                        string resumeFilePath = Path.Combine(Server.MapPath("~/Resumes/"), resumeFileName);
                        ResumeFile.SaveAs(resumeFilePath);

                        user.ResumePath = resumeFilePath;
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
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
                // Optionally, handle the error or display a friendly message to the user
                return View();
            }
        }
       /// <summary>
       /// Edit details
       /// </summary>
       /// <param name="Id"></param>
       /// <returns></returns>

        [HttpGet]
        public ActionResult EditDetails(int? Id)
        {
            UserRepository userRepository = new UserRepository();
            User user = userRepository.GetDetails().Find(u => u.Id == Id);

            // Make sure the ResumeFile property is null to avoid overwriting the existing resume
         //  user.ResumePath = null;

            return View(user);
        }
        /// <summary>
        /// Edit user profile
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="user"></param>
        /// <param name="ResumeFile"></param>
        /// <param name="ImageFile"></param>
        /// <returns></returns>
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


        public ActionResult Error()
        {
            return View();
        }


        /// <summary>
        /// GEt all posts
        /// </summary>
        /// <returns></returns>
        //public ActionResult GetAllPosts()
        //{
        //    List<JobPost> allPosts = jobPostRepository.GetAllPosts();
        //    return View("GetAllPosts", allPosts);
        //}


        public ActionResult GetAllPosts(string search)
        {
            List<JobPost> allPosts = jobPostRepository.GetAllPosts();

            if (!string.IsNullOrEmpty(search))
            {
                allPosts = allPosts.Where(post =>
                    post.title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    post.location.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    post.jobCategory.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) 
                   
                    .ToList();
            }

            return View(allPosts);
        }
        [HttpGet]
        public ActionResult Details(int Id)
        {
            // Fetch the job post details by id from the repository
            JobPost post = jobPostRepository.GetJobPostById(Id);

            // Return the view with the job post details
            return View(post);
        }









        //upoad file
        public ActionResult Upload()
        {
            return View();
        }
     
        public ActionResult Upload(HttpPostedFileBase file, HttpPostedFileBase image)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    if (Path.GetExtension(file.FileName).ToLower() == ".pdf" ||
                        Path.GetExtension(file.FileName).ToLower() == ".jpg" ||
                        Path.GetExtension(file.FileName).ToLower() == ".jpeg" ||
                        Path.GetExtension(file.FileName).ToLower() == ".png")
                    {
                        byte[] fileData;
                        using (var binaryReader = new BinaryReader(file.InputStream))
                        {
                            fileData = binaryReader.ReadBytes(file.ContentLength);
                        }

                        byte[] imageData = null;
                        if (image != null && image.ContentLength > 0)
                        {
                            using (var binaryReader = new BinaryReader(image.InputStream))
                            {
                                imageData = binaryReader.ReadBytes(image.ContentLength);
                            }
                        }

                        var uploadedFile = new UploadedFile
                        {
                            FileName = file.FileName,
                            FileData = fileData,
                            ImageData = imageData
                        };

                         userRepository.InsertUploadedFile(uploadedFile);
                        

                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Please upload a valid PDF, JPG, JPEG, or PNG file.";
                        return View(); // Return the view to show the error message
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Please select a file to upload.";
                    return View(); // Return the view to show the error message
                }
            }
            catch (Exception ex)
            {
                // Log the error using the error handling mechanism
                ErrHandler.WriteError(ex.Message);

                // Optionally, you can handle the exception or return an error view
                return View("Error");
            }

            return RedirectToAction("Homepage", "Home"); // Redirect to the homepage action of HomeController
        }

        //dispay file

        public ActionResult DisplayFiles()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM UploadedFiles", connection))
                {
                    var model = new List<UploadedFile>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model.Add(new UploadedFile
                            {
                                Id = (int)reader["Id"],
                                FileName = (string)reader["FileName"],
                                FileData = (byte[])reader["FileData"],

                            });
                        }
                    }
                    return View(model);
                }
            }
        }


        



        public ActionResult CreateUser()
        {
            var countries = userRepository.GetCountries();
            ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName");
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(UserRegistration user)
        {
            userRepository.SaveUserRegistration(user);
            return RedirectToAction("Index", "Home"); // Redirect to a different page.
        }

        [HttpPost]
        public ActionResult GetStatesByCountry(int countryId)
        {
            var states = userRepository.GetStatesByCountryId(countryId);
            return Json(states);
        }

        [HttpPost]
        public ActionResult GetCitiesByState(int stateId)
        {
            var cities = userRepository.GetCitiesByStateId(stateId);
            return Json(cities);
        }
    }
}

/*   public ActionResult Index()
   {
       var countries = signupRepository.GetCountries();
       ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName");
       return View();
   }

   [HttpPost]
   public JsonResult GetStates(int CountryId)
   {
       var states = signupRepository.GetStatesByCountry(CountryId);
       return Json(states);
   }

   [HttpPost]
   public JsonResult GetCities(int StateId)
   {
       var cities = signupRepository.GetCitiesByState(StateId);
       return Json(cities);
   }
 */
/*    public ActionResult RegisterUser()
    {

        var countries = signupRepository.GetCountries();
        ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName");
     //   ViewBag.States = new SelectList(new List<State>(), "StateId", "StateName"); // Initialize with empty list
      //  ViewBag.Cities = new SelectList(new List<City>(), "CityId", "CityName"); // Initialize with empty list

        return View(new UserRegistration());
    }

    [HttpPost]
    public ActionResult RegisterUser(UserRegistration model)
    {
        if (ModelState.IsValid)
        {
            signupRepository.RegisterUser(model);
            // Redirect to a success page or another action
            return RedirectToAction("RegistrationSuccess");
        }

        // If the model is not valid, redisplay the registration form with validation errors
        var countries = signupRepository.GetCountries();
        ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName");

        // Load the dropdown data based on the selected country and state
      //  var states = signupRepository.GetStatesByCountry(model.CountryId);
      //  ViewBag.States = new SelectList(states, "StateId", "StateName");

       // var cities = signupRepository.GetCitiesByState(model.StateId);
       // ViewBag.Cities = new SelectList(cities, "CityId", "CityName");

        return View(model);
    }

   public JsonResult GetStatesByCountry(int CountryId)
    {
        var states = signupRepository.GetStatesByCountry(CountryId);
        return Json(states);
    }

    [HttpPost]
    public JsonResult GetCitiesByState(int StateId)
    {
        var cities = signupRepository.GetCitiesByState(StateId);
        return Json(cities);
    }*/
/*  public ActionResult RegisterUser()
  {
      var model = new UserRegistration
      {
          Countries = signupRepository.GetCountries(),
          States = new List<State>(),
          Cities = new List<City>()
      };

      return View(model);
  }

  [HttpPost]
  public ActionResult RegisterUser(UserRegistration model)
  {
      if (ModelState.IsValid)
      {
          signupRepository.RegisterUser(model);
          // Redirect to a success page or another action
          return RedirectToAction("RegistrationSuccess");
      }

      // If the model is not valid, redisplay the registration form with validation errors
      model.Countries = signupRepository.GetCountries();
      model.States = signupRepository.GetStatesByCountry(model.CountryId);
      model.Cities = signupRepository.GetCitiesByState(model.StateId);

      return View(model);
  }

  public JsonResult GetStatesByCountry(int CountryId)
  {
      var states = signupRepository.GetStatesByCountry(CountryId);
      return Json(states, JsonRequestBehavior.AllowGet);
  }

  [HttpPost]
  public JsonResult GetCitiesByState(int StateId)
  {
      var cities = signupRepository.GetCitiesByState(StateId);
      return Json(cities);
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


  }*/

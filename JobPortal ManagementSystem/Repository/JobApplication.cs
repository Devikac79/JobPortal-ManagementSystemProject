using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using System.Configuration;
using JobPortal_ManagementSystem.Models;
using JobPortalManagementSystem.Models;
using System.Data;

namespace JobPortal_ManagementSystem.Repository
{
    public class JobApplicationRepository
    {
       // private readonly string connectionString;

        /*  public JobApplicationRepository()
          {
              // Replace "YourConnectionString" with the actual connection string from your configuration.
              string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();
          }
          public Signup GetUserProfileById(int userId)
          {
              Signup userProfile = null;

              using (var connection = new SqlConnection(connectionString))
              {
                  connection.Open();
                  using (var command = new SqlCommand("SELECT firstName, email FROM Table_Signup WHERE Id = @userId", connection))
                  {
                      command.Parameters.AddWithValue("@userId", userId);

                      using (var reader = command.ExecuteReader())
                      {
                          if (reader.Read())
                          {
                              userProfile = new Signup
                              {
                                  Id = userId,
                                  firstName = reader["firstName"].ToString(),
                                  email = reader["email"].ToString()
                              };
                          }
                      }
                  }
              }

              return userProfile;
          }

          public void SaveJobApplication(JobApplication application)
          {
              using (var connection = new SqlConnection(connectionString))
              {
                  connection.Open();
                  using (var command = new SqlCommand("INSERT INTO Table_JobApplications (UserName, Email, userId, jobPostId, applicationDate) VALUES (@userName, @email, @userId, @jobPostId, @applicationDate)", connection))
                  {
                      command.Parameters.AddWithValue("@userName", application.userName);
                      command.Parameters.AddWithValue("@email", application.email);
                      command.Parameters.AddWithValue("@userId", application.userId);
                      command.Parameters.AddWithValue("@jobPostId", application.jobPostId);
                      command.Parameters.AddWithValue("@applicationDate", DateTime.Now);

                      command.ExecuteNonQuery();
                  }
              }
          }*/
        public Signup GetUserProfileById(int userId)
        {
            Signup userProfile = null;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT firstName, email FROM Table_Signup WHERE Id = @userId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userProfile = new Signup
                            {
                                Id = userId,
                                firstName = reader["firstName"].ToString(),
                                email = reader["email"].ToString()
                            };
                        }
                    }
                }
            }

            return userProfile;
        }



        /*   public Signup GetUserProfileById(int userId)
           {
               try
               {
                   using (var connection = new SqlConnection(connectionString))
                   {
                       connection.Open();

                       using (var command = new SqlCommand("SELECT firstName, email FROM Table_Signup WHERE Id = @userId", connection))
                       {
                           command.Parameters.AddWithValue("@userId", userId);

                           using (var reader = command.ExecuteReader())
                           {
                               if (reader.Read())
                               {
                                   return new Signup
                                   {
                                       Id = userId,
                                       firstName = reader["firstName"].ToString(),
                                       email = reader["email"].ToString()
                                   };
                               }
                           }
                       }
                   }

                   return null; // Return null if no user profile is found for the given userId.
               }
               catch (SqlException ex)
               {
                   // Handle SQL exceptions (e.g., database connection issues) here.
                   // Log the exception or perform any other error handling as needed.
                   throw new Exception("An error occurred while retrieving user profile.", ex);
               }
               catch (Exception ex)
               {
                   // Handle other exceptions here.
                   // Log the exception or perform any other error handling as needed.
                   throw new Exception("An unexpected error occurred while retrieving user profile.", ex);
               }
           }
        */
        string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();
        public bool SaveJobApplication(JobApplication application)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("INSERT INTO Table_JobApplications (UserName, Email, userId, jobPostId, applicationDate) VALUES (@UserName, @Email, @userId, @jobPostId, @applicationDate)", connection))
                    {
                        command.Parameters.AddWithValue("@UserName", application.UserName);
                        command.Parameters.AddWithValue("@Email", application.Email);
                        command.Parameters.AddWithValue("@userId", application.userId);
                        command.Parameters.AddWithValue("@jobPostId", application.jobPostId);
                        command.Parameters.AddWithValue("@applicationDate", DateTime.Now);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or perform other error handling.
                return false;
            }
        }

        public List<JobApplication> GetAllAppliedJobs()
        {
            List<JobApplication> appliedJobs = new List<JobApplication>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT UserName, Email, jobPostId, applicationDate FROM Table_JobApplications", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JobApplication application = new JobApplication
                            {
                                UserName = reader["UserName"].ToString(),
                                Email = reader["Email"].ToString(),
                                jobPostId = Convert.ToInt32(reader["jobPostId"]),
                                applicationDate = Convert.ToDateTime(reader["applicationDate"])
                            };
                            appliedJobs.Add(application);
                        }
                    }
                }
            }

            return appliedJobs;
        }
    }
}




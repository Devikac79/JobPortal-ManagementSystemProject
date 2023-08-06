using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using System.Configuration;
using JobPortal_ManagementSystem.Models;
using JobPortalManagementSystem.Models;

namespace JobPortal_ManagementSystem.Repository
{
    public class JobApplicationRepository
    {
        private readonly string connectionString;

        public JobApplicationRepository()
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
        }
    }
}

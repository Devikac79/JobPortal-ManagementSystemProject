using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using System.Configuration;
using JobPortal_ManagementSystem.Models;
using JobPortalManagementSystem.Models;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace JobPortal_ManagementSystem.Repository
{
    public class JobApplicationRepository
    {
        /// <summary>
        /// Get user profile details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Signup GetUserProfileById(int userId)
        {
            Signup userProfile = null;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SPS_UserProfileById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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



        string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();


        /// <summary>
        /// Save Job Apllication 
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public bool SaveJobApplication(JobApplication application)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SPI_JobApplication", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserName", application.UserName);
                        command.Parameters.AddWithValue("@Email", application.Email);
                        command.Parameters.AddWithValue("@userId", application.userId);
                        command.Parameters.AddWithValue("@jobPostId", application.jobPostId);
                        command.Parameters.AddWithValue("@title", application.title);
                        command.Parameters.AddWithValue("@companyName", application.companyName);
                        command.Parameters.AddWithValue("@skills", application.skills);
                        command.Parameters.Add("@resume", SqlDbType.VarBinary, -1).Value = application.resume;
                      
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

        /// <summary>
        /// Checking if user applied for job
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public bool CheckIfUserAppliedForJob(int userId, int jobId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SP_CheckUserJobApplication", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@JobPostId", jobId);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }



        /// <summary>
        /// Update the applied field
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="jobId"></param>
        /// <param name="isApplied"></param>
        /// <returns></returns>
        public bool UpdateIsApplied(int userId, int jobId, bool isApplied)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SP_UpdateJobApplicationStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@JobId", jobId);
                        command.Parameters.AddWithValue("@IsApplied", isApplied);

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

        /// <summary>
        /// To get all aapplied jobs
        /// </summary>
        /// <returns></returns>

        public List<JobApplication> GetAllAppliedJobs()
        {
            List<JobApplication> appliedJobs = new List<JobApplication>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SPS_AllAppliedJobs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JobApplication application = new JobApplication
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                UserName = reader["UserName"].ToString(),
                                Email = reader["Email"].ToString(),
                                jobPostId = Convert.ToInt32(reader["jobPostId"]),
                                applicationDate = Convert.ToDateTime(reader["applicationDate"]),
                                IsScheduled = Convert.ToBoolean(reader["IsScheduled"]),
                                title = reader["title"].ToString(),
                                companyName = reader["companyName"].ToString(),
                                skills = reader["skills"].ToString(),
                                resume = reader["resume"] as byte[]
                            };
                            appliedJobs.Add(application);
                        }
                    }
                }
            }

            return appliedJobs;
        }



        /// <summary>
        /// Get all applied jon by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JobApplication GetJobApplicationById(int Id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SPS_JobApplicationById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", Id);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                JobApplication application = new JobApplication
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    UserName = reader["UserName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    userId = Convert.ToInt32(reader["userId"]),
                                    jobPostId = Convert.ToInt32(reader["jobPostId"]),
                                    applicationDate = Convert.ToDateTime(reader["applicationDate"]),
                                    IsScheduled = Convert.ToBoolean(reader["IsScheduled"]),
                                    title = reader["title"].ToString(),
                                    companyName = reader["companyName"].ToString()

                                };
                                return application;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                // For example: Console.WriteLine(ex.Message);
            }

            return null; // Return null if the application is not found
        }

        /// <summary>
        /// Update the schedule field
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="isScheduled"></param>
        public void UpdateIsScheduled(int Id, bool isScheduled)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SPU_IsScheduled", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@isScheduled", isScheduled);

                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Save the interview scheduled details
        /// </summary>
        /// <param name="interview"></param>
        public void SaveScheduledInterview(ScheduledInterview interview)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SPS_ScheduledInterview", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@applicationId", interview.ApplicationId);
                    command.Parameters.AddWithValue("@userId", interview.UserId);
                    command.Parameters.AddWithValue("@jobPostId", interview.JobPostId);
                    command.Parameters.AddWithValue("@interviewDate", interview.InterviewDate);
                    command.Parameters.AddWithValue("@location", interview.Location);
                    command.Parameters.AddWithValue("@title", interview.title);
                    command.Parameters.AddWithValue("@companyName", interview.companyName);

                    command.ExecuteNonQuery();
                }
            }
        }


     
        /// <summary>
        /// Get interview based on id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        public List<ScheduledInterview> GetInterviewsByUserId(int userId)
        {
            List<ScheduledInterview> interviews = new List<ScheduledInterview>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SPS_InterviewsByUserId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ScheduledInterview interview = new ScheduledInterview
                            {
                                InterviewId = Convert.ToInt32(reader["InterviewId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                JobPostId = Convert.ToInt32(reader["JobPostId"]),
                                InterviewDate = Convert.ToDateTime(reader["InterviewDate"]),
                                Location = reader["Location"].ToString(),
                                title = reader["title"].ToString(),
                                companyName = reader["companyName"].ToString()
                            };
                            interviews.Add(interview);
                        }
                    }
                }
            }

            return interviews;
        }


        /// <summary>
        /// Reject by admin 
        /// </summary>
        /// <param name="Id"></param>
        public void RejectApplication(int Id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SPD_RejectApplication", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Id);

                    command.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Get details of applied jobs
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<JobApplication> GetAppliedJobsForUser(int userId)
        {
            List<JobApplication> appliedJobs = new List<JobApplication>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_GetAppliedJobsForUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    JobApplication jobApplication = new JobApplication
                    {
                        title = reader["title"].ToString(),
                        companyName = reader["companyName"].ToString(),
                         applicationDate = Convert.ToDateTime(reader["applicationDate"])
                    };
                    appliedJobs.Add(jobApplication);
                }
            }

            return appliedJobs;
        }


        /// <summary>
        /// Reject by admin 
        /// </summary>
        /// <param name="Id"></param>
       

    }
}








//public bool HasUserAppliedForJob(int userId, int jobId)
//{
//    using (var connection = new SqlConnection(connectionString))
//    {
//        connection.Open();
//        using (var command = new SqlCommand("SELECT COUNT(*) FROM Table_JobApplications WHERE userId = @userId AND jobPostId = @jobId", connection))
//        {
//            command.Parameters.AddWithValue("@userId", userId);
//            command.Parameters.AddWithValue("@jobId", jobId);

//            int count = Convert.ToInt32(command.ExecuteScalar());
//            return count > 0;
//        }
//    }
//}

//public bool UpdateJobApplicationStatus(int applicationId, bool isApplied)
//{
//    try
//    {
//        using (var connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            using (var command = new SqlCommand("UPDATE Table_JobApplications SET IsApplied = @IsApplied WHERE Id = @Id", connection))
//            {
//                command.Parameters.AddWithValue("@Id", applicationId);
//                command.Parameters.AddWithValue("@IsApplied", isApplied);

//                int rowsAffected = command.ExecuteNonQuery();
//                return rowsAffected > 0;
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        // Log the exception or perform other error handling.
//        return false;
//    }
//}


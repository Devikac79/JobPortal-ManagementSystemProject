
using JobPortalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services.Description;

namespace JobPortalManagementSystem.Repository
{
    public class JobPostRepository
    {   /// <summary>
        /// Database connection
        /// </summary>
        private SqlConnection connection;
        /// <summary>
        /// Defining database connection method
        /// </summary>
        private void Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();
            connection = new SqlConnection(connectionString);
        }
        string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();

        /// <summary>
        /// /Get all categories
        /// </summary>
        /// <returns></returns>

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SP_GetCategories", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category category = new Category
                            {
                                categoryId = (int)reader["categoryId"],
                                category = reader["category"].ToString()
                            };
                            categories.Add(category);
                        }
                    }
                }
            }
            return categories;
        }

        /// <summary>
        /// Get all posts
        /// </summary>
        /// <returns></returns>
        public List<JobPost> GetAllPosts()
        {
            List<JobPost> allPosts = new List<JobPost>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SP_GetJobPosts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JobPost post = new JobPost
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                title = Convert.ToString(reader["title"]),
                                location = Convert.ToString(reader["location"]),
                                minSalary = Convert.ToInt32(reader["minSalary"]),
                                maxSalary = Convert.ToInt32(reader["maxSalary"]),
                                postDate = Convert.ToDateTime(reader["postDate"]),
                                endDate = Convert.ToDateTime(reader["endDate"]),
                                description = Convert.ToString(reader["description"]),
                                jobCategory = Convert.ToString(reader["jobCategory"]),
                                jobNature = Convert.ToString(reader["jobNature"]),
                                categoryId = Convert.ToInt32(reader["categoryId"]),
                                imageData = reader["imageData"] as byte[],
                                companyName = Convert.ToString(reader["companyName"])
                            };
                            allPosts.Add(post);
                        }
                    }
                }
            }
            return allPosts;
        }
        /// <summary>
        /// Get all categories bt id
        /// </summary>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        public List<Category> GetCategoriesByIds(List<int> categoryIds)
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT categoryId, category FROM Table_JobCategory WHERE categoryId IN (" + string.Join(",", categoryIds) + ")";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryIds", string.Join(",", categoryIds));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category category = new Category
                            {
                                categoryId = Convert.ToInt32(reader["categoryId"]),
                                category = Convert.ToString(reader["category"])
                            };
                            categories.Add(category);
                        }
                    }
                }
            }
            return categories;
        }
        /// <summary>
        /// Add post
        /// </summary>
        /// <param name="jobPost"></param>
        public void AddPost(JobPost jobPost)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SP_InsertJobPost", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@title", jobPost.title);
                    command.Parameters.AddWithValue("@location", jobPost.location);
                    command.Parameters.AddWithValue("@minSalary", jobPost.minSalary);
                    command.Parameters.AddWithValue("@maxSalary", jobPost.maxSalary);
                    command.Parameters.AddWithValue("@postDate", jobPost.postDate);
                    command.Parameters.AddWithValue("@endDate", jobPost.endDate);
                    command.Parameters.AddWithValue("@description", jobPost.description);
                    command.Parameters.AddWithValue("@jobCategory", jobPost.jobCategory);
                    command.Parameters.AddWithValue("@jobNature", jobPost.jobNature);
                    command.Parameters.AddWithValue("@categoryId", jobPost.categoryId);
                    command.Parameters.AddWithValue("@imageData", jobPost.imageData ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@companyName", jobPost.companyName);
                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Get job post by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JobPost GetJobPostById(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SP_GetJobPostById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            JobPost post = new JobPost
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                title = Convert.ToString(reader["title"]),
                                location = Convert.ToString(reader["location"]),
                                minSalary = Convert.ToInt32(reader["minSalary"]),
                                maxSalary = Convert.ToInt32(reader["maxSalary"]),
                                postDate = Convert.ToDateTime(reader["postDate"]),
                                endDate = Convert.ToDateTime(reader["endDate"]),
                                description = Convert.ToString(reader["description"]),
                                jobCategory = Convert.ToString(reader["jobCategory"]),
                                jobNature = Convert.ToString(reader["jobNature"]),
                                categoryId = Convert.ToInt32(reader["categoryId"]),
                                imageData = reader["imageData"] as byte[],
                                companyName = Convert.ToString(reader["companyName"])
                            };
                            return post;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Update Job Post
        /// </summary>
        /// <param name="jobPost"></param>
        public void UpdateJobPost(JobPost jobPost)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SP_UpdateJobPost", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", jobPost.Id);
                    command.Parameters.AddWithValue("@title", jobPost.title);
                    command.Parameters.AddWithValue("@location", jobPost.location);
                    command.Parameters.AddWithValue("@minSalary", jobPost.minSalary);
                    command.Parameters.AddWithValue("@maxSalary", jobPost.maxSalary);
                    command.Parameters.AddWithValue("@postDate", jobPost.postDate);
                    command.Parameters.AddWithValue("@endDate", jobPost.endDate);
                    command.Parameters.AddWithValue("@description", jobPost.description);
                    command.Parameters.AddWithValue("@jobCategory", jobPost.jobCategory);
                    command.Parameters.AddWithValue("@jobNature", jobPost.jobNature);
                    command.Parameters.AddWithValue("@categoryId", jobPost.categoryId);
                    // Convert the imageData byte array to SqlParameter of SqlDbType.VarBinary
                    SqlParameter imageDataParam = new SqlParameter("@imageData", SqlDbType.VarBinary);
                    imageDataParam.Value = jobPost.imageData ?? (object)DBNull.Value;
                    command.Parameters.Add(imageDataParam);

                    command.Parameters.AddWithValue("@companyName", jobPost.companyName);
                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Delete Job Post
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteJobPost(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SPD_JobPost", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", Id);
                        connection.Open();
                        int affectedRows = command.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
            }
            catch 
            {
                
                return false;
            }
        }


    }
}

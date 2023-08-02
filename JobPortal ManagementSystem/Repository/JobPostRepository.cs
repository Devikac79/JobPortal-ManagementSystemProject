
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
       
        /// <summary>
        /// Job Post view
        /// </summary>
        /// <returns></returns>
        public List<JobPost> GetJobPostDetails()
        {
            Connection();
            List<JobPost> JobPostList = new List<JobPost>();
            SqlCommand command = new SqlCommand("SPS_JobPosts", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter data = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            connection.Open();
            data.Fill(dataTable);
            connection.Close();
            foreach (DataRow datarow in dataTable.Rows)

                JobPostList.Add(
                    new JobPost
                    {
                        Id = Convert.ToInt32(datarow["Id"]),
                        title = Convert.ToString(datarow["title"]),
                        location = Convert.ToString(datarow["location"]),
                        minSalary = Convert.ToInt32(datarow["minSalary"]),
                        maxSalary = Convert.ToInt32(datarow["maxSalary"]),
                       

                        postDate = Convert.ToDateTime(datarow["postDate"]),
                        endDate = Convert.ToDateTime(datarow["endDate"]),
                        description = Convert.ToString(datarow["description"]),
                        jobCategory = Convert.ToString(datarow["jobCategory"]),
                        jobNature = Convert.ToString(datarow["jobNature"]),

                    }
                    );
            return JobPostList;
        }
       
      


            /// <summary>
            /// Adding job post
            /// </summary>
            /// <param name="jobPost"></param>
            /// <returns></returns>

            public bool AddJobPost(JobPost jobPost)
            {
           
                Connection();
                SqlCommand command = new SqlCommand("SPI_JobPosts", connection);
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

                connection.Open();
                int i = command.ExecuteNonQuery();
                connection.Close();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
         
              }




       

    }
}

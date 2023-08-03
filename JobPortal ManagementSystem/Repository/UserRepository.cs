using JobPortalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using JobPortal_ManagementSystem.Models;

namespace JobPortal_ManagementSystem.Repository
{
    public class UserRepository
    {
        private SqlConnection connection;
        /// <summary>
        /// Defining database connection method
        /// </summary>
        private void Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();
            connection = new SqlConnection(connectionString);
        }


        public bool AddDetails(User user)
        {
            Connection();
            SqlCommand command = new SqlCommand("AddUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);

            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Phone", user.Phone);
            command.Parameters.AddWithValue("@Address", user.Address);
            command.Parameters.AddWithValue("@ResumePath", user.ResumePath);
            command.Parameters.AddWithValue("ImagePath", user.ImagePath);
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
        /// <summary>
        /// Viewing the database signup record
        /// </summary>
        /// <returns></returns>
        public List<User> GetDetails()
        {
            Connection();
            List<User> List = new List<User>();
            SqlCommand command = new SqlCommand("GetUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            
            SqlDataAdapter data = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            connection.Open();
            data.Fill(dataTable);
            connection.Close();
            foreach (DataRow datarow in dataTable.Rows)

                List.Add(
                    new User
                    {
                        Id = Convert.ToInt32(datarow["Id"]),
                        FirstName = Convert.ToString(datarow["FirstName"]),
                        LastName = Convert.ToString(datarow["LastName"]),
                      
                      
                        Email = Convert.ToString(datarow["Email"]),
                        Password = Convert.ToString(datarow["Password"]),
                        Phone = Convert.ToString(datarow["Phone"]),
                        Address = Convert.ToString(datarow["Address"]),
                        TenthPercentageOrGrade = Convert.ToString(datarow["TenthPercentageOrGrade"]),
                        TwelfthPercentageOrGrade = Convert.ToString(datarow["TwelfthPercentageOrGrade"]),
                        GraduationGradeOrPercentage = Convert.ToString(datarow["GraduationGradeOrPercentage"]),
                        PostGraduationGradeOrPercentage = Convert.ToString(datarow["PostGraduationGradeOrPercentage"]),
                        ResumePath = Convert.ToString(datarow["ResumePath"]),
                        ImagePath = Convert.ToString(datarow["ImagePath"])

    }
                    );
            return List;
        }
        public bool EditDetails(User user)
        {
            Connection();
            SqlCommand command = new SqlCommand("UpdateUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", user.Id);

            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);

            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Phone", user.Phone);
            command.Parameters.AddWithValue("@Address", user.Address);
            command.Parameters.AddWithValue("@TenthPercentageOrGrade", user.TenthPercentageOrGrade);
            command.Parameters.AddWithValue("@TwelfthPercentageOrGrade", user.TwelfthPercentageOrGrade);
            command.Parameters.AddWithValue("@GraduationGradeOrPercentage", user.GraduationGradeOrPercentage);
            command.Parameters.AddWithValue("@PostGraduationGradeOrPercentage", user.PostGraduationGradeOrPercentage);
            command.Parameters.AddWithValue("@ResumePath", user.ResumePath);
            command.Parameters.AddWithValue("@ImagePath", user.ImagePath);
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
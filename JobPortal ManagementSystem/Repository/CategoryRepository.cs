using JobPortalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JobPortal_ManagementSystem.Repository
{
    public class CategoryRepository
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
        string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();

            public bool AddCategory(Category category)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Table_JobCategory (category) VALUES (@category)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlParameter paramCategory = new SqlParameter("@category", SqlDbType.VarChar);
                    paramCategory.Value = category.category;
                    command.Parameters.Add(paramCategory);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }



        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SPS_AllCategories", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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


    }
}
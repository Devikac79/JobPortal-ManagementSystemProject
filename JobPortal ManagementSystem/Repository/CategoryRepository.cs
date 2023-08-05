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


        /*  public bool AddCategory(Category category)
          {
              Connection();
              SqlCommand command = new SqlCommand("SPI_Category", connection);
              command.CommandType = CommandType.StoredProcedure;
              command.Parameters.AddWithValue("@category", category.category);

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
          }*/
        public bool AddCategory(Category category)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPI_Category", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@category", category.category);

            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();

            return i > 0;
        }

        string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();
        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT categoryId, category FROM Table_JobCategory";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
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
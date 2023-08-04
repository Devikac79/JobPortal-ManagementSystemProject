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


        public bool AddCategory(Category category)
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
        }
    }
}
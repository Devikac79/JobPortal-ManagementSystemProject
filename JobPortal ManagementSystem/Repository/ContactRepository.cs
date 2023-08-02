using JobPortalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace JobPortalManagementSystem.Repository
{
    
        public class ContactRepository
        {
            /// <summary>
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
            public bool AddContact(Contact contact)
            {
                Connection();
                SqlCommand command = new SqlCommand("SPI_Contact", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@name", contact.name);
                command.Parameters.AddWithValue("@email", contact.email);
            command.Parameters.AddWithValue("@subject", contact.subject);

            command.Parameters.AddWithValue("@message", contact.message);
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
            public List<Contact> GetContact()
            {
                Connection();
                List<Contact> ContactList = new List<Contact>();
                SqlCommand command = new SqlCommand("SPS_Contact", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter data = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                connection.Open();
                data.Fill(dataTable);
                connection.Close();
                foreach (DataRow datarow in dataTable.Rows)

                    ContactList.Add(
                        new Contact
                        {
                            contactId = Convert.ToInt32(datarow["contactId"]),
                            name = Convert.ToString(datarow["name"]),
                            email = Convert.ToString(datarow["email"]),
                            subject= Convert.ToString(datarow["subject"]),

                            message = Convert.ToString(datarow["message"]),
                        }
                        );
                return ContactList;
            }
        }
}
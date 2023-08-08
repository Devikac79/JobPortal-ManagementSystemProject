
using JobPortal_ManagementSystem.Models;
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
    public class SignupRepository
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
        /// Password enctrypting for database
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();

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

        /// <summary>
        /// Adding a new details to the signup record
        /// </summary>
        /// <param name="signup"></param>
        public bool AddSignupDetails(Signup signup)
        {
            try
            {
                Connection();
                SqlCommand command = new SqlCommand("SPI_Signup", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@firstName", signup.firstName);
                command.Parameters.AddWithValue("@lastName", signup.lastName);
                command.Parameters.AddWithValue("@dateOfBirth", signup.dateOfBirth);
                command.Parameters.AddWithValue("@gender", signup.gender);
                command.Parameters.AddWithValue("@email", signup.email);
                command.Parameters.AddWithValue("@phone", signup.phone);
                command.Parameters.AddWithValue("@address", signup.address);
                command.Parameters.AddWithValue("@city", signup.city);
                command.Parameters.AddWithValue("@state", signup.state);
                command.Parameters.AddWithValue("@pincode", signup.pincode);
                command.Parameters.AddWithValue("@country", signup.country);
                command.Parameters.AddWithValue("@username", signup.username);
               
                command.Parameters.AddWithValue("@password", signup.password);


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
            catch (Exception ex)
            {
                
                Console.WriteLine("An error occurred while adding signup details: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Viewing the database signup record
        /// </summary>
        /// <returns></returns>
        public List<Signup> GetSignupDetails()
        {
            Connection();
            List<Signup> SignupList = new List<Signup>();
            SqlCommand command = new SqlCommand("SPS_Signup", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter data = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            connection.Open();
            data.Fill(dataTable);
            connection.Close();
            foreach (DataRow datarow in dataTable.Rows)

                SignupList.Add(
                    new Signup
                    {
                        Id = Convert.ToInt32(datarow["Id"]),
                        firstName = Convert.ToString(datarow["firstName"]),
                        lastName = Convert.ToString(datarow["lastName"]),
                        dateOfBirth = Convert.ToDateTime(datarow["dateOfBirth"]),
                        gender = Convert.ToString(datarow["gender"]),
                        email = Convert.ToString(datarow["email"]),
                        phone = Convert.ToString(datarow["phone"]),
                        address = Convert.ToString(datarow["address"]),
                        city = Convert.ToString(datarow["city"]),
                        state = Convert.ToString(datarow["state"]),
                        pincode = Convert.ToInt32(datarow["pincode"]),
                        country = Convert.ToString(datarow["country"]),
                        username = Convert.ToString(datarow["username"]),
                        
                    }
                    );
            return SignupList;
        }
   
        /// <summary>
        /// Deleting the signup record of the memeber with specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteSignupDetails(int Id)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPD_Signup", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", Id);
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





        public string GetUserRole(string username, string password)
        {
            Connection();
            SqlCommand command = new SqlCommand("Sp_ValidateUserAndGetRole", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", username);
         
            command.Parameters.AddWithValue("@password", password);

            connection.Open();
            var role = command.ExecuteScalar() as string;
            connection.Close();

            return role;
        }

        public Signup GetSignupDetailsByUsernameAndPassword(string username, string password)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPS_SignupByUsernameAndPassword", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Signup signup = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    signup = new Signup
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        firstName = Convert.ToString(reader["firstName"]),
                        lastName = Convert.ToString(reader["lastName"]),
                        dateOfBirth = Convert.ToDateTime(reader["dateOfBirth"]),
                        gender = Convert.ToString(reader["gender"]),
                        email = Convert.ToString(reader["email"]),
                        phone = Convert.ToString(reader["phone"]),
                        address = Convert.ToString(reader["address"]),
                        city = Convert.ToString(reader["city"]),
                        state = Convert.ToString(reader["state"]),
                        pincode = Convert.ToInt32(reader["pincode"]),
                        country = Convert.ToString(reader["country"]),
                        username = Convert.ToString(reader["username"])
                    };
                }
            }

            reader.Close();
            connection.Close();
            return signup;
        }


        public Signup GetSignupById(int Id)
        {
            // Implement the logic to fetch the user details based on ID from the database.
            // Ensure to return null if the user is not found.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SPS_SignupById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", Id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Signup userSignup = new Signup
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        firstName = Convert.ToString(reader["firstName"]),
                        lastName = Convert.ToString(reader["lastName"]),
                        dateOfBirth = Convert.ToDateTime(reader["dateOfBirth"]),
                        gender = Convert.ToString(reader["gender"]),
                        email = Convert.ToString(reader["email"]),
                        phone = Convert.ToString(reader["phone"]),
                        address = Convert.ToString(reader["address"]),
                        city = Convert.ToString(reader["city"]),
                        state = Convert.ToString(reader["state"]),
                        pincode = Convert.ToInt32(reader["pincode"]),
                        country = Convert.ToString(reader["country"]),
                        username = Convert.ToString(reader["username"]),
                        // Add other properties as needed
                    };

                    return userSignup;
                }
            }

            // User not found, return null
            return null;
        }





        public bool EditSignupDetails(Signup signup)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SPU_Signup", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", signup.Id);

                    command.Parameters.AddWithValue("@firstName", signup.firstName);
                    command.Parameters.AddWithValue("@lastName", signup.lastName);
                    command.Parameters.AddWithValue("@dateOfBirth", signup.dateOfBirth);
                    command.Parameters.AddWithValue("@gender", signup.gender);
                    command.Parameters.AddWithValue("@email", signup.email);
                    command.Parameters.AddWithValue("@phone", signup.phone);
                    command.Parameters.AddWithValue("@address", signup.address);
                    command.Parameters.AddWithValue("@city", signup.city);
                    command.Parameters.AddWithValue("@state", signup.state);
                    command.Parameters.AddWithValue("@pincode", signup.pincode);
                    command.Parameters.AddWithValue("@country", signup.country);
                    command.Parameters.AddWithValue("@username", signup.username);
                    command.Parameters.AddWithValue("@password", signup.password);

                    connection.Open();
                    int i = command.ExecuteNonQuery();


                    return i > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while editing signup details: " + ex.Message);
                return false;
            }
        }



        /*   public bool EditSignupDetails(Signup signup)
           {
               try
               {
                   using (SqlConnection connection = new SqlConnection(connectionString))
                   {
                       SqlCommand command = new SqlCommand("SPU_Signup", connection);
                       command.CommandType = CommandType.StoredProcedure;
                       command.Parameters.AddWithValue("@Id", signup.Id);

                       command.Parameters.AddWithValue("@firstName", signup.firstName);
                       command.Parameters.AddWithValue("@lastName", signup.lastName);
                       command.Parameters.AddWithValue("@dateOfBirth", signup.dateOfBirth);
                       command.Parameters.AddWithValue("@gender", signup.gender);
                       command.Parameters.AddWithValue("@email", signup.email);
                       command.Parameters.AddWithValue("@phone", signup.phone);
                       command.Parameters.AddWithValue("@address", signup.address);
                       command.Parameters.AddWithValue("@city", signup.city);
                       command.Parameters.AddWithValue("@state", signup.state);
                       command.Parameters.AddWithValue("@pincode", signup.pincode);
                       command.Parameters.AddWithValue("@country", signup.country);
                       command.Parameters.AddWithValue("@username", signup.username);
                       command.Parameters.AddWithValue("@password", signup.password);

                       // Add the new fields
                       //  command.Parameters.AddWithValue("profileImage", signup.profileImage);

                       connection.Open();
                       int i = command.ExecuteNonQuery();

                       return i > 0;
                   }
               }
               catch (Exception ex)
               {
                   Console.WriteLine("An error occurred while editing signup details: " + ex.Message);
                   return false;
               }
           }
        */

    }
}
   



using JobPortal_ManagementSystem.Models;
using JobPortal_ManagementSystem.Repository;
using JobPortalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

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
       
        string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();
        /// <summary>
        /// Encrypt the password
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        private string Encrypt(string clearText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return clearText;
        }

        /// <summary>
        /// Decrypt the password
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        private string Decrypt(string cipherText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipherText;
        }

        /// <summary>
        /// Get user profile by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Signup GetUserProfileById(int userId)
        {
            Signup userProfile = null;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SP_GetUserProfileById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
              command.Parameters.AddWithValue("@password", Encrypt(signup.password));
             //  command.Parameters.AddWithValue("@password", signup.password);
                //string imageBase64 = Convert.ToBase64String(signup.image);
                // command.Parameters.AddWithValue("@image", imageBase64);
              //  command.Parameters.Add("@image", SqlDbType.VarBinary, -1).Value = signup.image;
              //  command.Parameters.Add("@resume", SqlDbType.VarBinary, -1).Value = signup.resume;
                //  command.Parameters.AddWithValue("@image", signup.image);

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
        /// Get Signup details by id
        /// </summary>
        /// <returns></returns>
        public List<Signup> GetSignupDetails()
        {
            List<Signup> allsignup = new List<Signup>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SPS_Signup", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader datarow = command.ExecuteReader())
                    {
                        while (datarow.Read())
                        {
                            Signup signup = new Signup
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
                                image = datarow["image"] as byte[],
                                resume = datarow["resume"] as byte[],
                            };
                            allsignup.Add(signup);
                        }
                    }
                }
            }
            return allsignup;
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

        /// <summary>
        /// Get user role
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>

        public string GetUserRole(string username, string password)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPS_ValidateUserAndGetRole", connection);
            command.CommandType = CommandType.StoredProcedure;
           command.Parameters.AddWithValue("@username", username);
         command.Parameters.AddWithValue("@password", Encrypt(password));

        //  command.Parameters.AddWithValue("@password", password);

            connection.Open();
            var role = command.ExecuteScalar() as string;
            connection.Close();

            return role;
        }
        /// <summary>
        /// Get userdetails by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Signup GetSignupDetailsByUsernameAndPassword(string username)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPS_SignupByUsernameAndPassword", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", username);
             // command.Parameters.AddWithValue("@password", password);
             // command.Parameters.AddWithValue("@password", Encrypt(password));
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

        /// <summary>
        /// Get SIgnup details
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
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
                        password = Convert.ToString(reader["password"]),
                       // password = Decrypt(Convert.ToString(reader["password"])),
                        image = reader["image"] as byte[],
                        resume = reader["resume"] as byte[],
                        // Add other properties as needed
                    };

                    return userSignup;
                }
            }

            // User not found, return null
            return null;
        }


        /// <summary>
        /// Edit SIgnup details
        /// </summary>
        /// <param name="signup"></param>
        public void EditSignupDetails(Signup signup)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
              // string decryptedPassword = Decrypt(signup.password); // Replace with your decryption logic

                connection.Open();
                using (SqlCommand command = new SqlCommand("SPU_Signup", connection))
                {
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

                    command.Parameters.AddWithValue("@password",Encrypt(signup.password));
                  // command.Parameters.AddWithValue("@password", decryptedPassword);
                    // Convert the imageData byte array to SqlParameter of SqlDbType.VarBinary
                    /* SqlParameter imageDataParam = new SqlParameter("@image", SqlDbType.VarBinary);
                     imageDataParam.Value = signup.image ?? (object)DBNull.Value;
                     command.Parameters.Add(imageDataParam);
                     SqlParameter resumeDataParam = new SqlParameter("@resume", SqlDbType.VarBinary);
                     resumeDataParam.Value = signup.resume ?? (object)DBNull.Value;
                     command.Parameters.Add(resumeDataParam);*/
                    SqlParameter imageParam = new SqlParameter("@image", SqlDbType.VarBinary, -1);
                    imageParam.Value = signup.image ?? (object)DBNull.Value; // If null, set to DBNull.Value
                    command.Parameters.Add(imageParam);

                    SqlParameter resumeParam = new SqlParameter("@resume", SqlDbType.VarBinary, -1);
                    resumeParam.Value = signup.resume ?? (object)DBNull.Value; // If null, set to DBNull.Value
                    command.Parameters.Add(resumeParam);

                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get SIgnup by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Signup GetSignUpById(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SP_GetSignupById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Id);
                    using (SqlDataReader datarow = command.ExecuteReader())
                    {
                        if (datarow.Read())
                        {
                            Signup signup = new Signup
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
                                password = Convert.ToString(datarow["password"]),
                                image = datarow["image"] as byte[],
                                resume = datarow["resume"] as byte[],
                            };
                            return signup;
                        }
                    }
                }
            }
            return null;
        }





        public void DeleteUser(int Id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SPD_DeleteUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Id);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}


/*   public List<Country> GetCountries()
   {
       List<Country> countries = new List<Country>();

       using (SqlConnection connection = new SqlConnection(connectionString))
       {
           string query = "SELECT * FROM Countries";
           using (SqlCommand command = new SqlCommand(query, connection))
           {
               connection.Open();
               using (SqlDataReader reader = command.ExecuteReader())
               {
                   while (reader.Read())
                   {
                       countries.Add(new Country
                       {
                           CountryId = (int)reader["CountryId"],
                           CountryName = (string)reader["CountryName"]
                       });
                   }
               }
           }
       }

       return countries;
   }


   public List<State> GetStatesByCountry(int CountryId)
   {
       List<State> states = new List<State>();

       using (SqlConnection connection = new SqlConnection(connectionString))
       {
           string query = "SELECT * FROM States WHERE CountryId = @CountryId";
           using (SqlCommand command = new SqlCommand(query, connection))
           {
               command.Parameters.AddWithValue("@CountryId", CountryId);
               connection.Open();
               using (SqlDataReader reader = command.ExecuteReader())
               {
                   while (reader.Read())
                   {
                       states.Add(new State
                       {
                           StateId = (int)reader["StateId"],
                           StateName = (string)reader["StateName"],
                           CountryId = (int)reader["CountryId"]
                       });
                   }
               }
           }
       }

       return states;
   }

   public List<City> GetCitiesByState(int StateId)
   {
       List<City> cities = new List<City>();

       using (SqlConnection connection = new SqlConnection(connectionString))
       {
           string query = "SELECT * FROM Cities WHERE StateId = @StateId";
           using (SqlCommand command = new SqlCommand(query, connection))
           {
               command.Parameters.AddWithValue("@StateId", StateId);
               connection.Open();
               using (SqlDataReader reader = command.ExecuteReader())
               {
                   while (reader.Read())
                   {
                       cities.Add(new City
                       {
                           CityId = (int)reader["CityId"],
                           CityName = (string)reader["CityName"],
                           StateId = (int)reader["StateId"]
                       });
                   }
               }
           }
       }

       return cities;
   }

   public void RegisterUser(UserRegistration user)
   {
       using (SqlConnection connection = new SqlConnection(connectionString))
       {
           string query = "INSERT INTO UserRegistration (CountryId, StateId, CityId) " +
                          "VALUES (@CountryId, @StateId, @CityId)";

           using (SqlCommand command = new SqlCommand(query, connection))
           {
               command.Parameters.AddWithValue("@CountryId", user.CountryId);
               command.Parameters.AddWithValue("@StateId", user.StateId);
               command.Parameters.AddWithValue("@CityId", user.CityId);

               connection.Open();
               command.ExecuteNonQuery();
           }
       }
   }
*/







/// <summary>
/// Viewing the database signup record
/// </summary>
/// <returns></returns>
/* public List<Signup> GetSignupDetails()
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
*/



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
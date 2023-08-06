
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
        /*   public List<Country> GetCountries()
           {
               List<Country> countries = new List<Country>();
               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                   connection.Open();

                   string query = "SELECT countryId, countryName FROM Table_Countries";
                   using (SqlCommand command = new SqlCommand(query, connection))
                   {
                       using (SqlDataReader reader = command.ExecuteReader())
                       {
                           while (reader.Read())
                           {
                               Country country = new Country
                               {
                                   countryId = Convert.ToInt32(reader["countryId"]),
                                   countryName = reader["countryName"].ToString()
                               };
                               countries.Add(country);
                           }
                       }
                   }
               }
               return countries;
           }

           public List<State> GetStates()
           {
               List<State> states = new List<State>();
               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                   connection.Open();

                   string query = "SELECT stateId, stateName, countryId FROM Table_States";
                   using (SqlCommand command = new SqlCommand(query, connection))
                   {
                       using (SqlDataReader reader = command.ExecuteReader())
                       {
                           while (reader.Read())
                           {
                               State state = new State
                               {
                                   stateId = Convert.ToInt32(reader["stateId"]),
                                   stateName = reader["stateName"].ToString(),
                                   countryId = Convert.ToInt32(reader["countryId"])
                               };
                               states.Add(state);
                           }
                       }
                   }
               }
               return states;
           }

           public List<City> GetCities()
           {
               List<City> cities = new List<City>();
               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                   connection.Open();

                   string query = "SELECT cityId, cityName, stateId FROM Table_Cities";
                   using (SqlCommand command = new SqlCommand(query, connection))
                   {
                       using (SqlDataReader reader = command.ExecuteReader())
                       {
                           while (reader.Read())
                           {
                               City city = new City
                               {
                                   cityId = Convert.ToInt32(reader["cityId"]),
                                   cityName = reader["cityName"].ToString(),
                                   stateId = Convert.ToInt32(reader["stateId"])
                               };
                               cities.Add(city);
                           }
                       }
                   }
               }
               return cities;
           }*/
 /*       public List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT countryId, countryName FROM Table_Countries";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Country country = new Country
                            {
                                countryId = Convert.ToInt32(reader["countryId"]),
                                countryName = reader["countryName"].ToString()
                            };
                            countries.Add(country);
                        }
                    }
                }
            }

            return countries;
        }

        */
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
     /*   /// <summary>
        /// Updating the signup record
        /// </summary>
        /// <param name="signup"></param>
        /// <returns></returns>
       public bool EditSignupDetails(Signup signup)
        {
            Connection();
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
         //   string hashedPassword = HashPassword(password);
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



        public List<Education> GetEducationsByUserId(int userId)
        {
            List<Education> educations = new List<Education>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Table_Education WHERE userId = @userId", connection);
                    command.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Education education = new Education
                        {
                            educationId = Convert.ToInt32(reader["educationId"]),
                            userId = Convert.ToInt32(reader["userId"]),
                            degree = Convert.ToString(reader["degree"]),
                            institute = Convert.ToString(reader["institute"]),
                            yearOfPassing = Convert.ToInt32(reader["yearOfPassing"])
                        };

                        educations.Add(education);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while fetching educations: " + ex.Message);
            }

            return educations;
        }

        public List<Experience> GetExperiencesByUserId(int userId)
        {
            List<Experience> experiences = new List<Experience>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Table_Experience WHERE userId = @userId", connection);
                    command.Parameters.AddWithValue("@userId", userId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Experience experience = new Experience
                        {
                            experienceId = Convert.ToInt32(reader["experienceId"]),
                            userId = Convert.ToInt32(reader["userId"]),
                            company = Convert.ToString(reader["company"]),
                            position = Convert.ToString(reader["position"]),
                            startDate = Convert.ToDateTime(reader["startDate"]),
                            endDate = reader["endDate"] != DBNull.Value ? Convert.ToDateTime(reader["endDate"]) : (DateTime?)null
                        };

                        experiences.Add(experience);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while fetching experiences: " + ex.Message);
            }

            return experiences;
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

                /*    // Update user's education in the database
                    foreach (var education in signup.Educations)
                    {
                        SqlCommand educationCommand;
                        if (education.educationId > 0)
                        {
                            // If the education record already has an ID, it means it's an existing record to be updated.
                            educationCommand = new SqlCommand("UPDATE Table_Education SET degree = @degree, institute = @institute, yearOfPassing = @yearOfPassing WHERE educationId = @educationId", connection);
                            educationCommand.Parameters.AddWithValue("@educationId", education.educationId);
                        }
                        else
                        {
                            // If the education record doesn't have an ID, it means it's a new record to be inserted.
                            educationCommand = new SqlCommand("INSERT INTO Table_Education (userId, degree, institute, yearOfPassing) VALUES (@userId, @degree, @institute, @yearOfPassing)", connection);
                            educationCommand.Parameters.AddWithValue("@userId", signup.Id);
                        }

                        educationCommand.Parameters.AddWithValue("@degree", education.degree);
                        educationCommand.Parameters.AddWithValue("@institute", education.institute);
                        educationCommand.Parameters.AddWithValue("@yearOfPassing", education.yearOfPassing);

                        educationCommand.ExecuteNonQuery();
                    }

                    // Update user's experience in the database
                    foreach (var experience in signup.Experiences)
                    {
                        SqlCommand experienceCommand;
                        if (experience.experienceId > 0)
                        {
                            // If the experience record already has an ID, it means it's an existing record to be updated.
                            experienceCommand = new SqlCommand("UPDATE Table_Experience SET company = @company, position = @position, startDate = @startDate, endDate = @endDate WHERE experienceId = @experienceId", connection);
                            experienceCommand.Parameters.AddWithValue("@experienceId", experience.experienceId);
                        }
                        else
                        {
                            // If the experience record doesn't have an ID, it means it's a new record to be inserted.
                            experienceCommand = new SqlCommand("INSERT INTO Table_Experience (userId, company, position, startDate, endDate) VALUES (@userId, @company, @position, @startDate, @endDate)", connection);
                            experienceCommand.Parameters.AddWithValue("@userId", signup.Id);
                        }

                        experienceCommand.Parameters.AddWithValue("@company", experience.company);
                        experienceCommand.Parameters.AddWithValue("@position", experience.position);
                        experienceCommand.Parameters.AddWithValue("@startDate", experience.startDate);
                        experienceCommand.Parameters.AddWithValue("@endDate", experience.endDate ?? (object)DBNull.Value);

                        experienceCommand.ExecuteNonQuery();
                    }*/

                    return i > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while editing signup details: " + ex.Message);
                return false;
            }
        }
       

        /*
                public List<Country> GetCountries()
                {
                    var countries = new List<Country>();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("GetCountries", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var country = new Country
                                    {
                                        countryId = Convert.ToInt32(reader["countryId"]),
                                        countryName = reader["countryName"].ToString()
                                    };
                                    countries.Add(country);
                                }
                            }
                        }
                    }

                    return countries;

                }

                public List<State> GetStatesByCountry(int countryId)
                {
                    var states = new List<State>();

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("GetStatesByCountry", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@countryId", countryId);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var state = new State
                                    {
                                        stateId = Convert.ToInt32(reader["stateId"]),
                                        stateName = reader["stateName"].ToString(),
                                        countryId = countryId
                                    };
                                    states.Add(state);
                                }
                            }
                        }
                    }

                    return states;
                }

                public List<City> GetCitiesByState(int stateId)
                {
                    var cities = new List<City>();

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("GetCitiesByState", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@stateId", stateId);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())


                                {
                                    var city = new City
                                    {
                                        cityId = Convert.ToInt32(reader["cityId"]),
                                        cityName = reader["cityName"].ToString(),
                                        stateId = stateId
                                    };
                                    cities.Add(city);
                                }
                            }
                        }
                    }

                    return cities;
                }

                */
        /*   public List<Education> GetEducationsByUserId(int userId)
            {
                List<Education> educations = new List<Education>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT educationId, userId, degree, institute, yearOfPassing FROM Table_Education WHERE userId = @userId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Education education = new Education
                                {
                                    educationId = Convert.ToInt32(reader["educationId"]),
                                    userId = Convert.ToInt32(reader["userId"]),
                                    degree = reader["degree"].ToString(),
                                    institute = reader["institute"].ToString(),
                                    yearOfPassing = Convert.ToInt32(reader["yearOfPassing"])
                                };
                                educations.Add(education);
                            }
                        }
                    }
                }

                return educations;
            }

            public List<Experience> GetExperiencesByUserId(int userId)
            {
                List<Experience> experiences = new List<Experience>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT experienceId, userId, company, position, startDate, endDate FROM Table_Experience WHERE userId = @userId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Experience experience = new Experience
                                {
                                    experienceId = Convert.ToInt32(reader["experienceId"]),
                                    userId = Convert.ToInt32(reader["userId"]),
                                    company = reader["company"].ToString(),
                                    position = reader["position"].ToString(),
                                    startDate = Convert.ToDateTime(reader["startDate"]),
                                    endDate = DBNull.Value.Equals(reader["endDate"]) ? (DateTime?)null : Convert.ToDateTime(reader["endDate"])
                                };
                                experiences.Add(experience);
                            }
                        }
                    }
                }

                return experiences;
            }

            public Signup GetSignupDetailsById(int userId)
            {
                Connection();
                Signup signup = null;
                SqlCommand command = new SqlCommand("SPS_SignupById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userId", userId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        signup = new Signup
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            firstName = reader["firstName"].ToString(),
                            lastName = reader["lastName"].ToString(),
                            dateOfBirth = Convert.ToDateTime(reader["dateOfBirth"]),
                            gender = reader["gender"].ToString(),
                            email = reader["email"].ToString(),
                            phone = reader["phone"].ToString(),
                            address = reader["address"].ToString(),
                            city = reader["city"].ToString(),
                            state = reader["state"].ToString(),
                            pincode = Convert.ToInt32(reader["pincode"]),
                            country = reader["country"].ToString(),
                            username = reader["username"].ToString(),
                            password = reader["password"].ToString(),
                        };
                    }
                }

                if (signup != null)
                {
                    signup.Educations = GetEducationsByUserId(userId);
                    signup.Experiences = GetExperiencesByUserId(userId);
                }

                return signup;
            }

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
                    return i > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while adding signup details: " + ex.Message);
                    return false;
                }
            }

            public bool AddEducationDetails(int userId, Education education)
            {
                try
                {
                    Connection();
                    SqlCommand command = new SqlCommand("SPI_Education", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@degree", education.degree);
                    command.Parameters.AddWithValue("@institute", education.institute);
                    command.Parameters.AddWithValue("@yearOfPassing", education.yearOfPassing);

                    connection.Open();
                    int i = command.ExecuteNonQuery();
                    connection.Close();
                    return i > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while adding education details: " + ex.Message);
                    return false;
                }
            }

            public bool AddExperienceDetails(int userId, Experience experience)
            {
                try
                {
                    Connection();
                    SqlCommand command = new SqlCommand("SPI_Experience", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@company", experience.company);
                    command.Parameters.AddWithValue("@position", experience.position);
                    command.Parameters.AddWithValue("@startDate", experience.startDate);
                    command.Parameters.AddWithValue("@endDate", experience.endDate);

                    connection.Open();
                    int i = command.ExecuteNonQuery();
                    connection.Close();
                    return i > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while adding experience details: " + ex.Message);
                    return false;
                }
            }

            public bool UpdateUserProfile(Signup signup)
            {
                try
                {

                    Connection();
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
                    connection.Close();
                    return i > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while updating signup details: " + ex.Message);
                    return false;
                }
            }*/
    }
}
   


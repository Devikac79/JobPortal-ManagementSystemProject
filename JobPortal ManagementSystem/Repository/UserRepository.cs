﻿using JobPortalManagementSystem.Models;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        public bool AddDetails(User user)
        {
            try
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
                command.Parameters.AddWithValue("@ImagePath", user.ImagePath);
              

                connection.Open();
                int i = command.ExecuteNonQuery();
                connection.Close();
                return i > 0;
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, log it, and return false to indicate failure.
                // You can also rethrow the exception if needed.
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
                        ImagePath = Convert.ToString(datarow["ImagePath"]),
                        // image = reader["image"] as byte[],
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

        string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();


        public void InsertUploadedFile(UploadedFile file)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("InsertUploadedFile", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FileName", file.FileName);
                    command.Parameters.AddWithValue("@FileData", file.FileData);
                    command.Parameters.AddWithValue("@Image", file.ImageData);

                    command.ExecuteNonQuery();
                }
            }
        }



        public List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Countries", connection);

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

            return countries;
        }

        public List<State> GetStatesByCountryId(int countryId)
        {
            List<State> states = new List<State>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM States WHERE CountryId = @CountryId", connection);
                command.Parameters.AddWithValue("@CountryId", countryId);

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

            return states;
        }

        public List<City> GetCitiesByStateId(int stateId)
        {
            List<City> cities = new List<City>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Cities WHERE StateId = @StateId", connection);
                command.Parameters.AddWithValue("@StateId", stateId);

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

            return cities;
        }

        public void SaveUserRegistration(UserRegistration user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                    "INSERT INTO UserRegistrations ( CountryId, StateId, CityId) VALUES ( @CountryId, @StateId, @CityId)",
                    connection);

              //  command.Parameters.AddWithValue("@UserId", user.UserId);
                command.Parameters.AddWithValue("@CountryId", user.CountryId);
                command.Parameters.AddWithValue("@StateId", user.StateId);
                command.Parameters.AddWithValue("@CityId", user.CityId);

                command.ExecuteNonQuery();
            }
        }



    }
}
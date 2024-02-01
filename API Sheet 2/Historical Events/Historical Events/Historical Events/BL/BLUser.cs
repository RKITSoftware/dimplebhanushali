using Historical_Events.Models;
using Historical_Events.User_Validation;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Historical_Events.BL
{
    public class BLUser
    {
        private static string _connection;

        public BLUser(string connectionString)
        {
            _connection = connectionString;
        }

        public string RegisterUser(usr01 objUser)
        {
            if (objUser == null)
            {
                return "Invalid details";
            }
            InsertUser(objUser);
            return "User added";
        }

        public bool LoginUser(string userName, string password)
        {
            // Validate credentials
            return ValidateUser.IsLogin(userName, password);
        }

        public static List<usr01> GetAllUsers()
        {
            string query = @"SELECT 
                                    r01f01, 
                                    r01f02, 
                                    r01f03, 
                                    r01f04, 
                                    r01f05 
                            FROM 
                                    usr01";
            return GetUsersFromDatabase(query);
        }

        public usr01 GetUserById(int id)
        {
            string query = $"SELECT r01f01, r01f02, r01f03, r01f04, r01f05 FROM usr01 WHERE r01f01 = {id};";
            List<usr01> users = GetUsersFromDatabase(query);

            return users.Count > 0 ? users[0] : null;
        }

        public usr01 DeleteUser(int id)
        {
            // Get the user to be deleted
            usr01 deletedUser = GetUserById(id);

            if (deletedUser != null)
            {
                // Execute the delete query
                string query = $"DELETE FROM usr01 WHERE r01f01 = {id};";
                ExecuteNonQuery(query);

                // Return the deleted user
                return deletedUser;
            }

            // Return null if the user doesn't exist
            return null;
        }

        private static List<usr01> GetUsersFromDatabase(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    return GetUsersFromDataReader(command.ExecuteReader());
                }
            }
        }

        private static List<usr01> GetUsersFromDataReader(MySqlDataReader reader)
        {
            List<usr01> resultList = new List<usr01>();

            while (reader.Read())
            {
                usr01 objUser = new usr01
                {
                    r01f01 = Convert.ToInt32(reader["r01f01"]),
                    r01f02 = reader["r01f02"].ToString(),
                    r01f03 = reader["r01f03"].ToString(),
                    r01f04 = reader["r01f04"].ToString(),
                    r01f05 = reader["r01f05"].ToString()
                };

                resultList.Add(objUser);
            }

            return resultList;
        }

        private void InsertUser(usr01 user)
        {
            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                string query = @"INSERT INTO 
                                            usr01 
                                                (r01f02, 
                                                r01f03, 
                                                r01f04, 
                                                r01f05) 
                                VALUES 
                                                (@r01f02, 
                                                @r01f03, 
                                                @r01f04, 
                                                @r01f05)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@r01f02", user.r01f02);
                    command.Parameters.AddWithValue("@r01f03", user.r01f03);
                    command.Parameters.AddWithValue("@r01f04", user.r01f04);
                    command.Parameters.AddWithValue("@r01f05", user.r01f05);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void ExecuteNonQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
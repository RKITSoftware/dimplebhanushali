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

        public string RegisterUser(User objUser)
        {
            if (objUser == null)
            {
                return "invalid Details";
            }
            InsertUser(objUser);
            return "User Added";
        }

        public bool LoginUser(string userName, string password)
        {
            // Validate credentials
            return ValidateUser.IsLogin(userName, password);
        }

        public static List<User> GetAllUsers()
        {
            string query = "SELECT * FROM users";
            return GetUsersFromDatabase(query);
        }

        public User GetUserById(int id)
        {
            string query = $"SELECT * FROM users WHERE Id = {id};";
            List<User> users = GetUsersFromDatabase(query);

            return users.Count > 0 ? users[0] : null;
        }

        public User DeleteUser(int id)
        {
            // Get the user to be deleted
            User deletedUser = GetUserById(id);

            if (deletedUser != null)
            {
                // Execute the delete query
                string query = $"DELETE FROM users WHERE Id = {id};";
                GetUsersFromDatabase(query);

                // Return the deleted user
                return deletedUser;
            }

            // Return null if the user doesn't exist
            return null;
        }


        private static List<User> GetUsersFromDatabase(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    return GetUsersFromDatabase(command);
                }
            }
        }

        private static List<User> GetUsersFromDatabase(MySqlCommand command)
        {
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                List<User> resultList = new List<User>();

                while (reader.Read())
                {
                    User objUser = new User
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["name"].ToString(),
                        Password = reader["password"].ToString(),
                        UserName = reader["username"].ToString(),
                        Roles = reader["role"].ToString()
                    };

                    resultList.Add(objUser);
                }
                return resultList;
            }
        }

        private void InsertUser(User user)
        {
            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                string query = "INSERT INTO users (Name, UserName, Password, Role) VALUES (@Name, @UserName, @Password, @Roles)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@UserName", user.UserName);
                    command.Parameters.AddWithValue("@Password", BLAES.Encrypt(user.Password)); // store Encrypted Password
                    command.Parameters.AddWithValue("@Roles", "user");

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
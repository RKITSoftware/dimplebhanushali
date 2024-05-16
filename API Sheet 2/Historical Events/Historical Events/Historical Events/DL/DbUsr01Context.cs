using Historical_Events.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Historical_Events.DL
{
    /// <summary>
    /// Database class for managing users.
    /// </summary>
    public class DbUsr01Context
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        private static string _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbUsr01Context"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string of the database.</param>
        public DbUsr01Context(string connectionString)
        {
            _connection = connectionString;
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public List<USR01> GetAllUsers()
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

        /// <summary>
        /// Retrieves a user by their ID from the database.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        public USR01 GetUserById(int id)
        {
            string query = $@"SELECT 
                                    r01f01, 
                                    r01f02, 
                                    r01f03, 
                                    r01f04, 
                                    r01f05 
                              FROM 
                                    usr01 
                              WHERE 
                                    r01f01 = {id};";
            List<USR01> users = GetUsersFromDatabase(query);

            return users.Count > 0 ? users[0] : null;
        }

        /// <summary>
        /// Retrieves users from the database based on the provided query.
        /// </summary>
        /// <param name="query">The SQL query to retrieve users.</param>
        /// <returns>A list of users.</returns>
        private static List<USR01> GetUsersFromDatabase(string query)
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

        /// <summary>
        /// Retrieves users from the provided data reader.
        /// </summary>
        /// <param name="reader">The data reader containing user data.</param>
        /// <returns>A list of users.</returns>
        private static List<USR01> GetUsersFromDataReader(MySqlDataReader reader)
        {
            List<USR01> resultList = new List<USR01>();

            while (reader.Read())
            {
                USR01 objUser = new USR01
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
    }
}

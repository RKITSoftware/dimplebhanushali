using Historical_Events.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Historical_Events.DL
{
    public class DbUsr01Context
    {
        private static string _connection;

        public DbUsr01Context(string connectionString)
        {
            _connection = connectionString;
        }

        public List<usr01> GetAllUsers()
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
            List<usr01> users = GetUsersFromDatabase(query);

            return users.Count > 0 ? users[0] : null;
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

    }
}
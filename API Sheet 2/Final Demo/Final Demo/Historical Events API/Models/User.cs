using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using System.Configuration;

namespace Historical_Events_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }

        /// <summary>
        /// Gets a list of predefined users for validation.
        /// </summary>
        /// <returns>A list of User objects.</returns>
        public static List<User> GetUsers()
        {
            // Predefined list of users for validation
            List<User> lstUsers = new List<User>
            {
                new User { Id = 1,Name="Dimple Mithiya",UserName="dimple",Password="admin123",Roles=  "admin,superadmin" },
                new User { Id = 2,Name="Pankaj Mithiya",UserName="pankaj",Password="user123",Roles= "user" },
                new User { Id = 3,Name="Abc Xyz",UserName="abc",Password="admin123",Roles= "superadmin"},
                new User { Id = 4,Name="Ankit Bhanushali",UserName="ankit",Password="admin123",Roles= "admin,superadmin" },
                new User { Id = 5,Name="Xyz Abc",UserName="xyz",Password="user123",Roles= "user"},
            };

            return lstUsers;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Virtual_Diary.Models;

namespace Virtual_Diary.BL
{
    /// <summary>
    /// BLUser Class for Managing User Operations.
    /// </summary>
    public class BLUser
    {
        #region Public Members 

        // Predefined list of users for validation
        public static List<User> lstUsers = new List<User>
        {
            new User { Id = 1, Name="Dimple Mithiya", UserName="dimple", Password="admin123", Roles= "admin,superadmin" },
            new User { Id = 2, Name="Pankaj Mithiya", UserName="pankaj", Password="user123", Roles= "user" },
            new User { Id = 3, Name="Abc Xyz", UserName="abc", Password="admin123", Roles= "superadmin"},
            new User { Id = 4, Name="Ankit Bhanushali", UserName="ankit", Password="admin123", Roles= "admin,superadmin" },
            new User { Id = 5, Name="Xyz Abc", UserName="xyz", Password="user123", Roles= "user"},
        };

        public static Response response;

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new user and adds it to the list of users.
        /// </summary>
        /// <param name="newUser">The user object to be created.</param>
        /// <returns>A Response object indicating the success or failure of the operation.</returns>
        public static Response CreateUser(User newUser)
        {
            response = new Response();
            try
            {
                newUser.Id = lstUsers.Max(u => u.Id) + 1; // Generate a new ID
                lstUsers.Add(newUser);
                response.IsError = false;
                response.Message = "User created successfully.";
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A Response object containing the user object if found, otherwise an error message.</returns>
        public static Response GetUserById(int id)
        {
            response = new Response();
            try
            {
                User user = lstUsers.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    response.Data = ConvertToDataTable(new List<User> { user });
                    response.IsError = false;
                    response.Message = "User retrieved successfully.";
                }
                else
                {
                    response.IsError = true;
                    response.Message = "User not found.";
                }
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A Response object containing all users.</returns>
        public static Response GetUsers()
        {
            response = new Response();
            try
            {
                // Convert the list of users to a DataTable
                DataTable dataTable = ConvertToDataTable(lstUsers);
                // Set the DataTable to the Data property of the response
                response.Data = dataTable;
                response.IsError = false;
                response.Message = "Users retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Updates an existing user's information.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updatedUser">The updated user object.</param>
        /// <returns>A Response object indicating the success or failure of the operation.</returns>
        public static Response UpdateUser(User updatedUser)
        {
            response = new Response();
            try
            {
                User userToUpdate = lstUsers.FirstOrDefault(u => u.Id == updatedUser.Id);
                if (userToUpdate != null)
                {
                    userToUpdate.Name = updatedUser.Name;
                    userToUpdate.UserName = updatedUser.UserName;
                    userToUpdate.Password = updatedUser.Password;
                    userToUpdate.Roles = updatedUser.Roles;
                    response.IsError = false;
                    response.Message = "User updated successfully.";
                }
                else
                {
                    response.IsError = true;
                    response.Message = "User not found.";
                }
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A Response object indicating the success or failure of the operation.</returns>
        public static Response DeleteUser(int id)
        {
            response = new Response();
            try
            {
                User userToDelete = lstUsers.FirstOrDefault(u => u.Id == id);
                if (userToDelete != null)
                {
                    lstUsers.Remove(userToDelete);
                    response.IsError = false;
                    response.Message = "User deleted successfully.";
                }
                else
                {
                    response.IsError = true;
                    response.Message = "User not found.";
                }
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Converts a list of users into a DataTable.
        /// </summary>
        /// <param name="users">The list of users to convert.</param>
        /// <returns>A DataTable containing the user data.</returns>
        private static DataTable ConvertToDataTable(List<User> users)
        {
            DataTable dataTable = new DataTable();
            // Add columns to DataTable
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("UserName", typeof(string));
            dataTable.Columns.Add("Password", typeof(string));
            dataTable.Columns.Add("Roles", typeof(string));
            // Add rows to DataTable
            foreach (User user in users)
            {
                dataTable.Rows.Add(user.Id, user.Name, user.UserName, user.Password, user.Roles);
            }
            return dataTable;
        }

        #endregion
    }
}

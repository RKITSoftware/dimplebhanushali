using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Virtual_Diary.Helper;
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

        /// <summary>
        /// Instance od response.
        /// </summary>
        public Response response;
        
        /// <summary>
        /// enum Operation.
        /// </summary>
        public enmOperations operation;

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

        #endregion

        #region Private Member
        private User _objUser;
        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A Response object containing the user object if found, otherwise an error message.</returns>
        public Response GetUserById(int id)
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
        public Response GetAllUsers()
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
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A Response object indicating the success or failure of the operation.</returns>
        public Response DeleteUser(int id)
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
        /// PreSave Method for initialising _objUser instance.
        /// </summary>
        /// <param name="objUser">Object of User</param>
        /// <returns>Response.</returns>
        public Response PreSave(User objUser)
        {
            response = new Response();
            _objUser = objUser;
            if (operation == enmOperations.I)
            {
                _objUser.Id = lstUsers.Count + 1;
                _objUser.Roles = "User";
            }
            else if (operation == enmOperations.U)
            {
                if (!lstUsers.Any(u => u.Id == _objUser.Id))
                {
                    response.Message = $"No Record for User with Id => {_objUser.Id} Found";
                }                
            }
            return response;
        }

        /// <summary>
        /// Validate Methods for Validating Duplicate Entry.
        /// </summary>
        /// <returns>Response object with proper validation message.</returns>
        public Response Validate()
        {
            response = new Response();
            if (operation == enmOperations.I)
            {
                if (lstUsers.Any(u => u.UserName == _objUser.UserName))
                {
                    response.IsError = true;
                    response.Message = "User with Same User Name Already Exist.";
                }
            }
            else if (operation == enmOperations.U)
            {
                if (!lstUsers.Any(u => u.UserName == _objUser.UserName))
                {
                    response.IsError = true;
                    response.Message = "User with Same User Name Already Exist.";
                }

            }

            return response;
        }

        /// <summary>
        /// Save Methods for Insert and Update based on Operation.
        /// </summary>
        /// <returns>Response object with Proper Insertion or Updation Message.</returns>
        public Response Save()
        {
            response = new Response();

            if (operation == enmOperations.I)
            {
                lstUsers.Add(_objUser);
                response.Message = enmOperations.I.GetMessage();
            }
            else if (operation == enmOperations.U)
            {
                User objUser = lstUsers.FirstOrDefault(u => u.Id == _objUser.Id);
                if (objUser != null)
                {
                    objUser.Name = _objUser.Name;
                    objUser.Password = _objUser.Password;
                    // Ensure you don't update the UserName if it's not intended to be updated
                    // objUser.UserName = _objUser.UserName;

                    response.Message = enmOperations.U.GetMessage();
                }
            }

            return response;
        }

        #endregion

        #region Private Methods

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

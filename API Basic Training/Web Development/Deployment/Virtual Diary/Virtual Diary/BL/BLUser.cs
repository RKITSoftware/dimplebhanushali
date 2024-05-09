using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Virtual_Diary.Helper;
using Virtual_Diary.Logging;
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

        public static EnumMessage operation;

        public static Response response;

        #endregion

        #region Private Members

        private static User _objUser;

        #endregion

        #region Public Methods

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
        /// Presave on insert
        /// </summary>
        /// <param name="objDiary"> Diary Object </param>
        public static void PreSave(User objUser)
        {
            _objUser = objUser;
            if (operation == EnumMessage.I)
            {
                _objUser.DateCreated = DateTime.Now;
            }
        }

        /// <summary>
        /// Validation that checks for a duplicate entry.
        /// </summary>
        /// <param name="newDiaryEntry">New User to be validated.</param>
        /// <returns>A response indicating whether the entry is valid.</returns>
        public static Response Validate()
        {
            Response response = new Response();
            try
            {
                Logger.LogInfo("Attempting to validate new User for duplicates.");

                var usersResponse = GetUsers();

                if (!usersResponse.IsError)
                {
                    DataTable usersTable = usersResponse.Data;
                    // Convert DataTable to list of User objects
                    List<User> users = ConvertDataTableToList(usersTable);

                    // Check if the new User already exists in the list of users
                    bool isDuplicate = users.Any(user => user.UserName.Equals(_objUser.UserName));

                    if (isDuplicate)
                    {
                        Logger.LogWarn("Duplicate User found.");
                        response.IsError = true;
                        response.Message = "Duplicate User found.";
                    }
                    else
                    {
                        Logger.LogInfo("No duplicate User found. Validation successful.");
                        response.IsError = false;
                        response.Message = "Validation successful.";
                    }
                }
                else
                {
                    // Handle error from GetUsers() method
                    response.IsError = true;
                    response.Message = usersResponse.Message;
                    Logger.LogWarn("Error occurred while retrieving users: " + response.Message);
                }
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message;
                Logger.LogError("Error occurred while validating new User for duplicates.", ex);
            }

            return response;
        }

        public static Response ValidateOnDelete(int id)
        {
            Response response = new Response();
            if (operation == EnumMessage.D)
            {
                Logger.LogInfo("Attempting to validate User for duplicates.");

                // Fetch the user by id from your data source
                Response userToDelete = GetUserById(id);

                if (!userToDelete.IsError)
                {
                    Logger.LogWarn($"User with id {id} found and can be deleted.");
                    DataTable userTable = userToDelete.Data;
                    response.Message = $"User with id {id} found and can be deleted.";
                    response.Data = userTable;
                }
                else
                {
                    // Handle error from GetUserById() method
                    response.IsError = true;
                    response.Message = userToDelete.Message;
                    Logger.LogWarn("Error occurred while retrieving user: " + response.Message);
                }
            }
            return response;
        }



        /// <summary>
        /// Saves the User.
        /// </summary>
        /// <returns>A response indicating the save operation result.</returns>
        public static Response Save()
        {
            Response response = new Response();
            Logger.LogInfo("Attempting to create a new User.");
            if (operation == EnumMessage.I)
            {
                _objUser.Id = lstUsers.Count + 1;
                lstUsers.Add(_objUser);
                response.Message = EnumMessage.I.GetMessage();
                Logger.LogInfo("New User created successfully.");
            }
            else if (operation == EnumMessage.U)
            {
                User userToEdit = lstUsers.FirstOrDefault(d => d.Id == _objUser.Id);

                if (userToEdit == null)
                {
                    Logger.LogWarn($"No User Found With Id => {_objUser.Id}");
                    throw new InvalidOperationException($"No User Found With Id => {_objUser.Id}");
                }

                // Update User details
                userToEdit.Name = _objUser.Name;
                userToEdit.Roles = _objUser.Roles;
                userToEdit.Password = _objUser.Password;

                response.Message = EnumMessage.U.GetMessage();
                Logger.LogInfo($"User with Id {_objUser.Id} edited successfully.");
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

        private static List<User> ConvertDataTableToList(DataTable dataTable)
        {
            List<User> users = new List<User>();
            try
            {
                // Assuming your DataTable structure has columns corresponding to User properties
                foreach (DataRow row in dataTable.Rows)
                {
                    User user = new User
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        UserName = row["Username"].ToString(),
                        Name = row["Name"].ToString(),
                        Password = row["Password"].ToString(),
                        Roles = row["Roles"].ToString()
                    };
                    users.Add(user);
                }
            }
            catch (Exception ex)
            {
                // Handle exception if conversion fails
                Logger.LogError("Error occurred while converting DataTable to list of User objects.", ex);
                // You might want to throw an exception or return an empty list here depending on your requirements
            }
            return users;
        }

        #endregion

    }
}

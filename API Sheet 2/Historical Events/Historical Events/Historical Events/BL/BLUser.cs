using Historical_Events.Data;
using Historical_Events.DL;
using Historical_Events.Helpers;
using Historical_Events.Models;
using Historical_Events.Models.DTO;
using Historical_Events.User_Validation;
using MySql.Data.MySqlClient;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Data;

namespace Historical_Events.BL
{
    /// <summary> 
    /// Business logic class for managing users.
    /// </summary>
    public class BLUser
    {
        #region Private Members
        /// <summary>
        /// Connection String.
        /// </summary>
        private static string _connection;

        /// <summary>
        /// Instance of USR01
        /// </summary>
        private USR01 _objUser;
        #endregion

        #region Public Memebers
        /// <summary>
        /// Instance of Response.
        /// </summary>
        public Response response;

        /// <summary>
        /// Enm Operation.
        /// </summary>
        public enmOperation operation;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for Initialising Connection String.
        /// </summary>
        /// <param name="connectionString"></param>
        public BLUser(string connectionString)
        {
            _connection = connectionString;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Pre-saves user data before saving or updating.
        /// </summary>
        public void PreSave(DTOUSR01 objUser)
        {
            if (_objUser == null)
            {
                _objUser = new USR01();
            }

            objUser.Map(_objUser);
            BLAES _objBlAes = new BLAES();
            _objUser.r01f06 = _objBlAes.Encrypt(_objUser.r01f06);
            _objUser.r01f07 = enmRoles.U.ToString();

            if (operation == enmOperation.I)
            {
                _objUser.r01f08 = DateTime.Now;
            }
            else if (operation == enmOperation.U)
            {
                _objUser.r01f09 = DateTime.Now;
            }
        }

        /// <summary>
        /// Validates user data before saving or updating.
        /// </summary>
        public Response Validate()
        {
            response = new Response();
            bool isDuplicate;

            if (operation == enmOperation.I)
            {
                using (IDbConnection db = MyDbContext.CreateConnection())
                {
                    isDuplicate = db.Exists<USR01>(x => x.r01f03 == _objUser.r01f03 || x.r01f05 == _objUser.r01f05 || x.r01f04 == _objUser.r01f04);
                }

                if (isDuplicate)
                {
                    response.isError = true;
                    response.Message = "Duplicate entry found.";
                }
            }
            if (operation == enmOperation.U)
            {
                bool isUserIdExists;

                using (IDbConnection db = MyDbContext.CreateConnection())
                {
                    // Check if user ID exists in database 
                    isUserIdExists = db.Exists<USR01>(x => x.r01f01 == _objUser.r01f01);

                    isDuplicate = db.Exists<USR01>(x =>
                        (x.r01f03 == _objUser.r01f03 || x.r01f05 == _objUser.r01f05 || x.r01f04 == _objUser.r01f04) &&
                        x.r01f01 != _objUser.r01f01);
                }

                if (!isUserIdExists)
                {
                    response.isError = true;
                    response.Message = "User ID not exists.";
                    return response;
                }

                if (isDuplicate)
                {
                    response.isError = true;
                    response.Message = "Duplicate entry found.";
                }
            }
            return response;
        }

        /// <summary>
        /// Saves user data to the database.
        /// </summary>
        public Response Save()
        {
            response = new Response();

            using (IDbConnection db = MyDbContext.CreateConnection())
            {
                if (operation == enmOperation.I)
                {
                    db.Insert(_objUser);
                    response.Message = enmOperation.I.GetMessage();
                }
                else if (operation == enmOperation.U)
                {
                    db.Update(_objUser);
                    response.Message = enmOperation.U.GetMessage();
                }
            }
            return response;
        }

        /// <summary>
        /// Creates necessary tables in the database.
        /// </summary>
        public void CreateTables()
        {
            using (IDbConnection db = MyDbContext.CreateConnection())
            {
                db.CreateTable<USR01>();
            }
        }

        /// <summary>
        /// Validates if the user record can be deleted.
        /// </summary>
        public Response ValidateOnDelete(int id)
        {
            response = new Response();

            if (operation == enmOperation.D)
            {
                using (IDbConnection db = MyDbContext.CreateConnection())
                {
                    USR01 existingObj = db.SingleById<USR01>(id);

                    if (existingObj == null)
                    {
                        response.isError = true;
                        response.Message = $"No record found with Id => {id}.";
                    }
                    else
                    {
                        response.Message = $"Record with Id => {id} exists and can be deleted.";
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// Logs in a user and generates a JWT token.
        /// </summary>
        public Response LoginUser(string userName, string password)
        {
            response = new Response();
            BLValidateUser blValidateUser = new BLValidateUser();

            // Get user ID after validating credentials
            USR01 objUser = GetCurrentUserIdAndRole(userName, password);

            bool isCredentialCorrect = blValidateUser.IsLogin(userName, password);
            if (!isCredentialCorrect)
            {
                response.isError = true;
                response.Message = "Invalid Credential";
                return response;
            }

            string token = blValidateUser.GenerateJwtToken(userName, objUser.r01f01, objUser.r01f06);

            response.isError = false;
            response.Message = "Token generated";
            response.Data = token;
            return response;
        }

        /// <summary>
        /// Retrieves the current user ID and role based on the provided username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A tuple containing the user ID and role.</returns>
        public USR01 GetCurrentUserIdAndRole(string username, string password)
        {
            BLValidateUser blValidateUser = new BLValidateUser();

            // Check if the credentials are correct
            bool isCredentialCorrect = blValidateUser.IsLogin(username, password);
            if (isCredentialCorrect)
            {
                BLAES _objBlAes = new BLAES();
                password = _objBlAes.Encrypt(password);
                string query = $@"SELECT 
                                        r01f01, 
                                        r01f07 
                                  FROM 
                                        USR01 
                                  WHERE 
                                        r01f03 = '{username}' AND r01f06 = '{password}'";

                using (MySqlConnection connection = new MySqlConnection(_connection))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = reader.GetInt32(0);
                                string role = reader.GetString(1);
                                USR01 user = new USR01 { r01f01 = userId, r01f06 = role };
                                return user;
                            }
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Creates an admin user and updates the role from user to admin based on the provided id.
        /// </summary>
        /// <param name="id">The id of the user to promote to admin.</param>
        public void CreateAdmin(int id)
        {
            string query = $@"UPDATE 
                                        USR01 
                                  SET 
                                        r01f07 = '{enmRoles.A.ToString()}' 
                                  WHERE 
                                        r01f01 = {id};";

            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        public Response GetAllUsers()
        {
            DbUsr01Context dbUsr01Context = new DbUsr01Context(_connection);
            response = new Response
            {
                isError = false,
                Message = "All users are fetched",
                Data = dbUsr01Context.GetAllUsers()
            };
            return response;
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        public Response GetUserById(int id)
        {
            DbUsr01Context dbUsr01Context = new DbUsr01Context(_connection);
            response = new Response
            {
                isError = false,
                Message = "Fetched user by user id",
                Data = dbUsr01Context.GetUserById(id)
            };
            return response;
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        public Response DeleteUser(int id)
        {
            Response response = new Response();
            using (IDbConnection db = MyDbContext.CreateConnection())
            {
                // Deleting the record with the specified id from the database table corresponding to type T
                int rowsAffected = db.DeleteById<USR01>(id);
                response.Message = $"Record with id {id} deleted successfully.";
            }
            return response;
        }

        #endregion
    }
}

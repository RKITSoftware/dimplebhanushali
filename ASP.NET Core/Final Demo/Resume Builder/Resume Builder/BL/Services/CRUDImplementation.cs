using Resume_Builder.BL.Interfaces;
using Resume_Builder.Data;
using Resume_Builder.Helpers;
using Resume_Builder.Models;
using Resume_Builder.Models.POCO;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Resume_Builder.DL.Services
{
    /// <summary>
    /// Implementation of the CRUD service interface.
    /// </summary>
    /// <typeparam name="T">Type of the model class.</typeparam>
    public class CRUDImplementation<T> : ICRUDService<T> where T : class
    {
        /// <summary>
        /// Instance of Response.
        /// </summary>
        public Response response;

        /// <summary>
        /// Instance of T - Type Object.
        /// </summary>
        public T _objT;

        /// <summary>
        /// Enume Message Operation 
        /// </summary>
        public static EnumMessage operation;

        /// <summary>
        /// Instance of Db Connection Factory.
        /// </summary>
        private readonly DbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Instance of HttpContextAccessor for Passing user id from Http Context.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor for CRUDImplementation.
        /// </summary>
        /// <param name="dbConnectionFactory">Database connection factory.</param>
        /// <param name="httpContextAccessor">HTTP context accessor.</param>
        public CRUDImplementation(DbConnectionFactory dbConnectionFactory, IHttpContextAccessor httpContextAccessor)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Retrieves all items.
        /// </summary>
        public Response Get()
        {
            // Initialize response object
            response = new Response();

            // Check if the user is authenticated
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                response.HasError = true;
                response.Message = "Unauthorized";
                return response;
            }

            // Extract user's claims
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "jwt_userId");

            if (userIdClaim == null)
            {
                response.HasError = true;
                response.Message = "User ID claim not found";
                return response;
            }

            // Parse user ID from claim
            int userId;
            if (!int.TryParse(userIdClaim.Value, out userId))
            {
                response.HasError = true;
                response.Message = "Invalid User ID claim";
                return response;
            }

            // Create a database connection using the connection factory
            using (var db = _dbConnectionFactory.CreateConnection())
            {
                // Fetch data from the database table corresponding to type T for the authenticated user
                List<T> data = db.Select<T>(q => Sql.In("UserId", userId));
                response.Data = data;
            }

            return response;
        }

        /// <summary>
        /// Retrieves an item by its ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        public Response Get(int userId)
        {
            // Initialize response object
            response = new Response();

            // Create a database connection using the connection factory
            using (var db = _dbConnectionFactory.CreateConnection())
            {
                // Define the table name dynamically based on the type T
                var tableName = typeof(T).Name;

                // Construct the SQL query string
                var sql = $"SELECT * FROM {tableName} WHERE UserId = @UserId";

                // Execute the query and fetch records
                List<T> entities = db.Select<T>(sql, new { UserId = userId });

                if (entities.Count > 0)
                {
                    response.Data = entities;
                }
                else
                {
                    response.Message = $"No records found for user id {userId}.";
                }
            }
            return response;
        }

        /// <summary>
        /// Saves the object.
        /// </summary>
        public Response Save()
        {
            // Initialize response object
            response = new Response();

            if (operation == EnumMessage.I)
            {
                // Insert operation
                using IDbConnection db = _dbConnectionFactory.CreateConnection();
                db.Insert(_objT);
                response.Message = EnumMessage.I.GetMessage();
            }
            else if (operation == EnumMessage.U)
            {
                // Update operation
                using IDbConnection db = _dbConnectionFactory.CreateConnection();
                db.Update(_objT);
                response.Message = EnumMessage.U.GetMessage();
            }

            return response;
        }

        /// <summary>
        /// Validates the object before saving.
        /// </summary>
        public Response Validate()
        {
            // Initialize response object
            response = new Response();

            try
            {
                // Validate table existence and object uniqueness
                // (Implementation omitted for brevity)
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Validation failed: {ex.Message}";
            }

            return response;
        }

        /// <summary>
        /// Validates before deleting an item.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        public Response ValidateOnDelete(int id)
        {
            // Initialize response object
            response = new Response();

            if (operation == EnumMessage.D)
            {
                try
                {
                    using (var db = _dbConnectionFactory.CreateConnection())
                    {
                        // Check if record exists before deletion
                        // (Implementation omitted for brevity)
                    }
                }
                catch (Exception ex)
                {
                    response.HasError = true;
                    response.Message = $"Validation failed: {ex.Message}";
                }
            }
            else
            {
                response.HasError = true;
                response.Message = $"Validation failed";
            }
            return response;
        }

        /// <summary>
        /// Deletes an item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        public Response Delete(int id)
        {
            // Initialize response object
            response = new Response();

            // Create a database connection using the connection factory
            using (var db = _dbConnectionFactory.CreateConnection())
            {
                // Deleting the record with the specified id from the database table corresponding to type T
                int rowsAffected = db.DeleteById<T>(id);

                if (rowsAffected > 0)
                {
                    response.Message = $"Record with id {id} deleted successfully.";
                }
                else
                {
                    response.Message = $"No record found with id {id} to delete.";
                }
            }

            return response;
        }

        /// <summary>
        /// Performs actions before saving an object.
        /// </summary>
        /// <param name="obj">The object to be saved.</param>
        public void PreSave(object obj)
        {
            if (_objT == null)
            {
                _objT = Activator.CreateInstance<T>();
            }
            obj.Map(_objT);
        }

        /// <summary>
        /// Get user details by user id 
        /// </summary>
        /// <param name="r01f01"> user id </param>
        /// <returns> object of user details </returns>
        object ICRUDService<T>.GetUserDetails(int id)
        {
            // Object of user details 
            object userDetails = null;

            using (var db = _dbConnectionFactory.CreateConnection())
            {
                userDetails = db.SingleById<USR01>(id);
            }

            return userDetails;
        }
    }
}

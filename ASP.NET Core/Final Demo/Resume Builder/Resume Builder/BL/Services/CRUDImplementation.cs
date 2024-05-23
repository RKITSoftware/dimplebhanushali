using Resume_Builder.BL.Interfaces;
using Resume_Builder.Data;
using Resume_Builder.DL.Interfaces;
using Resume_Builder.Helpers;
using Resume_Builder.Models;
using Resume_Builder.Models.POCO;
using ServiceStack.OrmLite;
using System.Data;

namespace Resume_Builder.DL.Services
{
    /// <summary>
    /// Implementation of the CRUD service interface.
    /// </summary>
    /// <typeparam name="T">Type of the model class.</typeparam>
    public class CRUDImplementation<T> : ICRUDService<T> where T : class
    {
        #region Public Members

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
        public static enmMessage operation;

        #endregion

        #region Private Members

        /// <summary>
        /// Instance of Db Connection Factory.
        /// </summary>
        private readonly DbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Instance of HttpContextAccessor for Passing user id from Http Context.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Instance of HttpContextAccessor for Passing user id from Http Context.
        /// </summary>
        private readonly IEmailService _sender;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for CRUDImplementation.
        /// </summary>
        /// <param name="dbConnectionFactory">Database connection factory.</param>
        /// <param name="httpContextAccessor">HTTP context accessor.</param>
        public CRUDImplementation(DbConnectionFactory dbConnectionFactory, 
                                  IHttpContextAccessor httpContextAccessor,
                                  IEmailService sender)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _httpContextAccessor = httpContextAccessor;
            _sender = sender;
        }

        #endregion

        #region Public Methods

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

            if (operation == enmMessage.I)
            {
                // Insert operation
                using IDbConnection db = _dbConnectionFactory.CreateConnection();
                db.Insert(_objT);
                response.Message = enmMessage.I.GetMessage();
            }
            else if (operation == enmMessage.U)
            {
                // Update operation
                using IDbConnection db = _dbConnectionFactory.CreateConnection();
                db.Update(_objT);
                response.Message = enmMessage.U.GetMessage();
            }

            return response;
        }

        /// <summary>
        /// Validates the object before saving.
        /// </summary>
        public Response Validate()
        {
            response = new Response();

            try
            {
                using (var db = _dbConnectionFactory.CreateConnection())
                {
                    // Check if the table exists
                    if (!db.TableExists<T>())
                    {
                        response.HasError = true;
                        response.Message = "Table does not exist.";
                        return response;
                    }
                }

                Type typeObj = _objT.GetType();

                switch (typeObj.Name)
                {
                    case "EDU01":
                        if (_objT != null)
                        {
                            EDU01 eduObj = _objT as EDU01;
                            using (var db = _dbConnectionFactory.CreateConnection())
                            {
                                bool isDuplicate = db.Exists<EDU01>(x => x.U01F04 == eduObj.U01F04);
                                if (isDuplicate)
                                {
                                    response.HasError = true;
                                    response.Message = "Duplicate entry found.";
                                }
                            }
                        }
                        else
                        {
                            response.HasError = true;
                            response.Message = "Object to validate is null.";
                        }
                        break;

                    case "CER01":
                        if (_objT != null)
                        {
                            CER01 cerObj = _objT as CER01;
                            using (var db = _dbConnectionFactory.CreateConnection())
                            {
                                bool isDuplicate = db.Exists<CER01>(x => x.R01F03 == cerObj.R01F03);
                                if (isDuplicate)
                                {
                                    response.HasError = true;
                                    response.Message = "Duplicate entry found.";
                                }
                            }
                        }
                        else
                        {
                            response.HasError = true;
                            response.Message = "Object to validate is null.";
                        }
                        break;

                    case "EXP01":
                        if (_objT != null)
                        {
                            EXP01 expObj = _objT as EXP01;
                            using (var db = _dbConnectionFactory.CreateConnection())
                            {
                                bool isDuplicate = db.Exists<EXP01>(x => x.P01F03 == expObj.P01F03 && x.P01F04 == expObj.P01F04);
                                if (isDuplicate)
                                {
                                    response.HasError = true;
                                    response.Message = "Duplicate entry found.";
                                }
                            }
                        }
                        else
                        {
                            response.HasError = true;
                            response.Message = "Object to validate is null.";
                        }
                        break;

                    case "LAN01":
                        if (_objT != null)
                        {
                            LAN01 expObj = _objT as LAN01;
                            using (var db = _dbConnectionFactory.CreateConnection())
                            {
                                bool isDuplicate = db.Exists<LAN01>(x => x.N01F03 == expObj.N01F03);
                                if (isDuplicate)
                                {
                                    response.HasError = true;
                                    response.Message = "Duplicate entry found.";
                                }
                            }
                        }
                        else
                        {
                            response.HasError = true;
                            response.Message = "Object to validate is null.";
                        }
                        break;

                    case "PRO01":
                        if (_objT != null)
                        {
                            PRO01 proObj = _objT as PRO01;
                            using (var db = _dbConnectionFactory.CreateConnection())
                            {
                                bool isDuplicate = db.Exists<PRO01>(x => x.O01F03 == proObj.O01F03);
                                if (isDuplicate)
                                {
                                    response.HasError = true;
                                    response.Message = "Duplicate entry found.";
                                }
                            }
                        }
                        else
                        {
                            response.HasError = true;
                            response.Message = "Object to validate is null.";
                        }
                        break;

                    case "SKL01":
                        if (_objT != null)
                        {
                            SKL01 sklObj = _objT as SKL01;
                            using (var db = _dbConnectionFactory.CreateConnection())
                            {
                                bool isDuplicate = db.Exists<SKL01>(x => x.L01F03 == sklObj.L01F03);
                                if (isDuplicate)
                                {
                                    response.HasError = true;
                                    response.Message = "Duplicate entry found.";
                                }
                            }
                        }
                        else
                        {
                            response.HasError = true;
                            response.Message = "Object to validate is null.";
                        }
                        break;

                    case "USR01":
                        if (_objT != null)
                        {
                            USR01 usrObj = _objT as USR01;
                            using (var db = _dbConnectionFactory.CreateConnection())
                            {
                                bool isDuplicate = db.Exists<USR01>(x => x.R01F04 == usrObj.R01F04 && x.R01F05 == usrObj.R01F05);
                                if (isDuplicate)
                                {
                                    response.HasError = true;
                                    response.Message = "Duplicate entry found.";
                                }
                            }
                        }
                        else
                        {
                            response.HasError = true;
                            response.Message = "Object to validate is null.";
                        }
                        break;

                    case "RES01":
                        break;

                    default:
                        response.HasError = true;
                        response.Message = "Unknown table type.";
                        break;
                }

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
            response = new Response();

            if (operation == enmMessage.D)
            {
                try
                {
                    using (var db = _dbConnectionFactory.CreateConnection())
                    {
                        T existingObj = db.SingleById<T>(id);

                        if (existingObj == null)
                        {
                            response.HasError = true;
                            response.Message = $"No record found with Id => {id}.";
                        }
                        else
                        {
                            response.Message = $"Record with Id => {id} exists and can be deleted.";
                        }
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

        /// <summary>
        /// Sends an email message to the specified email address.
        /// </summary>
        /// <param name="email">The email address of the recipient.</param>
        /// <param name="message">The message content to be sent.</param>
        public void SendEmail(string email, string message)
        {
            _sender.Send(email, message, null);
        }

        #endregion
    }
}

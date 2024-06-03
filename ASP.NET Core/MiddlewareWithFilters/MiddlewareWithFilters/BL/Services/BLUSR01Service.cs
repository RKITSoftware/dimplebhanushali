using MiddlewareWithFilters.BL.Interfaces;
using MiddlewareWithFilters.Data;
using MiddlewareWithFilters.Helpers;
using MiddlewareWithFilters.Models;
using MiddlewareWithFilters.Models.DTO;
using MiddlewareWithFilters.Models.POCO;
using ServiceStack.OrmLite;
using System.Data;

namespace MiddlewareWithFilters.BL.Services
{
    /// <summary>
    /// Service implementation for managing users.
    /// </summary>
    public class BLUSR01Service : IUSR01Service
    {
        #region Private Members
        /// <summary>
        /// Db Factory Instance.
        /// </summary>
        private readonly DbConnectionFactory _db;

        /// <summary>
        /// USR01 Object.
        /// </summary>
        private USR01 _objUSR01;
        #endregion

        #region Public Members
        /// <summary>
        /// Enum Operation Property.
        /// </summary>
        public enmOperation Operation { get; set; }

        /// <summary>
        /// Instance of Response.
        /// </summary>
        public Response response;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for Initialising Db Factory Instance.
        /// </summary>
        /// <param name="dbFactory">DbConnectionFactory</param>
        public BLUSR01Service(DbConnectionFactory dbFactory)
        {
            _db = dbFactory;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A response containing the list of users.</returns>
        public Response Get()
        {
            response = new Response();

            using (IDbConnection db = _db.CreateConnection())
            {
                response.Data = db.Select<USR01>();
            }
            return response;
        }

        /// <summary>
        /// Retrieves a user by ID from the database.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A response containing the user information.</returns>
        public Response Get(int id)
        {
            // Initialize response object
            response = new Response();

            // Create a database connection using the connection factory
            using (IDbConnection db = _db.CreateConnection())
            {
                // Construct the SQL query string
                string sql = $"SELECT * FROM USR01 WHERE R01F01 = @UserId";

                // Execute the query and fetch records
                List<USR01> entities = db.Select<USR01>(sql, new { UserId = id });

                if (entities.Count > 0)
                {
                    response.Data = entities;
                }
                else
                {
                    response.Message = $"No records found for user id {id}.";
                }
            }
            return response;
        }

        /// <summary>
        /// Prepares a user object for saving.
        /// </summary>
        /// <param name="objUSR">The user object to save.</param>
        public void PreSave(DTOUSR01 objUSR)
        {
            _objUSR01 = new USR01();
            objUSR.Map(_objUSR01);

            if (Operation == enmOperation.I)
            {
                _objUSR01.R01F07 = DateTime.Now;
            }
            else if (Operation == enmOperation.U)
            {
                _objUSR01.R01F08 = DateTime.Now;
            }
        }

        /// <summary>
        /// Saves the user data to the database.
        /// </summary>
        /// <returns>A response containing the result of the operation.</returns>
        public Response Save()
        {
            response = new Response();

            using (IDbConnection db = _db.CreateConnection())
            {
                if (Operation == enmOperation.I)
                {
                    db.Insert(_objUSR01);
                    response.Message = enmOperation.I.GetMessage();
                }
                else if (Operation == enmOperation.U)
                {
                    db.Update(_objUSR01);
                    response.Message = enmOperation.U.GetMessage();
                }
            }

            return response;
        }

        /// <summary>
        /// Validates user data.
        /// </summary>
        /// <returns>A response containing the validation result.</returns>
        public Response Validate()
        {
            var response = new Response();

            if (Operation == enmOperation.I)
            {
                using (IDbConnection db = _db.CreateConnection())
                {
                    bool isDuplicate = db.Exists<USR01>(x => x.R01F04 == _objUSR01.R01F04 ||
                                                        x.R01F06 == _objUSR01.R01F06);
                    if (isDuplicate)
                    {
                        response.HasError = true;
                        response.Message = "Duplicate entry found.";
                    }
                }
            }
            else if (Operation == enmOperation.U)
            {
                using (IDbConnection db = _db.CreateConnection())
                {
                    bool isDuplicate = db.Exists<USR01>(x => (x.R01F04 == _objUSR01.R01F04 ||
                                                        x.R01F06 == _objUSR01.R01F06) &&
                                                        x.R01F01 != _objUSR01.R01F01);
                    if (isDuplicate)
                    {
                        response.HasError = true;
                        response.Message = "Duplicate entry found.";
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// Validates user data before deletion.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A response containing the validation result.</returns>
        public Response ValidateOnDelete(int id)
        {
            response = new Response();

            if (Operation == enmOperation.D)
            {
                using (IDbConnection db = _db.CreateConnection())
                {
                    bool isExist = db.Exists<USR01>(x => x.R01F01 == id);
                    if (isExist)
                    {
                        response.Message = "Record with Id Exist and can be Deleted !!! ";
                    }
                }

                _objUSR01 = new USR01();
                _objUSR01.R01F01 = id;
            }

            return response;
        }

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <returns>A response containing the result of the operation.</returns>
        public Response Delete()
        {
            response = new Response();

            using (IDbConnection db = _db.CreateConnection())
            {
                db.DeleteById<USR01>(_objUSR01.R01F01);
            }
            response.Message = enmOperation.D.GetMessage();
            return response;
        }

        #endregion
    }
}
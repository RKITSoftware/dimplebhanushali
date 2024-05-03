using Certificate_Generator.Data;
using Certificate_Generator.Helpers;
using Certificate_Generator.Models;
using Certificate_Generator.Models.DTO;
using Certificate_Generator.Models.POCO;
using ServiceStack.OrmLite;
using System.Data;

namespace Certificate_Generator.BL
{
    /// <summary>
    /// BLUSR01 Handler for Handling User Operations.
    /// </summary>
    public class BLUSR01Handler
    {
        #region Private Members
        // Instance of DbConnection Factory
        private readonly DbConnectionFactory _dbConnectionFactory;
        
        // Instance of USR01
        private USR01 _objUser;
        #endregion

        #region Public Members
        // Instance of EnumMessage
        public EnumMessage operation;

        // Instance of Response
        public Response response;
        #endregion

        #region Constructor
        
        /// <summary>
        /// Constructor for Initialising DBConnectionFactory.
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        public BLUSR01Handler(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        #endregion

        #region Public Methods
        
        /// <summary>
        /// GetAllUSR01Async Method for Retriving all Users.
        /// </summary>
        /// <returns>Task Response With Users</returns>
        public async Task<Response> GetAllUSR01Async()
        {
            response = new Response();
            using IDbConnection db = _dbConnectionFactory.CreateConnection();

            // Construct SQL query
            var sql = "SELECT * FROM USR01";

            // Execute SQL query and fetch results asynchronously
            List<USR01> users = await db.SqlListAsync<USR01>(sql);
            response.Data = users;
            return response;

        }

        /// <summary>
        /// GetUSR01ByIdAsync Method for Retriving Single user By id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Response with Perticular User having user Id</returns>
        public Response GetUSR01ByIdAsync(int id)
        {
            response = new Response();

            try
            {
                using IDbConnection db = _dbConnectionFactory.CreateConnection();

                // Execute SQL query with parameter and fetch result asynchronously
                USR01 user = db.Single<USR01>(x => x.R01F01 == id);

                // Check if user is found
                if (user != null)
                {
                    response.Data = user;
                }
                else
                {
                    response.HasError = true;
                    response.Message = "User not found.";
                }
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Failed to retrieve user: {ex.Message}";
            }

            return response;
        }


       /// <summary>
       /// CreateUSR01 Method for Adding User in DB
       /// </summary>
       /// <param name="user">USR01 Model Object</param>
       /// <returns>Response with Proper Message</returns>
        public Response CreateUSR01(USR01 user)
        {
            response = new Response();

            try
            {
                using IDbConnection db = _dbConnectionFactory.CreateConnection();
                db.Insert(user);
                response.Message = EnumMessage.I.GetMessage();
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Failed to create user: {ex.Message}";
            }

            return response;
        }

        /// <summary>
        /// UpdateUSR01 Method for Updating USer details.
        /// </summary>
        /// <param name="user">USR01 Model Object</param>
        /// <returns>Response with Proper Response MEssages.</returns>
        public Response UpdateUSR01(USR01 user)
        {
            response = new Response();

            try
            {
                using IDbConnection db = _dbConnectionFactory.CreateConnection();
                db.Update(user);
                response.Message = EnumMessage.U.GetMessage();
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Failed to update user: {ex.Message}";
            }

            return response;
        }

        /// <summary>
        /// DeleteUSR01 Method For Removing User From DB.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Response with Proper Messages</returns>
        public Response DeleteUSR01(int id)
        {
            response = new Response();

            try
            {
                using IDbConnection db = _dbConnectionFactory.CreateConnection();
                db.DeleteById<USR01>(id);
                response.Message = EnumMessage.D.GetMessage();
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = $"Failed to delete user: {ex.Message}";
            }

            return response;
        }

        /// <summary>
        /// Create Table Method for Creating Tables With ORM
        /// </summary>
        public void CreateTables()
        {
            using (IDbConnection db = _dbConnectionFactory.CreateConnection())
            {
                db.CreateTableIfNotExists<USR01>();
                db.CreateTableIfNotExists<CER01>();
                db.CreateTableIfNotExists<DAT01>();
                db.CreateTableIfNotExists<GEN01>();
                db.CreateTableIfNotExists<BJB01>();
                db.CreateTableIfNotExists<BCD01>();
            }
        }

        /// <summary>
        /// PreSave Method For Mapping Object From DTO to POCO.
        /// </summary>
        /// <param name="dtoUsr">DTOUSR01 Object Model</param>
        public void PreSave(DTOUSR01 dtoUsr)
        {
            // Initialize _objUser if it's null
            if (_objUser == null)
            {
                _objUser = new USR01();
            }

            dtoUsr.Map(_objUser);

            if (operation == EnumMessage.I)
            {
                _objUser.R01F06 = DateTime.Now;
            }
            else if (operation == EnumMessage.U)
            {
                _objUser.R01F07 = DateTime.Now;
            }
        }

        /// <summary>
        /// Validate Method
        /// </summary>
        /// <returns>Response With Proper Message.</returns>
        public Response Validate()
        {
            response = new Response();

            try
            {
                using IDbConnection db = _dbConnectionFactory.CreateConnection();

                // Check if the database and table exist
                if (!db.TableExists<USR01>())
                {
                    response.HasError = true;
                    response.Message = "USR01 table does not exist.";
                    return response;
                }

                if (operation == EnumMessage.I)
                {
                    // Check if the record to be inserted already exists
                    USR01 existingUser = db.Single<USR01>(x => x.R01F02 == _objUser.R01F02 || x.R01F03 == _objUser.R01F03);
                    if (existingUser != null)
                    {
                        response.HasError = true;
                        response.Message = "User with the same name or email already exists.";
                        return response;
                    }
                }
                else if (operation == EnumMessage.U)
                {
                    // Check if the record to be updated exists
                    USR01 existingUser = db.SingleById<USR01>(_objUser.R01F01);
                    if (existingUser == null)
                    {
                        response.HasError = true;
                        response.Message = "User does not exist.";
                        return response;
                    }

                    // Check for duplicate records based on both name and email if updating
                    USR01 duplicateUser = db.Single<USR01>(x =>
                        (x.R01F02 == _objUser.R01F02 || x.R01F03 == _objUser.R01F03) &&
                        x.R01F01 != _objUser.R01F01);

                    if (duplicateUser != null)
                    {
                        response.HasError = true;
                        response.Message = "Another user with the same name or email already exists.";
                        return response;
                    }
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
        /// Save Method For Adding and Updating User
        /// </summary>
        /// <returns>Response with Proper Message</returns>
        public Response Save()
        {
            response = new Response();

            if (operation == EnumMessage.I)
            {
                using IDbConnection db = _dbConnectionFactory.CreateConnection();
                db.Insert(_objUser);
                response.Message = EnumMessage.I.GetMessage();
            }
            else if (operation == EnumMessage.U)
            {
                using IDbConnection db = _dbConnectionFactory.CreateConnection();
                db.Update(_objUser);
                response.Message = EnumMessage.U.GetMessage();
            }

            return response;
        }

        /// <summary>
        /// Validate on Delete Method.
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns>Response With Proper Message</returns>
        public Response ValidateOnDelete(int id)
        {
            response = new Response();

            if (operation == EnumMessage.D)
            {
                using IDbConnection db = _dbConnectionFactory.CreateConnection();
                USR01 existingUser = db.Single<USR01>(x => x.R01F01 == id);

                if (existingUser == null)
                {
                    response.HasError = true;
                    response.Message = "User with the same name or email already exists.";
                    return response;
                }
            }
            response.Message = $"User with Id => {id} exist and can be deleted";
            return response;
        }

        #endregion

    }
}

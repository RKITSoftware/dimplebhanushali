using Historical_Events.Data;
using Historical_Events.DL;
using Historical_Events.Helpers;
using Historical_Events.Models;
using Historical_Events.Models.DTO;
using ServiceStack.OrmLite;
using System.Data;

namespace Historical_Events.BL
{
    /// <summary>
    /// Class For Managing Historical Events.
    /// </summary>
    public class BLHistory
    {
        #region Private Member

        /// <summary>
        /// Connection String for DB.
        /// </summary>
        private readonly string _connection;

        /// <summary>
        /// Instance of DbHST01Context.
        /// </summary>
        private DbHst01Context _dbHst01Context;

        /// <summary>
        /// POCO Model
        /// </summary>
        private HSTEVT01 _objHstEvt01;

        #endregion

        #region Public Members

        /// <summary>
        /// Instance of Response Model.
        /// </summary>
        public Response response;

        /// <summary>
        /// Enum Operation instance for handling Insert, Update, Delete.
        /// </summary>
        public enmOperation enmOperation;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for initialisng Connection String.
        /// </summary>
        /// <param name="connectionString">Connection String of Db</param>
        public BLHistory(string connectionString)
        {
            _connection = connectionString;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Presaves DTO to entity object.
        /// </summary>
        /// <param name="objDTOHstEvt01">DTOHstEvt01 object</param>
        public void Presave(DTOHstEvt01 objDTOHstEvt01)
        {
            _objHstEvt01 = new HSTEVT01();
            objDTOHstEvt01.Map(_objHstEvt01);
        }

        /// <summary>
        /// Validates if the table exists in the database.
        /// </summary>
        /// <returns>Response indicating the validation result.</returns>
        public Response Validate()
        {
            response = new Response();
            using (IDbConnection db = MyDbContext.CreateConnection())
            {
                if (!db.TableExists<HSTEVT01>())
                {
                    response.isError = true;
                    response.Message = "Table Does not Exist.";
                }
            }
            return response;
        }

        /// <summary>
        /// Validates the deletion of a record based on ID.
        /// </summary>
        /// <param name="id">ID of the record to be deleted</param>
        /// <returns>Response indicating the validation result.</returns>
        public Response ValidateOnDelete(int id)
        {
            response = new Response();
            if (enmOperation == enmOperation.D)
            {
                bool isIdExists;
                using (IDbConnection db = MyDbContext.CreateConnection())
                {
                    isIdExists = db.Exists<HSTEVT01>(hst => hst.t01f01 == id);
                }

                if (isIdExists)
                {
                    response.isError = false;
                    response.Message = "Id exists in database";
                    return response;
                }
                else
                {
                    response.isError = true;
                    response.Message = "No such id exists in database";
                    return response;
                }
            }

            response.isError = true;
            response.Message = "Internal server error";
            return response;
        }

        /// <summary>
        /// Saves or updates a record based on the operation type.
        /// </summary>
        /// <returns>Response indicating the result of the operation.</returns>
        public Response Save()
        {
            response = new Response();
            if (enmOperation == enmOperation.I)
            {
                using (IDbConnection db = MyDbContext.CreateConnection())
                {
                    db.Insert<HSTEVT01>(_objHstEvt01);
                }

                response.isError = false;
                response.Message = enmOperation.I.GetMessage();
                return response;
            }
            else if (enmOperation == enmOperation.U)
            {
                using (IDbConnection db = MyDbContext.CreateConnection())
                {
                    db.Update<HSTEVT01>(_objHstEvt01);
                }

                response.isError = false;
                response.Message = enmOperation.U.GetMessage();
                return response;
            }

            response.isError = true;
            response.Message = "Internal server error";
            return response;
        }

        /// <summary>
        /// Deletes a record based on ID.
        /// </summary>
        /// <param name="id">ID of the record to be deleted.</param>
        /// <returns>Response indicating the result of the deletion.</returns>
        public Response Delete(int id)
        {
            response = new Response();
            if (enmOperation == enmOperation.D)
            {
                using (IDbConnection db = MyDbContext.CreateConnection())
                {
                    db.DeleteById<HSTEVT01>(id);
                }

                response.isError = false;
                response.Message = enmOperation.D.GetMessage();
                return response;
            }

            response.isError = true;
            response.Message = "Not Deleted";
            return response;
        }

        /// <summary>
        /// Gets all events from the database.
        /// </summary>
        /// <returns>Response containing the list of events.</returns>
        public Response GetAllEvents(int pageNumber)
        {
            _dbHst01Context = new DbHst01Context(_connection);

            response = new Response
            {
                isError = false,
                Message = "Fetched all events",
                Data = _dbHst01Context.GetAllEvents(pageNumber),
            };

            return response;
        }

        /// <summary>
        /// Gets event by id from the database.
        /// </summary>
        /// <returns>Response containing the event`.</returns>
        public Response GetEventById(int id)
        {
            _dbHst01Context = new DbHst01Context(_connection);

            response = new Response
            {
                isError = false,
                Message = "Fetched event by id",
                Data = _dbHst01Context.GetEventById(id),
            };

            return response;
        }

        /// <summary>
        /// Gets events from the database.
        /// </summary>
        /// <returns>Response containing the list of events.</returns>
        public Response SearchEvents(int pageNumber, int? startYear, int? endYear, string startDate, string endDate, string keyword)
        {
            _dbHst01Context = new DbHst01Context(_connection);

            response = new Response
            {
                isError = false,
                Message = "List of event is fetched",
                Data = _dbHst01Context.SearchEvents(pageNumber, startYear, endYear, startDate, endDate, keyword)
            };

            return response;
        }

        /// <summary>
        /// Gets events for today from the database.
        /// </summary>
        /// <returns>Response containing the list of events.</returns>
        public Response GetTodaysEvents(int pageNumber)
        {
            _dbHst01Context = new DbHst01Context(_connection);

            response = new Response
            {
                isError = false,
                Message = "Events Fetched",
                Data = _dbHst01Context.GetEventsForToday(pageNumber)
            };

            return response;
        }

        /// <summary>
        /// Gets events from the database.
        /// </summary>
        /// <returns>Response containing the list of events.</returns>
        public Response GetEventsByCategory(string category)
        {
            _dbHst01Context = new DbHst01Context(_connection);

            response = new Response
            {
                isError = false,
                Message = "List of event is fetched is based on category",
                Data = _dbHst01Context.GetEventsByCategory(category)
            };

            return response;
        }

        /// <summary>
        /// Gets Latest events from the database.
        /// </summary>
        /// <returns>Response containing the list of events.</returns>
        public Response GetLatestEvents(int count)
        {
            _dbHst01Context = new DbHst01Context(_connection);

            response = new Response
            {
                isError = false,
                Message = "Get latest events",
                Data = _dbHst01Context.GetLatestEvents(count)
            };
            return response;
        }

        /// <summary>
        /// Gets events by Date Range `from the database.
        /// </summary>
        /// <returns>Response containing the list of events.</returns>
        public Response GetEventsByDateRange(string startDate, string endDate)
        {
            _dbHst01Context = new DbHst01Context(_connection);

            response = new Response
            {
                isError = false,
                Message = "Fetched events based on date",
                Data = _dbHst01Context.GetEventsByDateRange(startDate, endDate)
            };
            return response;
        }

        /// <summary>
        /// Gets events from the database Containing specific Keyword..
        /// </summary>
        /// <returns>Response containing the list of events.</returns>
        public Response GetEventsByKeyword(string keyword)
        {
            _dbHst01Context = new DbHst01Context(_connection);

            response = new Response
            {
                isError = false,
                Message = "Fetched events by keyword",
                Data = _dbHst01Context.GetEventsByKeyword(keyword)
            };
            return response;
        }

        /// <summary>
        /// Gets unique Categories from the database.
        /// </summary>
        /// <returns>Response containing the list of Categories.</returns>
        public Response GetUniqueCategories()
        {
            response = new Response
            {
                isError = false,
                Message = "Unique categories",
                Data = _dbHst01Context.GetUniqueCategories()
            };
            return response;
        }


        #endregion
    }
}
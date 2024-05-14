using Historical_Events.Data;
using Historical_Events.DL;
using Historical_Events.Helpers;
using Historical_Events.Models;
using Historical_Events.Models.DTO;
using ServiceStack.OrmLite;
using System.Data;

namespace Historical_Events.BL
{
    public class BLHistory
    {
        #region Private Member

        private readonly string _connection;

        private DbHst01Context _dbHst01Context;

        /// <summary>
        /// POCO Model
        /// </summary>
        private hstevt01 _objHstEvt01;
        
        #endregion

        #region Public Members
        
        public Response response;

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

        public void Presave(DTOHstEvt01 objDTOHstEvt01)
        {
            _objHstEvt01 = new hstevt01();
            objDTOHstEvt01.Map(_objHstEvt01);
        }

        public Response Validate()
        {
            response = new Response();
            if(enmOperation == enmOperation.I)
            {

            }
            else if(enmOperation == enmOperation.U)
            {

            }
            return response;
        }

        public Response ValidateOnDelete(int id)
        {
            response = new Response();
            if (enmOperation == enmOperation.D)
            {
                bool isIdExists;
                using (IDbConnection db = MyDbContext.CreateConnection())
                {
                    isIdExists = db.Exists<hstevt01>(hst => hst.t01f01 == id);
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

        public Response Save()
        {
            response = new Response();
            if (enmOperation == enmOperation.I)
            {
                using (IDbConnection db = MyDbContext.CreateConnection())
                {
                    db.Insert<hstevt01>(_objHstEvt01);
                }

                response.isError = false;
                response.Message = "Inserted Successfully";
                return response;
            }
            else if (enmOperation == enmOperation.U)
            {
                using (IDbConnection db = MyDbContext.CreateConnection())
                {
                    db.Update<hstevt01>(_objHstEvt01);
                }

                response.isError = false;
                response.Message = "Updated Successfully";
                return response;
            }

            response.isError = true;
            response.Message = "Internal server error";
            return response;
        }

        public Response Delete(int id)
        {
            response = new Response();  
            if(enmOperation == enmOperation.D)
            {
                using (IDbConnection db = MyDbContext.CreateConnection())
                {
                    db.DeleteById<hstevt01>(id);
                }

                response.isError = false;
                response.Message = "Deleted Successfully";
                return response;
            }

            response.isError = true;
            response.Message = "Not Deleted";
            return response;
        }

        //public Response CreateHistoricalEvent(hstevt01 newEvent)
        //{
        //    _dbHst01Context = new DbHst01Context(_connection);
        //    response = _dbHst01Context.CreateHistoricalEvent(newEvent);
        //    return response;
        //}

        //public Response EditHistoricalEvent(int id, hstevt01 updatedEvent)
        //{
        //    _dbHst01Context = new DbHst01Context(_connection);
        //    response = _dbHst01Context.EditHistoricalEvent(id, updatedEvent);
        //    return response;
        //}

        //public Response DeleteHistoricalEvent(int id)
        //{
        //    _dbHst01Context = new DbHst01Context(null);
        //    response = _dbHst01Context.DeleteHistoricalEvent(id);
        //    return response;
        //}

        public Response GetAllEvents()
        {
            _dbHst01Context = new DbHst01Context(_connection);

            response = new Response
            {
                isError = false,
                Message = "Fetched all events",
                Data = _dbHst01Context.GetAllEvents(),
            };

            return response;
        }

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

        public Response SearchEvents(int? startYear, int? endYear, string startDate, string endDate, string keyword)
        {
            _dbHst01Context = new DbHst01Context(_connection);

            response = new Response
            {
                isError = false,
                Message = "List of event is fetched",
                Data = _dbHst01Context.SearchEvents(startYear, endYear, startDate, endDate, keyword)
            };

            return response;
        }

        public Response GetEventsByCategory(string category)
        {
            _dbHst01Context = new DbHst01Context (_connection);

            response = new Response
            {
                isError = false,
                Message = "List of event is fetched is based on category",
                Data = _dbHst01Context.GetEventsByCategory(category)
            };

            return response;
        }

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
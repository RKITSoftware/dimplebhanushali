using Historical_Events.BL;
using Historical_Events.Models;
using Historical_Events.Models.DTO;
using System.Configuration;
using System.Web.Http;

namespace Historical_Events.Controllers
{
    /// <summary>
    /// Controller for managing historical events.
    /// </summary>
    [RoutePrefix("api/History")]
    public class CLHistoryController : ApiController
    {
        #region Private Member
        /// <summary>
        ///  Instance of Business Logic for Historical Events manager.
        /// </summary>
        private readonly BLHistory _blHistory;
        #endregion

        #region Public Member
        /// <summary>
        /// Initializes a new instance of the <see cref="CLHistoryController"/> class.
        /// </summary>
        public Response response;
        #endregion

        #region Construcotr
        public CLHistoryController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            _blHistory = new BLHistory(connectionString);
        }
        #endregion

        /// <summary>
        /// Retrieves all historical events.
        /// </summary>
        [HttpGet, Route("GetAll")]
        public IHttpActionResult GetAll(int pageNumber)
        {
            response = _blHistory.GetAllEvents(pageNumber);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves all historical events of Today.
        /// </summary>
        [HttpGet, Route("GetTodaysEvents")]
        public IHttpActionResult GetAllForToday(int pageNumber)
        {
            response = _blHistory.GetTodaysEvents(pageNumber);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a historical event by its ID.
        /// </summary>
        [HttpGet, Route("GetById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            response = _blHistory.GetEventById(id);
            return Ok(response);
        }

        /// <summary>
        /// Searches for historical events based on various criteria.
        /// </summary>
        [HttpGet, Route("Search")]
        public IHttpActionResult Search(int pageNumber,int? startYear = null, int? endYear = null, string startDate = null, string endDate = null, string keyword = null)
        {
            response = _blHistory.SearchEvents(pageNumber, startYear, endYear, startDate, endDate, keyword);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves historical events by category.
        /// </summary>
        [HttpGet, Route("GetByCategory/{category}")]
        public IHttpActionResult GetByCategory(string category)
        {
            response = _blHistory.GetEventsByCategory(category);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves the latest historical events.
        /// </summary>
        [HttpGet, Route("GetLatest/{count}")]
        public IHttpActionResult GetLatest(int count)
        {
            response = _blHistory.GetLatestEvents(count);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves historical events within a specified date range.
        /// </summary>
        [HttpGet, Route("GetByDateRange/{startDate}/{endDate}")]
        public IHttpActionResult GetByDateRange(string startDate, string endDate)
        {
            response = _blHistory.GetEventsByDateRange(startDate, endDate);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves historical events based on a keyword.
        /// </summary>
        [HttpGet, Route("GetByKeyword/{keyword}")]
        public IHttpActionResult GetByKeyword(string keyword)
        {
            response = _blHistory.GetEventsByKeyword(keyword);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves unique categories of historical events.
        /// </summary>
        [HttpGet, Route("GetUniqueCategories")]
        public IHttpActionResult GetUniqueCategories()
        {
            response = _blHistory.GetUniqueCategories();
            return Ok(response);
        }
    }
}

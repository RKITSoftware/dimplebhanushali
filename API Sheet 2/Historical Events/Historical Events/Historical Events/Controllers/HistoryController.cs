using Historical_Events.Basic_Authorisation;
using Historical_Events.BL;
using Historical_Events.Models;
using Historical_Events.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

namespace Historical_Events.Controllers
{
    [RoutePrefix("api/History")]
    public class HistoryController : ApiController
    {
        private readonly BLHistory _blHistory;

        public Response response;
        public HistoryController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            _blHistory = new BLHistory(connectionString);
        }

        [HttpGet, Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            response = _blHistory.GetAllEvents();
            return Ok(response);
        }

        [HttpGet, Route("GetById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            response = _blHistory.GetEventById(id);
            return Ok(response);
        }

        [HttpGet, Route("Search")]
        public IHttpActionResult Search(int? startYear = null, int? endYear = null, string startDate = null, string endDate = null, string keyword = null)
        {
            response = _blHistory.SearchEvents(startYear, endYear, startDate, endDate, keyword);
            return Ok(response);
        }

        [HttpGet, Route("GetByCategory/{category}")]
        public IHttpActionResult GetByCategory(string category)
        {
            response = _blHistory.GetEventsByCategory(category);
            return Ok(response);
        }

        [HttpGet, Route("GetLatest/{count}")]
        public IHttpActionResult GetLatest(int count)
        {
            response = _blHistory.GetLatestEvents(count);
            return Ok(response);
        }

        [HttpGet, Route("GetByDateRange/{startDate}/{endDate}")]
        public IHttpActionResult GetByDateRange(string startDate, string endDate)
        {
            response = _blHistory.GetEventsByDateRange(startDate, endDate);
            return Ok(response);
        }

        [HttpGet, Route("GetByKeyword/{keyword}")]
        public IHttpActionResult GetByKeyword(string keyword)
        {
            response = _blHistory.GetEventsByKeyword(keyword);
            return Ok(response);
        }

        [HttpGet, Route("GetUniqueCategories")]
        public IHttpActionResult GetUniqueCategories()
        {
            response = _blHistory.GetUniqueCategories();
            return Ok(response);
        }

        [HttpPost, Route("Create")]
        [BasicAuthentication]
        [BasicAuthorisation(Roles = "admin")]
        public IHttpActionResult CreateHistoricalEvent(DTOHstEvt01 objDTOHstEvt01)
        {
            _blHistory.enmOperation = Helpers.enmOperation.I;

            _blHistory.Presave(objDTOHstEvt01);

            response = _blHistory.Validate();
            if (response.isError)
            {
                return Ok(response);
            }

            response = _blHistory.Save();
            return Ok(response);
        }

        [HttpPut, Route("Edit/{id}")]
        [BasicAuthentication]
        [BasicAuthorisation(Roles = "admin")]
        public IHttpActionResult EditHistoricalEvent(DTOHstEvt01 objDTOHstEvt01)
        {
            _blHistory.enmOperation = Helpers.enmOperation.U;

            _blHistory.Presave(objDTOHstEvt01);

            response = _blHistory.Validate();
            if (response.isError)
            {
                return Ok(response);
            }

            response = _blHistory.Save();
            return Ok(response);
        }

        [HttpDelete, Route("Delete/{id}")]
        [BasicAuthentication]
        [BasicAuthorisation(Roles = "admin")]
        public IHttpActionResult DeleteHistoricalEvent(int id)
        {
            response = _blHistory.ValidateOnDelete(id);
            if (response.isError)
            {
                return Ok(response);
            }

            response = _blHistory.Delete(id);
            return Ok(response);
        }
    }
}

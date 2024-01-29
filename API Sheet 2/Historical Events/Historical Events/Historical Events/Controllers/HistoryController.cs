using Historical_Events.Basic_Authorisation;
using Historical_Events.BL;
using Historical_Events.Models;
using MySql.Data.MySqlClient;
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

        public HistoryController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            _blHistory = new BLHistory(connectionString);
        }

        [HttpGet, Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            try
            {
                List<HistoricalEvent> resultList = _blHistory.GetAllEvents();
                return Ok(resultList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("GetById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                HistoricalEvent result = _blHistory.GetEventById(id);

                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("Search")]
        public IHttpActionResult Search(int? startYear = null, int? endYear = null, string startDate = null, string endDate = null, string keyword = null)
        {
            try
            {
                List<HistoricalEvent> resultList = _blHistory.SearchEvents(startYear, endYear, startDate, endDate, keyword);
                return Ok(resultList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("GetByCategory/{category}")]
        public IHttpActionResult GetByCategory(string category)
        {
            try
            {
                List<HistoricalEvent> resultList = _blHistory.GetEventsByCategory(category);
                return Ok(resultList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("GetLatest/{count}")]
        public IHttpActionResult GetLatest(int count)
        {
            try
            {
                List<HistoricalEvent> resultList = _blHistory.GetLatestEvents(count);
                return Ok(resultList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("GetByDateRange/{startDate}/{endDate}")]
        public IHttpActionResult GetByDateRange(string startDate, string endDate)
        {
            try
            {
                List<HistoricalEvent> resultList = _blHistory.GetEventsByDateRange(startDate, endDate);
                return Ok(resultList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("GetByKeyword/{keyword}")]
        public IHttpActionResult GetByKeyword(string keyword)
        {
            try
            {
                List<HistoricalEvent> resultList = _blHistory.GetEventsByKeyword(keyword);
                return Ok(resultList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("GetUniqueCategories")]
        public IHttpActionResult GetUniqueCategories()
        {
            try
            {
                List<string> categories = _blHistory.GetUniqueCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost, Route("Create")]
        [BasicAuthentication]
        [BasicAuthorisation(Roles = "admin")]
        public IHttpActionResult CreateHistoricalEvent(HistoricalEvent newEvent)
        {
            try
            {
                _blHistory.CreateHistoricalEvent(newEvent);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut, Route("Edit/{id}")]
        [BasicAuthentication]
        [BasicAuthorisation(Roles = "admin")]
        public IHttpActionResult EditHistoricalEvent(int id, HistoricalEvent updatedEvent)
        {
            try
            {
                _blHistory.EditHistoricalEvent(id, updatedEvent);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete, Route("Delete/{id}")]
        [BasicAuthentication]
        [BasicAuthorisation(Roles = "admin")]
        public IHttpActionResult DeleteHistoricalEvent(int id)
        {
            try
            {
                _blHistory.DeleteHistoricalEvent(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

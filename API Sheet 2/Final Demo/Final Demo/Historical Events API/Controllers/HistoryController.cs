using Historical_Events_API.BasicAuth;
using Historical_Events_API.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Http;

namespace Historical_Events_API.Controllers
{
    [RoutePrefix("api/History")]
    public class HistoryController : ApiController
    {
        private string _connection = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        [HttpGet, Route("GetAll")]
        [AllowAnonymous]
        public IHttpActionResult GetAll(int? page = 1, int? pageSize = 10, string sortBy = "date", string sortOrder = "asc")
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connection))
                {
                    connection.Open();

                    // Construct the base query
                    string query = "SELECT * FROM historicalevent WHERE 1 = 1";

                    // Append conditions based on sorting parameters
                    query += $" ORDER BY {sortBy} {sortOrder}";

                    // Append conditions based on pagination parameters
                    int offset = (page.GetValueOrDefault() - 1) * pageSize.GetValueOrDefault();
                    query += $" LIMIT {pageSize} OFFSET {offset}";

                    List<HistoricalEvent> resultList = GetHistoricalEventsFromDatabase(query, connection);

                    return Ok(resultList);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("GetById/{id}")]
        [AllowAnonymous]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connection))
                {
                    connection.Open();

                    string query = $"SELECT * FROM historicalevent WHERE Id = {id};";

                    List<HistoricalEvent> resultList = GetHistoricalEventsFromDatabase(query, connection);

                    if (resultList.Count > 0)
                        return Ok(resultList[0]); // Assuming there's only one result for a specific ID
                    else
                        return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("Search")]
        [AllowAnonymous]
        public IHttpActionResult Search(int? startYear = null, int? endYear = null, string startDate = null, string endDate = null, string keyword = null)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connection))
                {
                    connection.Open();

                    // Construct the base query
                    string query = "SELECT * FROM historicalevent WHERE 1 = 1";

                    // Append conditions based on parameters
                    if (startYear.HasValue && endYear.HasValue)
                    {
                        query += $" AND YEAR(PublishDate) BETWEEN {startYear} AND {endYear}";
                    }

                    if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                    {
                        query += $" AND PublishDate BETWEEN '{startDate}' AND '{endDate}'";
                    }

                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query += $" AND HeadlineText LIKE '%{keyword}%'";
                    }

                    List<HistoricalEvent> resultList = GetHistoricalEventsFromDatabase(query, connection);

                    return Ok(resultList);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut, Route("Edit/{id}")]
        [BasicAuthorisationAttribute(Roles = "admin")]
        public IHttpActionResult EditHistoricalEvent(int id, HistoricalEvent updatedEvent)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connection))
                {
                    connection.Open();

                    string query = $"SELECT * FROM historicalevent WHERE Id = {id};";

                    List<HistoricalEvent> existingEvents = GetHistoricalEventsFromDatabase(query, connection);

                    if (existingEvents.Count == 0)
                    {
                        return NotFound();
                    }

                    HistoricalEvent existingEvent = existingEvents[0];

                    // Validate authorization (example: only allow admins to edit)
                    if (!User.IsInRole("admin"))
                    {
                        return StatusCode(HttpStatusCode.Forbidden);
                    }

                    // Update event properties
                    existingEvent.PublishDate = updatedEvent.PublishDate;
                    existingEvent.HeadlineCategory = updatedEvent.HeadlineCategory;
                    existingEvent.HeadlineText = updatedEvent.HeadlineText;

                    // Execute the update query
                    query = $"UPDATE historicalevent SET PublishDate = '{existingEvent.PublishDate}', " +
                            $"HeadlineCategory = '{existingEvent.HeadlineCategory}', " +
                            $"HeadlineText = '{existingEvent.HeadlineText}' " +
                            $"WHERE Id = {id};";

                    using (MySqlCommand updateCommand = new MySqlCommand(query, connection))
                    {
                        updateCommand.ExecuteNonQuery();
                    }

                    return Ok(existingEvent);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete, Route("Delete/{id}")]
        [BasicAuthorisationAttribute(Roles = "admin")]
        public IHttpActionResult DeleteHistoricalEvent(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connection))
                {
                    connection.Open();

                    // Validate authorization (example: only allow admins to delete)
                    if (!User.IsInRole("admin"))
                    {
                        return StatusCode(HttpStatusCode.Forbidden);
                    }

                    // Execute the delete query
                    string query = $"DELETE FROM historicalevent WHERE Id = {id};";
                    using (MySqlCommand deleteCommand = new MySqlCommand(query, connection))
                    {
                        int rowsAffected = deleteCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return Ok();
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private List<HistoricalEvent> GetHistoricalEventsFromDatabase(string query, MySqlConnection connection)
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<HistoricalEvent> resultList = new List<HistoricalEvent>();

                    while (reader.Read())
                    {
                        HistoricalEvent historicalEvent = new HistoricalEvent
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            PublishDate = Convert.ToInt32(reader["PublishDate"]),
                            HeadlineCategory = reader["HeadlineCategory"].ToString(),
                            HeadlineText = reader["HeadlineText"].ToString()
                        };

                        resultList.Add(historicalEvent);
                    }

                    return resultList;
                }
            }
        }
    }
}

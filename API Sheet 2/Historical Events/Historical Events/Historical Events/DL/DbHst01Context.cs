using Historical_Events.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace Historical_Events.DL
{
    /// <summary>
    /// DB Class for Handling Historical Events.
    /// </summary>
    public class DbHst01Context
    {
        #region Private Members
        
        /// <summary>
        /// Connection String
        /// </summary>
        private readonly string _connection;

        /// <summary>
        /// Page Size.
        /// </summary>
        private readonly int _pageSize = 10;
        
        #endregion

        #region Public Members
        
        /// <summary>
        /// Instance of Response Class.
        /// </summary>
        public Response response;
        
        #endregion

        #region Constructor
        
        /// <summary>
        /// Constructor for initialisng Connection String.
        /// </summary>
        /// <param name="connectionString">Connection String of Db</param>
        public DbHst01Context(string connectionString)
        {
            _connection = connectionString;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves a paginated list of historical events from the database.
        /// </summary>
        /// <param name="pageNumber">The page number (1-based).</param>
        /// <returns>A paginated list of historical events.</returns>
        public List<HSTEVT01> GetAllEvents(int pageNumber)
        {
            int offset = (pageNumber - 1) * _pageSize; // Calculate the offset

            string query = $@"SELECT 
                            t01f01, 
                            t01f02, 
                            t01f03, 
                            t01f04 
                    FROM 
                            hstevt01 
                    LIMIT
                            {_pageSize} OFFSET {offset};"; // Apply pagination

            List<HSTEVT01> data = GetHistoricalEventsFromDatabase(query);

            return data;
        }

        /// <summary>
        /// Retrieves a historical event by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the historical event to retrieve.</param>
        /// <returns>The historical event if found; otherwise, null.</returns>
        public HSTEVT01 GetEventById(int id)
        {
            string query = $"SELECT " +
                                    $"t01f01, " +
                                    $"t01f02, " +
                                    $"t01f03, " +
                                    $"t01f04 " +
                           $"FROM " +
                                    $"hstevt01 " +
                            $"WHERE " +
                                   $"t01f01 = {id};";
            List<HSTEVT01> events = GetHistoricalEventsFromDatabase(query);

            return events.Count > 0 ? events[0] : null;
        }

        /// <summary>
        /// Retrieves a paginated list of historical events based on specified criteria.
        /// </summary>
        /// <param name="pageNumber">The page number (1-based).</param>
        /// <param name="startYear">The start year filter.</param>
        /// <param name="endYear">The end year filter.</param>
        /// <param name="startDate">The start date filter.</param>
        /// <param name="endDate">The end date filter.</param>
        /// <param name="keyword">The keyword to search for.</param>
        /// <returns>A paginated list of historical events matching the criteria.</returns>
        public List<HSTEVT01> SearchEvents(int pageNumber, int? startYear, int? endYear, string startDate, string endDate, string keyword)
        {
            int offset = (pageNumber - 1) * _pageSize; // Calculate the offset

            // Construct the base query
            string query = $@"SELECT 
                            t01f01, 
                            t01f02, 
                            t01f03, 
                            t01f04 
                    FROM 
                            hstevt01 
                    WHERE 
                            1 = 1";

            // Append conditions based on parameters
            if (startYear.HasValue && endYear.HasValue)
            {
                query += $" AND YEAR(t01f02) BETWEEN {startYear} AND {endYear}";
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                query += $" AND t01f02 BETWEEN '{startDate}' AND '{endDate}'";
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query += $" AND t01f04 LIKE '%{keyword}%'";
            }

            query += $" LIMIT {_pageSize} OFFSET {offset}"; // Apply pagination

            List<HSTEVT01> lstHstEvt01 = GetHistoricalEventsFromDatabase(query);

            return lstHstEvt01;
        }

        /// <summary>
        /// Retrieves historical events belonging to a specific category.
        /// </summary>
        /// <param name="category">The category to filter by.</param>
        /// <returns>A list of historical events in the specified category.</returns>
        public List<HSTEVT01> GetEventsByCategory(string category)
        {
            string query = $@"SELECT 
                                    t01f01, 
                                    t01f02, 
                                    t01f03, 
                                    t01f04  
                            FROM 
                                    hstevt01 
                            WHERE 
                                    t01f03 = '{category}';";

            List<HSTEVT01> lstHstEvt01 = GetHistoricalEventsFromDatabase(query);
            return lstHstEvt01;
        }

        /// <summary>
        /// Retrieves the latest historical events up to the specified count.
        /// </summary>
        /// <param name="count">The number of latest events to retrieve.</param>
        /// <returns>A list of the latest historical events.</returns>
        public List<HSTEVT01> GetLatestEvents(int count)
        {
            string query = $@"SELECT 
                                    t01f01, 
                                    t01f02, 
                                    t01f03, 
                                    t01f04  
                            FROM 
                                    hstevt01 
                            ORDER BY 
                                    t01f02 
                            DESC 
                                LIMIT {count};";

            List<HSTEVT01> lstHstEvt01 = GetHistoricalEventsFromDatabase(query);
            return lstHstEvt01;
        }

        /// <summary>
        /// Retrieves historical events within the specified date range.
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <returns>A list of historical events within the date range.</returns>
        public List<HSTEVT01> GetEventsByDateRange(string startDate, string endDate)
        {
            string query = $@"SELECT 
                                    t01f01, 
                                    t01f02, 
                                    t01f03, 
                                    t01f04  
                            FROM 
                                    hstevt01 
                            WHERE 
                                    t01f02 
                            BETWEEN 
                                    '{startDate}' AND '{endDate}';";


            List<HSTEVT01> lstHstEvt01 = GetHistoricalEventsFromDatabase(query);
            return lstHstEvt01;
        }

        /// <summary>
        /// Retrieves historical events containing the specified keyword.
        /// </summary>
        /// <param name="keyword">The keyword to search for.</param>
        /// <returns>A list of historical events containing the keyword.</returns>
        public List<HSTEVT01> GetEventsByKeyword(string keyword)
        {
            string query = $@"SELECT 
                                    t01f01, 
                                    t01f02, 
                                    t01f03, 
                                    t01f04  
                            FROM 
                                    hstevt01 
                            WHERE 
                                    t01f04 LIKE '{keyword}';";

            List<HSTEVT01> lstHstEvt01 = GetHistoricalEventsFromDatabase(query);
            return lstHstEvt01;
        }

        /// <summary>
        /// Retrieves unique categories of historical events.
        /// </summary>
        /// <returns>A list of unique categories.</returns>
        public List<string> GetUniqueCategories()
        {
            string query = @"SELECT 
                            DISTINCT 
                                    t01f03 
                            FROM    
                                    hstevt01;";

            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    return GetCategoriesFromDatabase(command);
                }
            }
        }

        /// <summary>
        /// Retrieves historical events that occurred on the current date.
        /// </summary>
        /// <returns>A list of historical events for today.</returns>
        public List<HSTEVT01> GetEventsForToday(int pageNumber)
        {
            // Calculate the offset based on the page number and page size
            int offset = (pageNumber - 1) * _pageSize;

            // Get today's day and month in the format "MMdd"
            string today = DateTime.Today.ToString("MMdd");

            // Construct the query to retrieve events for today's day and month
            string query = $@"SELECT 
                        t01f01, 
                        t01f02, 
                        t01f03, 
                        t01f04 
                    FROM 
                        hstevt01 
                    WHERE 
                        SUBSTRING(t01f02, 5) = '{today}' -- Matches day and month only
                    LIMIT
                        {_pageSize} OFFSET {offset};";

            // Retrieve events from the database based on the constructed query
            List<HSTEVT01> lstHstEvt01 = GetHistoricalEventsFromDatabase(query);
            return lstHstEvt01;
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Retrieves historical events from the database based on the provided query.
        /// </summary>
        /// <param name="query">The SQL query to retrieve historical events.</param>
        /// <returns>A list of historical events.</returns>
        private List<HSTEVT01> GetHistoricalEventsFromDatabase(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    return GetHistoricalEventsFromDatabase(command);
                }
            }
        }

        /// <summary>
        /// Retrieves historical events from the database using the provided MySqlCommand object.
        /// </summary>
        /// <param name="command">The MySqlCommand object to execute.</param>
        /// <returns>A list of historical events.</returns>
        private List<HSTEVT01> GetHistoricalEventsFromDatabase(MySqlCommand command)
        {
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                List<HSTEVT01> resultList = new List<HSTEVT01>();

                while (reader.Read())
                {
                    HSTEVT01 historicalEvent = new HSTEVT01
                    {
                        t01f01 = Convert.ToInt32(reader["t01f01"]),
                        t01f02 = Convert.ToInt32(reader["t01f02"]),
                        t01f03 = reader["t01f03"].ToString(),
                        t01f04 = reader["t01f04"].ToString()
                    };

                    resultList.Add(historicalEvent);
                }

                return resultList;
            }
        }

        /// <summary>
        /// Retrieves categories from the database using the provided MySqlCommand object.
        /// </summary>
        /// <param name="command">The MySqlCommand object to execute.</param>
        /// <returns>A list of category names.</returns>
        private List<string> GetCategoriesFromDatabase(MySqlCommand command)
        {
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                List<string> categories = new List<string>();

                while (reader.Read())
                {
                    categories.Add(reader["t01f03"].ToString());
                }
                return categories;
            }
        }

        #endregion
    }
}
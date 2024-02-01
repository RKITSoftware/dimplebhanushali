using Historical_Events.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Historical_Events.BL
{
    public class BLHistory
    {
        private readonly string _connection;

        public BLHistory(string connectionString)
        {
            _connection = connectionString;
        }

        public List<hstevt01> GetAllEvents()
        {
            string query = @"SELECT 
                                    t01f01, 
                                    t01f02, 
                                    t01f03, 
                                    t01f04 
                            FROM 
                                    hstevt01";
            return GetHistoricalEventsFromDatabase(query);
        }

        public hstevt01 GetEventById(int id)
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
            List<hstevt01> events = GetHistoricalEventsFromDatabase(query);

            return events.Count > 0 ? events[0] : null;
        }

        public List<hstevt01> SearchEvents(int? startYear, int? endYear, string startDate, string endDate, string keyword)
        {
            // Construct the base query
            string query = @"SELECT 
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

            return GetHistoricalEventsFromDatabase(query);
        }

        public List<hstevt01> GetEventsByCategory(string category)
        {
            string query = @"SELECT 
                                    t01f01, 
                                    t01f02, 
                                    t01f03, 
                                    t01f04  
                            FROM 
                                    hstevt01 
                            WHERE 
                                    t01f03 = @Category;";

            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Category", category);

                    return GetHistoricalEventsFromDatabase(command);
                }
            }
        }

        public List<hstevt01> GetLatestEvents(int count)
        {
            string query = @"SELECT 
                                    t01f01, 
                                    t01f02, 
                                    t01f03, 
                                    t01f04  
                            FROM 
                                    hstevt01 
                            ORDER BY 
                                    t01f02 
                            DESC 
                                LIMIT @Count;";

            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Count", count);

                    return GetHistoricalEventsFromDatabase(command);
                }
            }
        }

        public List<hstevt01> GetEventsByDateRange(string startDate, string endDate)
        {
            string query = @"SELECT 
                                    t01f01, 
                                    t01f02, 
                                    t01f03, 
                                    t01f04  
                            FROM 
                                    hstevt01 
                            WHERE 
                                    t01f02 
                            BETWEEN 
                                    @StartDate AND @EndDate;";

            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    return GetHistoricalEventsFromDatabase(command);
                }
            }
        }

        public List<hstevt01> GetEventsByKeyword(string keyword)
        {
            string query = @"SELECT 
                                    t01f01, 
                                    t01f02, 
                                    t01f03, 
                                    t01f04  
                            FROM 
                                    hstevt01 
                            WHERE 
                                    t01f04 LIKE @Keyword;";

            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

                    return GetHistoricalEventsFromDatabase(command);
                }
            }
        }

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

        public void CreateHistoricalEvent(hstevt01 newEvent)
        {
            string query = @"INSERT INTO 
                                    hstevt01 
                                            (t01f02, 
                                            t01f03, 
                                            t01f04) 
                            VALUES 
                                            (@t01f02, 
                                            @t01f03, 
                                            @t01f04);";

            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand insertCommand = new MySqlCommand(query, connection))
                {
                    insertCommand.Parameters.AddWithValue("@t01f02", newEvent.t01f02);
                    insertCommand.Parameters.AddWithValue("@t01f03", newEvent.t01f03);
                    insertCommand.Parameters.AddWithValue("@t01f04", newEvent.t01f04);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }

        public void EditHistoricalEvent(int id, hstevt01 updatedEvent)
        {
            string query = $"SELECT t01f01, t01f02, t01f03, t01f04  FROM hstevt01 WHERE t01f01 = {id};";
            List<hstevt01> existingEvents = GetHistoricalEventsFromDatabase(query);

            if (existingEvents.Count == 0)
            {
                throw new Exception("Event not found");
            }

            hstevt01 existingEvent = existingEvents[0];

            // Update event properties
            existingEvent.t01f02 = updatedEvent.t01f02;
            existingEvent.t01f03 = updatedEvent.t01f03;
            existingEvent.t01f04 = updatedEvent.t01f04;

            // Execute the update query
            query = @"UPDATE 
                                hstevt01 
                    SET 
                                t01f02 = @t01f02,  
                                t01f03 = @t01f03,  
                                t01f04 = @t01f04  
                    WHERE 
                                t01f01 = @Id;";

            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand updateCommand = new MySqlCommand(query, connection))
                {
                    updateCommand.Parameters.AddWithValue("@t01f02", existingEvent.t01f02);
                    updateCommand.Parameters.AddWithValue("@t01f03", existingEvent.t01f03);
                    updateCommand.Parameters.AddWithValue("@t01f04", existingEvent.t01f04);
                    updateCommand.Parameters.AddWithValue("@Id", id);

                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        public void DeleteHistoricalEvent(int id)
        {
            string query = @"DELETE FROM 
                                            hstevt01 
                            WHERE 
                                            t01f01 = @Id;";
            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand deleteCommand = new MySqlCommand(query, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Event not found");
                    }
                }
            }
        }

        private List<hstevt01> GetHistoricalEventsFromDatabase(string query)
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

        private List<hstevt01> GetHistoricalEventsFromDatabase(MySqlCommand command)
        {
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                List<hstevt01> resultList = new List<hstevt01>();

                while (reader.Read())
                {
                    hstevt01 historicalEvent = new hstevt01
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
    }
}

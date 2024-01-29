using Historical_Events.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace Historical_Events.BL
{
    public class BLHistory
    {
        private readonly string _connection;

        public BLHistory(string connectionString)
        {
            _connection = connectionString;
        }

        public List<HistoricalEvent> GetAllEvents()
        {
            string query = "SELECT * FROM historicalevent";
            return GetHistoricalEventsFromDatabase(query);
        }

        public HistoricalEvent GetEventById(int id)
        {
            string query = $"SELECT * FROM historicalevent WHERE Id = {id};";
            List<HistoricalEvent> events = GetHistoricalEventsFromDatabase(query);

            return events.Count > 0 ? events[0] : null;
        }

        public List<HistoricalEvent> SearchEvents(int? startYear, int? endYear, string startDate, string endDate, string keyword)
        {
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

            return GetHistoricalEventsFromDatabase(query);
        }

        public List<HistoricalEvent> GetEventsByCategory(string category)
        {
            string query = "SELECT * FROM historicalevent WHERE HeadlineCategory = @Category;";

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

        public List<HistoricalEvent> GetLatestEvents(int count)
        {
            string query = "SELECT * FROM historicalevent ORDER BY PublishDate DESC LIMIT @Count;";

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

        public List<HistoricalEvent> GetEventsByDateRange(string startDate, string endDate)
        {
            string query = "SELECT * FROM historicalevent WHERE PublishDate BETWEEN @StartDate AND @EndDate;";

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

        public List<HistoricalEvent> GetEventsByKeyword(string keyword)
        {
            string query = "SELECT * FROM historicalevent WHERE HeadlineText LIKE @Keyword;";

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
            string query = "SELECT DISTINCT HeadlineCategory FROM historicalevent;";

            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    return GetCategoriesFromDatabase(command);
                }
            }
        }

        public void CreateHistoricalEvent(HistoricalEvent newEvent)
        {
            string query = "INSERT INTO historicalevent (PublishDate, HeadlineCategory, HeadlineText) " +
                           "VALUES (@PublishDate, @HeadlineCategory, @HeadlineText);";

            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand insertCommand = new MySqlCommand(query, connection))
                {
                    insertCommand.Parameters.AddWithValue("@PublishDate", newEvent.PublishDate);
                    insertCommand.Parameters.AddWithValue("@HeadlineCategory", newEvent.HeadlineCategory);
                    insertCommand.Parameters.AddWithValue("@HeadlineText", newEvent.HeadlineText);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }

        public void EditHistoricalEvent(int id, HistoricalEvent updatedEvent)
        {
            string query = $"SELECT * FROM historicalevent WHERE Id = {id};";
            List<HistoricalEvent> existingEvents = GetHistoricalEventsFromDatabase(query);

            if (existingEvents.Count == 0)
            {
                throw new Exception("Event not found");
            }

            HistoricalEvent existingEvent = existingEvents[0];

            // Update event properties
            existingEvent.PublishDate = updatedEvent.PublishDate;
            existingEvent.HeadlineCategory = updatedEvent.HeadlineCategory;
            existingEvent.HeadlineText = updatedEvent.HeadlineText;

            // Execute the update query
            query = "UPDATE historicalevent SET " +
                    "PublishDate = @PublishDate, " +
                    "HeadlineCategory = @HeadlineCategory, " +
                    "HeadlineText = @HeadlineText " +
                    "WHERE Id = @Id;";

            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand updateCommand = new MySqlCommand(query, connection))
                {
                    updateCommand.Parameters.AddWithValue("@PublishDate", existingEvent.PublishDate);
                    updateCommand.Parameters.AddWithValue("@HeadlineCategory", existingEvent.HeadlineCategory);
                    updateCommand.Parameters.AddWithValue("@HeadlineText", existingEvent.HeadlineText);
                    updateCommand.Parameters.AddWithValue("@Id", id);

                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        public void DeleteHistoricalEvent(int id)
        {
            string query = "DELETE FROM historicalevent WHERE Id = @Id;";
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

        private List<HistoricalEvent> GetHistoricalEventsFromDatabase(string query)
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

        private List<HistoricalEvent> GetHistoricalEventsFromDatabase(MySqlCommand command)
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

        private List<string> GetCategoriesFromDatabase(MySqlCommand command)
        {
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                List<string> categories = new List<string>();

                while (reader.Read())
                {
                    categories.Add(reader["HeadlineCategory"].ToString());
                }
                return categories;
            }
        }
    }
}
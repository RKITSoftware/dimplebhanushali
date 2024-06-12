using MySql.Data.MySqlClient;
using System.Data;

namespace Historical_Events.DL
{
    /// <summary>
    /// Database class for managing users.
    /// </summary>
    public class DbUsr01Context
    {
        #region Private Member
        /// <summary>
        /// Connection string.
        /// </summary>
        private static string _connection;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DbUsr01Context"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string of the database.</param>
        public DbUsr01Context(string connectionString)
        {
            _connection = connectionString;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public DataTable GetAllUsers()
        {
            string query = @"SELECT 
                                        r01f01, 
                                        r01f02, 
                                        r01f03, 
                                        r01f04, 
                                        r01f05,
                                        CASE 
                                            WHEN r01f07 = 'A' THEN 'Admin'
                                            WHEN r01f07 = 'U' THEN 'User'
                                            ELSE 'Unknown'
                                        END AS r01f07
                                FROM 
                                        usr01";


            return GetUsersFromDatabase(query);
        }

        /// <summary>
        /// Retrieves a user by their ID from the database.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        public DataTable GetUserById(int id)
        {
            string query = $@"SELECT 
                                    r01f01, 
                                    r01f02, 
                                    r01f03, 
                                    r01f04, 
                                    r01f05,
                                    CASE 
                                        WHEN r01f07 = 'A' THEN 'Admin'
                                        WHEN r01f07 = 'U' THEN 'User'
                                        ELSE 'Unknown'
                                    END AS r01f07
                              FROM 
                                    usr01
                              WHERE 
                                    r01f01 = {id};";

            return GetUsersFromDatabase(query);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Retrieves users from the database based on the provided query.
        /// </summary>
        /// <param name="query">The SQL query to retrieve users.</param>
        /// <returns>A list of users.</returns>
        private static DataTable GetUsersFromDatabase(string query)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
            }

            return dataTable;
        }

        #endregion
    }
}

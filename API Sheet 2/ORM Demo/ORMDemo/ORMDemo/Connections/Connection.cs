using ServiceStack.OrmLite;
using System.Configuration;

namespace ORMDemo
{
    /// <summary>
    /// Represents a connection class for managing the database connection using OrmLite.
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Gets the connection string from the configuration file.
        /// </summary>
        public static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        /// <summary>
        /// Gets the OrmLite connection factory initialized with the MySQL 5.5 dialect.
        /// </summary>
        public static OrmLiteConnectionFactory connectionFactory = new OrmLiteConnectionFactory(
            connectionString, MySql55Dialect.Provider);
    }
}

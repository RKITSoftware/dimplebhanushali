using ServiceStack.OrmLite;
using System.Configuration;
using System.Data;

namespace Historical_Events.Data
{
    /// <summary>
    /// DbContext Class for Using ORM.
    /// </summary>
    public class MyDbContext
    {
        /// <summary>
        /// Gets the connection string from the configuration file.
        /// </summary>
        public static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        /// <summary>
        /// Gets the OrmLite connection factory initialized with the MySQL 5.5 dialect.
        /// </summary>
        public static IDbConnection CreateConnection()
        {
            var dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
            return dbFactory.Open();
        }
    }
}
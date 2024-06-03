using ServiceStack.OrmLite;
using System.Data;

namespace MiddlewareWithFilters.Data
{
    /// <summary>
    /// Factory class for creating database connections.
    /// </summary>
    public class DbConnectionFactory
    {
        #region Private Member
        /// <summary>
        /// Instance of IConfiguration.
        /// </summary>
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnectionFactory"/> class.
        /// </summary>
        /// <param name="configuration">The configuration object.</param>
        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new database connection.
        /// </summary>
        /// <returns>A new instance of IDbConnection representing the database connection.</returns>
        public IDbConnection CreateConnection()
        {
            string connectionString = _configuration.GetConnectionString("Conn");
            OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
            return dbFactory.Open();
        }

        #endregion
    }
}

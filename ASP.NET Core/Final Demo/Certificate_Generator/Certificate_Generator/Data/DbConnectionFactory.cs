using ServiceStack.OrmLite;
using System.Data;

namespace Certificate_Generator.Data
{
    public class DbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString("CertGen");
            var dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
            return dbFactory.Open();
        }
    }
}

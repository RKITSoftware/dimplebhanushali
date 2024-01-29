using ServiceStack.OrmLite;
using System.Configuration;

namespace ORMDemo
{
    public class Connection
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        public static OrmLiteConnectionFactory connectionFactory = new OrmLiteConnectionFactory(
            connectionString,MySql55Dialect.Provider);
    }
}
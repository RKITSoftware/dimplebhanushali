using Resume_Builder.Data;
using Resume_Builder.Models.POCO;
using ServiceStack.OrmLite;
using System.Data;

namespace Resume_Builder.BL
{
    /// <summary>
    /// Class for handling table operations using ORMLite.
    /// </summary>
    public class BLTables
    {
        #region Private Member

        /// <summary>
        /// Instance of Db Connection Factory.
        /// </summary>
        private DbConnectionFactory _connectionFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for BLTables.
        /// </summary>
        /// <param name="dbConnectionFactory">Database connection factory.</param>
        public BLTables(DbConnectionFactory dbConnectionFactory)
        {
            _connectionFactory = dbConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates tables if they do not exist.
        /// </summary>
        public void CreateTables()
        {
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                // Create tables if they do not exist
                db.CreateTableIfNotExists<USR01>();
                db.CreateTableIfNotExists<RES01>();
                db.CreateTableIfNotExists<CER01>();
                db.CreateTableIfNotExists<EDU01>();
                db.CreateTableIfNotExists<EXP01>();
                db.CreateTableIfNotExists<LAN01>();
                db.CreateTableIfNotExists<PRO01>();
                db.CreateTableIfNotExists<SKL01>();
            }
        }

        /// <summary>
        /// Drops existing tables and recreates them.
        /// </summary>
        public void DropNCreateTables()
        {
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                // Drop existing tables and recreate them
                db.DropAndCreateTable<USR01>();
                db.DropAndCreateTable<RES01>();
                db.DropAndCreateTable<CER01>();
                db.DropAndCreateTable<EDU01>();
                db.DropAndCreateTable<EXP01>();
                db.DropAndCreateTable<LAN01>();
                db.DropAndCreateTable<PRO01>();
                db.DropAndCreateTable<SKL01>();
            }
        }

        #endregion
    }
}

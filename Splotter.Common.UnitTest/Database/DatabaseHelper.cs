using System.Data;

namespace Splotter.Common.UnitTest.Database
{
    public class DatabaseHelper
    {
        private DatabaseHelper() { }

        public static object ExecuteScalar(string selectSql, IDbConnection dbConnection)
        {
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                dbCmd.CommandText = selectSql;
                return dbCmd.ExecuteScalar();
            }
        }
        public static int ExecuteNonQuery(string sqlScript, IDbConnection dbConnection)
        {
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                dbCmd.CommandText = string.Format(sqlScript);
                return dbCmd.ExecuteNonQuery();
            }
        }

    }
}

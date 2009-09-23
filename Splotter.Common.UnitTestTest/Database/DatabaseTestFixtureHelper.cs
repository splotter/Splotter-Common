using System;
using System.Data;
using Splotter.Common.UnitTest.Database;
using MbUnit.Framework;


namespace Splotter.Common.UnitTestTest
{
    public class DatabaseTestFixtureHelper
    {
        /// <summary>
        /// Verify the Database
        /// </summary>
        /// <param name="dbConnection">IDbConnection object</param>
        public static void VerifyDatabase(IDbConnection dbConnection)
        {
            using (dbConnection)
            {
                dbConnection.Open();

                string maxIdSql = "select max(id) from Table1";
                int newId = 0;
                object idObj = DatabaseHelper.ExecuteScalar(maxIdSql, dbConnection);
                if (!(idObj is DBNull))
                {
                    newId = (int)idObj;
                }
                newId++;
                string insertSql = string.Format("insert into Table1 values({0}, 'Value')", newId);
                string deleteSql = string.Format("delete from Table1 where Id = {0}", newId);
                Assert.AreEqual(1, DatabaseHelper.ExecuteNonQuery(insertSql, dbConnection));
                Assert.AreEqual(1, DatabaseHelper.ExecuteNonQuery(deleteSql, dbConnection));
            }
        }

        /// <summary>
        /// Verify that the Database contains the test data
        /// </summary>
        /// <param name="dbConnection">IDbConnection object</param>
        public static void VerifyTestData(IDbConnection dbConnection)
        {
            using (dbConnection)
            {
                dbConnection.Open();
                string selectSql = "select count(*) from Table1";
                Assert.AreEqual(2, (int)DatabaseHelper.ExecuteScalar(selectSql, dbConnection));
            }
        }

    }
}

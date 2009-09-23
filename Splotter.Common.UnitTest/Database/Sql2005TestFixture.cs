using System.Data.SqlClient;
using Microsoft.Win32;
using NDbUnit.Core.SqlClient;
using NDbUnit.Core;

namespace Splotter.Common.UnitTest.Database
{

    public class Sql2005TestFixture : DatabaseTestFixtureBase
    {
        public override string ConnectionString
        {
            get
            {
                return BuildConnectionStringFrom(DatabaseName);
            }
        }

        protected override string BuildConnectionStringFrom(string databaseName)
        {
            return
                string.Format(@"Data Source=(local);Initial Catalog={0};Integrated Security=SSPI", databaseName);
        }

        protected override void CreateDatabaseMedia()
        {
            string sqlServerDataDirectory = GetSqlServerDataDirectory();
            string createDatabaseScript = "IF (SELECT DB_ID('" + DatabaseName + "')) IS NULL  "
                                          + " CREATE DATABASE " + DatabaseName
                                          + " ON PRIMARY "
                                          + " (NAME = " + DatabaseName + "_Data, "
                                          + @" FILENAME = '" + sqlServerDataDirectory + DatabaseName + ".mdf', "
                                          + " SIZE = 5MB,"
                                          + " FILEGROWTH =" + 10 + ") "
                                          + " LOG ON (NAME =" + DatabaseName + "_Log, "
                                          + @" FILENAME = '" + sqlServerDataDirectory + DatabaseName + ".ldf', "
                                          + " SIZE = 1MB, "
                                          + " FILEGROWTH =" + 5 + ")";

            string connectionString = BuildConnectionStringFrom("master");
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                DatabaseHelper.ExecuteNonQuery(createDatabaseScript, sqlCon);
            }

        }

        protected virtual string GetSqlServerDataDirectory()
        {
            string sqlServerRegKey = @"SOFTWARE\Microsoft\Microsoft SQL Server\";
            string sqlServerInstanceName =
                (string)Registry.LocalMachine
                             .OpenSubKey(sqlServerRegKey + @"Instance Names\SQL")
                             .GetValue("MSSQLSERVER");
            string sqlServerInstanceSetupRegKey = sqlServerRegKey + sqlServerInstanceName + @"\Setup";
            return
                (string)Registry.LocalMachine
                             .OpenSubKey(sqlServerInstanceSetupRegKey)
                             .GetValue("SQLDataRoot") + @"\Data\";
        }

        public override void LoadTestData()
        {
            SqlDbUnitTest dbUnitTest = new SqlDbUnitTest(ConnectionString);
            dbUnitTest.ReadXmlSchema(XmlSchemaFile);
            dbUnitTest.ReadXml(XmlTestDataFile);
            dbUnitTest.PerformDbOperation(DbOperationFlag.CleanInsert);
        }

        protected override void SetupDatabaseSchema()
        {
            SqlServerSchemaFromXmlBuilder schemaBuilder = new SqlServerSchemaFromXmlBuilder(ConnectionString);
            schemaBuilder.XmlSchemaFile = XmlSchemaFile;
            schemaBuilder.XmlDataFile = XmlTestDataFile;
            schemaBuilder.Execute();
        }

    }

}

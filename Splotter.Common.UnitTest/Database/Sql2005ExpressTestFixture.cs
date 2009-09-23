using Microsoft.Win32;

namespace Splotter.Common.UnitTest.Database
{
    public class Sql2005ExpressTestFixture : Sql2005TestFixture
    {
        protected override string BuildConnectionStringFrom(string databaseName)
        {
            return
                string.Format(@"Data Source=(local)\sqlexpress;Initial Catalog={0};Integrated Security=SSPI", databaseName);

        }

        protected override string GetSqlServerDataDirectory()
        {
            string sqlServerRegKey = @"SOFTWARE\Microsoft\Microsoft SQL Server\";
            string sqlServerInstanceName =
                (string)Registry.LocalMachine
                             .OpenSubKey(sqlServerRegKey + @"Instance Names\SQL")
                             .GetValue("SQLEXPRESS");
            string sqlServerInstanceSetupRegKey = sqlServerRegKey + sqlServerInstanceName + @"\Setup";
            return
                (string)Registry.LocalMachine
                             .OpenSubKey(sqlServerInstanceSetupRegKey)
                             .GetValue("SQLDataRoot") + @"\Data\";
        }

    }
}

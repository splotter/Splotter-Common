using System.Data.SqlClient;
using MbUnit.Framework;
using Splotter.Common.UnitTest.Database;

namespace Splotter.Common.UnitTestTest
{
    [TestFixture]
    public class Sql2005TestFixtureTest : Sql2005TestFixture
    {
        [Test]
        public void CanSetupDatabaseAndLoadTestData()
        {
            SetupDatabase();
            LoadTestData();

            DatabaseTestFixtureHelper.VerifyDatabase(new SqlConnection(ConnectionString));
            DatabaseTestFixtureHelper.VerifyTestData(new SqlConnection(ConnectionString));
        }

        [Test]
        public void CanSetupDatabase()
        {
            SetupDatabase();
            DatabaseTestFixtureHelper.VerifyDatabase(new SqlConnection(ConnectionString));
        }


        [Test]
        public void CanDeleteDatabase()
        {
            
        }

    }
}

using System.Data.SqlClient;
using MbUnit.Framework;
using Splotter.Common.UnitTest.Database;

namespace Splotter.Common.UnitTestTest
{
    [TestFixture]
    public class Sql2005ExpressTestFixtureTest : Sql2005ExpressTestFixture
    {
        public override string XmlSchemaFile
        {
            get
            {
                return @"..\..\Database\TestData\Database.xsd";
            }
        }

        public override string XmlTestDataFile
        {
            get
            {
                return @"..\..\Database\TestData\TestData.xml";
            }
        }

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
    }
}

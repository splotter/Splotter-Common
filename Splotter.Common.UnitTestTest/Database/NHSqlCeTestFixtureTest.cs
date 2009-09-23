using MbUnit.Framework;
using Splotter.Common.UnitTest.Database;
using System.Data.SqlServerCe;
using NHibernate.Cfg;
using System.Reflection;

namespace Splotter.Common.UnitTestTest
{
    [TestFixture]
    public class NHSqlCeTestFixtureTest : NHSqlCeTestFixture
    {
        public override string XmlTestDataFile
        {
            get
            {
                return @"..\..\Database\TestData\TestData.xml";
            }
        }

        public override string XmlSchemaFile
        {
            get
            {
                return @"..\..\Database\TestData\Database.xsd";
            }
        }

        [TestFixtureSetUp]
        public void InitializeAllTests()
        {
            AddMappedAssembly(typeof(NHSqlCeTestFixtureTest).Assembly);
        }

        [Test]
        public void CanSetupDatabase()
        {
            SetupDatabase();
            DatabaseTestFixtureHelper.VerifyDatabase(new SqlCeConnection(ConnectionString));
        }

        [Test]
        public void CanSetupDatabaseAndLoadTestData()
        {
            SetupDatabase();
            LoadTestData();

            DatabaseTestFixtureHelper.VerifyDatabase(new SqlCeConnection(ConnectionString));
            DatabaseTestFixtureHelper.VerifyTestData(new SqlCeConnection(ConnectionString));
        }


    }
}

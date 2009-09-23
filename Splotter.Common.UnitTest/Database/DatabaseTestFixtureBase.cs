
namespace Splotter.Common.UnitTest.Database
{
    public abstract class DatabaseTestFixtureBase
    {
        public abstract string ConnectionString { get; }

        protected virtual string DatabaseName
        {
            get
            {
                return "DataBaseUnitTest";
            }
        }

        public virtual string XmlTestDataFile
        {
            get { return @"..\..\Database\TestData.xml"; }
        }

        public virtual string XmlSchemaFile
        {
            get
            {
                { return @"..\..\Database\Database.xsd"; }
            }
        }

        public void SetupDatabase()
        {
            CreateDatabaseMedia();
            SetupDatabaseSchema();
        }

        protected abstract string BuildConnectionStringFrom(string databaseName);

        protected abstract void CreateDatabaseMedia();

        protected abstract void SetupDatabaseSchema();

        public abstract void LoadTestData();

    }
}

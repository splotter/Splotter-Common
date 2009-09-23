using System.IO;
using System.Data.SqlServerCe;
using NHibernate.Cfg;
using System.Data;
using NDbUnit.Core.SqlServerCe;
using NDbUnit.Core;
using System.Collections.Generic;
using System.Reflection;
using NHibernate;

namespace Splotter.Common.UnitTest.Database
{
    public class NHSqlCeTestFixture : DatabaseTestFixtureBase
    {
        private Configuration _config;
        private static ISessionFactory _sessionFactory;

        public NHSqlCeTestFixture()
        {
            _config = new Configuration();
            _config.Properties = NHibernateProperties;
        }

        public override string ConnectionString
        {
            get
            {
                return BuildConnectionStringFrom(GetDbFile());
            }
        }

        public void AddMappedAssembly(Assembly mappedAssembly)
        {
            _config.AddAssembly(mappedAssembly);
        }

        protected ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = _config.BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        protected ISession GetSession()
        {
            return SessionFactory.OpenSession();
        }

        protected IDictionary<string, string> NHibernateProperties
        {
            get
            {
                Dictionary<string, string> properties = new Dictionary<string, string>();
                properties.Add(Environment.ConnectionDriver, "NHibernate.Driver.SqlServerCeDriver");
                properties.Add(Environment.Dialect, "NHibernate.Dialect.MsSqlCeDialect");
                properties.Add(Environment.ConnectionProvider,
                               "NHibernate.Connection.DriverConnectionProvider");
                properties.Add(Environment.ConnectionString, ConnectionString);
                properties.Add(Environment.ShowSql, "true");
                properties.Add(Environment.ReleaseConnections, "on_close");
                return properties;
            }
        }

        public override void LoadTestData()
        {
            SqlCeDbUnitTest sqlCeUnit = new SqlCeDbUnitTest(ConnectionString);
            sqlCeUnit.ReadXmlSchema(XmlSchemaFile);
            sqlCeUnit.ReadXml(XmlTestDataFile);
            sqlCeUnit.PerformDbOperation(DbOperationFlag.CleanInsert);
        }

        protected override string BuildConnectionStringFrom(string databaseName)
        {
            return
                string.Format(@"Data Source='{0}';", databaseName);
        }

        protected override void CreateDatabaseMedia()
        {
            string dbFile = GetDbFile();

            if (File.Exists(dbFile))
                File.Delete(dbFile);

            SqlCeEngine sqlCeEngine = new SqlCeEngine();
            sqlCeEngine.LocalConnectionString = ConnectionString;
            sqlCeEngine.CreateDatabase();
        }

        protected override void SetupDatabaseSchema()
        {
            HbmSchemaExporter schemaExporter = new HbmSchemaExporter(_config, new SqlCeConnection(ConnectionString));
            schemaExporter.Execute();
        }

        private string GetDbFile()
        {
            string dbFile;
            if (!DatabaseName.EndsWith(".sdf"))
                dbFile = string.Format("{0}.sdf", DatabaseName);
            else
                dbFile = DatabaseName;
            return dbFile;
        }

    }
}

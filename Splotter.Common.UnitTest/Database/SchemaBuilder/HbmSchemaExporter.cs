using System.Data.Common;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;

namespace Splotter.Common.UnitTest.Database
{
    public class HbmSchemaExporter : IDbSchemaBuilder
    {
        private Configuration _configuration;

        private DbConnection _dbConnection;

        private bool _executeToDatabase = true;

        private bool _formatOutput = true;

        private bool _justDropTable = false;

        private bool _outputToConsole = true;

        /// <summary>
        /// Initializes a new instance of the HbmSchemaExporter class.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="dbConnection"></param>
        public HbmSchemaExporter(Configuration configuration, DbConnection dbConnection)
        {
            _configuration = configuration;
            _dbConnection = dbConnection;
        }

        public Configuration Configuration
        {
            get { return _configuration; }
            set
            {
                _configuration = value;
            }
        }

        public string ConnectionString
        {
            get { return _dbConnection.ConnectionString; }
            set
            {
                _dbConnection.ConnectionString = value;
            }
        }

        public DbConnection DbConnection
        {
            get { return _dbConnection; }
            set
            {
                _dbConnection = value;
            }
        }

        public bool ExecuteToDatabase
        {
            get { return _executeToDatabase; }
            set
            {
                _executeToDatabase = value;
            }
        }

        public bool FormatOutput
        {
            get { return _formatOutput; }
            set
            {
                _formatOutput = value;
            }
        }

        public bool JustDropTable
        {
            get { return _justDropTable; }
            set
            {
                _justDropTable = value;
            }
        }

        public bool OutputToConsole
        {
            get { return _outputToConsole; }
            set
            {
                _outputToConsole = value;
            }
        }

        public void Execute()
        {
            SchemaExport schemaExport = new SchemaExport(_configuration);
            bool wasDbConnectionClosed = (_dbConnection.State == System.Data.ConnectionState.Closed);
            if (wasDbConnectionClosed)
            {
                _dbConnection.Open();
            }

            try
            {
                schemaExport.Execute(_outputToConsole, _executeToDatabase, _justDropTable, _formatOutput, _dbConnection, null);
            }
            catch (DbException) { throw; }
            finally
            {
                if (wasDbConnectionClosed && _dbConnection.State == System.Data.ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }

    }
}

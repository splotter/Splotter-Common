using System;
using SQLXMLBULKLOADLib;
using System.Threading;

namespace Splotter.Common.UnitTest.Database
{
    public class SqlServerSchemaFromXmlBuilder : IDbSchemaBuilder
    {
        private string _connectionString;

        private string _provider = "SQLOLEDB";

        private string _xmlDataFile;

        private string _xmlSchemaFile = String.Empty;

        private object lockObject = new object();

        private bool sqlXmlBulkLoadHasException;

        private string sqlXmlExceptionMessage;

        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
            }
        }

        public string XmlDataFile
        {
            get
            {
                return _xmlDataFile;
            }
            set
            {
                _xmlDataFile = value;
            }
        }

        public string XmlSchemaFile
        {
            get
            {
                return _xmlSchemaFile;
            }
            set
            {
                _xmlSchemaFile = value;
            }
        }

        public void Execute()
        {
            Thread bulkLoad = new Thread(new ParameterizedThreadStart(LoadData));
            bulkLoad.SetApartmentState(ApartmentState.STA);
            bulkLoad.Start(_connectionString);
            bulkLoad.Join();
            if (sqlXmlBulkLoadHasException)
                throw new InvalidOperationException(sqlXmlExceptionMessage);
        }

        public string Provider
        {
            get
            {
                return _provider;
            }
            set
            {
                _provider = value;
            }
        }

        public SqlServerSchemaFromXmlBuilder(string connectionString)
        {
            _connectionString = connectionString;
        }

        [STAThread]
        private void LoadData(object connectionString)
        {
            try
            {
                SQLXMLBulkLoad4Class sqlXmlBulkLoad = new SQLXMLBulkLoad4Class();
                sqlXmlBulkLoad.ConnectionString = string.Format("Provider={0};{1}", _provider, connectionString.ToString());
                sqlXmlBulkLoad.SchemaGen = true;
                sqlXmlBulkLoad.SGDropTables = true;
                sqlXmlBulkLoad.Execute(_xmlSchemaFile, _xmlDataFile);
            }
            catch (Exception ex)
            {
                lock (lockObject)
                {
                    sqlXmlBulkLoadHasException = true;
                    sqlXmlExceptionMessage = string.Concat("SqlXmlBulkLoad failed: ", ex.Message);
                }
            }
        }

    }
}

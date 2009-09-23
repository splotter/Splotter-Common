
namespace Splotter.Common.UnitTestTest
{
    public class TestBusinessEntity
    {
        private int _id;
        public virtual int Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        private string _value;
        public virtual string Value
        {
            get { return _value; }
            set
            {
                _value = value;
            }
        }
        

        public static string TableName = "Table1";


        public class Mapping
        {
            private Mapping() { }

            public static string Id = "Id";
            public static string Value = "Value";
        }
        
    }
}

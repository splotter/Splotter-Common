using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Splotter.Common.Persistence;


using Moq;

namespace Splotter.Common.PersistenceTest
{
    public class FakeEntity
    {
        private int _id;
        private string _data;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public static IQueryable<FakeEntity> CreateFakeEntities()
        {
            IList<FakeEntity> entities = new List<FakeEntity>();
            entities.Add(new FakeEntity { ID = 1, Data = "One" });
            entities.Add(new FakeEntity { ID = 2, Data = "Two" });
            entities.Add(new FakeEntity { ID = 3, Data = "Three" });

            return entities.AsQueryable<FakeEntity>();
        }

    }

}

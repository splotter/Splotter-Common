using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Splotter.Common.Persistence;


namespace Splotter.Common.PersistenceTest
{
    public class FakeEntityWithID1 : SpecificationBase<FakeEntity>
    {
        public override System.Linq.Expressions.Expression<Func<FakeEntity, bool>> Predicate
        {
            get { return x=>x.ID==1; }
        }
    }
}

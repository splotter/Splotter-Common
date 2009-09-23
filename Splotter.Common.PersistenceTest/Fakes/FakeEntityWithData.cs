using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Linq.Expressions;
using Splotter.Common.Persistence;

namespace Splotter.Common.PersistenceTest
{
    public class FakeEntityWithData : ParameterizedSpecificationBase<FakeEntity, string>
    {
        public override Expression<Func<FakeEntity, bool>> Predicate
        {
            get { return x => x.Data == _param; }
        }
    }
}

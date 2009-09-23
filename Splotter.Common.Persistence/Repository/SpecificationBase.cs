using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Splotter.Common.Persistence
{
    public abstract class SpecificationBase<T> 
        where T : class
    {
        public abstract Expression<Func<T,bool>> Predicate { get; }

        public IQueryable<T> Query(IQueryable<T> elements)
        {
            return elements.Where(Predicate);
        }
    }
}

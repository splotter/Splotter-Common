using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Linq;
using Splotter.Common.Persistence;

namespace Splotter.Common.Persistence.Linq
{
    public class Linq2SqlRepositoryFactory<TDataContext> : RepositoryFactoryBase
        where TDataContext : DataContext, new()
    {

        public Linq2SqlRepositoryFactory(Linq2SqlPersistenceManager<TDataContext> persistenceManager):
            base(persistenceManager)
        {

        }

        public override IRepository<T> GetRepository<T>()
        {
            return new Linq2SqlRepository<TDataContext, T>(this);
        }
    }
}

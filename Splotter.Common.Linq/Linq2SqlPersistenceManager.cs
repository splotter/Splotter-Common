using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Splotter.Common.Persistence;
using System.Data.Linq;

namespace Splotter.Common.Persistence.Linq
{
    public class Linq2SqlPersistenceManager<TDataContext> : IPersistenceManager
        where TDataContext : DataContext, new()
    {
        public IRepositoryFactory RepositoryFactory
        {
            get
            {
               return new Linq2SqlRepositoryFactory<TDataContext>(this);
            }
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            return new Linq2SqlUnitOfWork<TDataContext>(new TDataContext());
        }

    }
}

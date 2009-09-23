using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Linq;
using Splotter.Common.Persistence;
using System.Linq.Expressions;

namespace Splotter.Common.Persistence.Linq
{
    public class Linq2SqlRepository<TDataContext, TEntity> : RepositoryBase<TEntity>

        where TDataContext : DataContext, new()
        where TEntity : class
    {
        public Linq2SqlRepository(Linq2SqlRepositoryFactory<TDataContext> repositoryFactory):
            base(repositoryFactory)
        {

        }

    }

}

    


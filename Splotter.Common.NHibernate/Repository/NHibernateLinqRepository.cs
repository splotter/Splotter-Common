using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splotter.Common.Persistence.NHibernate
{
    public class NHibernateLinqRepository<TNHibernateLinqContextForUnitOfWork, TEntity> : RepositoryBase<TEntity>
        where TNHibernateLinqContextForUnitOfWork : NHibernateLinqContextForUnitOfWorkBase, new()
        where TEntity : class
    {
        public NHibernateLinqRepository(NHibernateLinqRepositoryFactory<TNHibernateLinqContextForUnitOfWork> repositoryFactory)
            : base(repositoryFactory)
        {

        }

    }
}

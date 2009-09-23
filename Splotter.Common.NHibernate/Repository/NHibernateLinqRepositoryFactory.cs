using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splotter.Common.Persistence.NHibernate
{
    public class NHibernateLinqRepositoryFactory<TNHibernateLinqContextForUnitOfWork> : RepositoryFactoryBase
        where TNHibernateLinqContextForUnitOfWork : NHibernateLinqContextForUnitOfWorkBase, new()
    {
        public NHibernateLinqRepositoryFactory(IPersistenceManager persistenceManager)
            : base(persistenceManager)
        {
        }

        public override IRepository<TEntity> GetRepository<TEntity>()
        {
            return new NHibernateLinqRepository<TNHibernateLinqContextForUnitOfWork, TEntity>(this);
        }
    }
}

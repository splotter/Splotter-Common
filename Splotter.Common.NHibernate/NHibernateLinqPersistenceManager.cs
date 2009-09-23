using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;

namespace Splotter.Common.Persistence.NHibernate
{
    public class NHibernateLinqPersistenceManager<TNHibernateLinqContextForUnitOfWork> : IPersistenceManager
        where TNHibernateLinqContextForUnitOfWork : NHibernateLinqContextForUnitOfWorkBase, new()
    {

        public IUnitOfWork CreateUnitOfWork()
        {
            return new TNHibernateLinqContextForUnitOfWork();
        }

        public IRepositoryFactory RepositoryFactory
        {
            get { return new NHibernateLinqRepositoryFactory<TNHibernateLinqContextForUnitOfWork>(this); }
        }

    }
}

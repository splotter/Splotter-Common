using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Splotter.Common.Persistence
{
    public abstract class RepositoryFactoryBase : IRepositoryFactory
    {
        private IPersistenceManager _persistenceManager;

        public RepositoryFactoryBase(IPersistenceManager persistenceManager)
        {
            _persistenceManager = persistenceManager;
        }

        public IUnitOfWork CurrentUnitOfWork
        {
            get { return UnitOfWorkController.Current; }
        }

        public IPersistenceManager PersistenceManager
        {
            get { return _persistenceManager; }
        }

        public abstract IRepository<T> GetRepository<T>() where T : class;

    }
}

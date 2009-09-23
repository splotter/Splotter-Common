using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Splotter.Common.Persistence
{
    public interface IRepositoryFactory
    {
        IPersistenceManager PersistenceManager { get; }
        IUnitOfWork CurrentUnitOfWork { get; }
        IRepository<T> GetRepository<T>() where T : class; 
    }
}

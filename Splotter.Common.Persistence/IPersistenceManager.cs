using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Splotter.Common.Persistence
{
    public interface IPersistenceManager
    {
        IUnitOfWork CreateUnitOfWork();
        IRepositoryFactory RepositoryFactory { get; }
    }
}

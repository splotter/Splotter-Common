using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splotter.Common.Persistence
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public virtual void Dispose()
        {
            UnitOfWorkController.DisposeUnitOfWork(this);
        }

        public abstract void Flush();
        public abstract void Save<T>(T entity) where T : class;
        public abstract void Delete<T>(T entity) where T : class;
        public abstract IQueryable<T> GetAll<T>() where T : class;

    }
}

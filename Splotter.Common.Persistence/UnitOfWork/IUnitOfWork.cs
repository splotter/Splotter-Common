using System;
using System.Linq;
namespace Splotter.Common.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IQueryable<T> GetAll<T>() where T : class;
        void Delete<T>(T entity)  where T: class;
        void Save<T>(T entity) where T : class;
        void Flush();
    }
}

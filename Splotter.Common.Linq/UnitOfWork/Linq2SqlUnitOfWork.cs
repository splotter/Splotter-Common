using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Linq;
using System.Reflection;
using Splotter.Common.Persistence;

namespace Splotter.Common.Persistence.Linq
{
    public class Linq2SqlUnitOfWork<TDataContext> : UnitOfWorkBase
        where TDataContext : DataContext, new()
    {
        TDataContext _dataContext;

        public Linq2SqlUnitOfWork(TDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public override void Flush()
        {
            _dataContext.SubmitChanges();
        }

        public override void Dispose()
        {
            _dataContext.Dispose();
            base.Dispose();
        }

        public override void Save<T>(T entity)
        {
            Table<T> table = GetTable<T>();
            if (!table.Contains(entity))
            {
                table.InsertOnSubmit(entity);
            }
        }

        public override void Delete<T>(T entity)
        {
            GetTable<T>().DeleteOnSubmit(entity);
        }

        public override IQueryable<T> GetAll<T>()
        {
           return GetTable<T>();
        }

        private Table<T> GetTable<T>()
            where T : class
        {
            return _dataContext.GetTable<T>();
        }

        public TDataContext DataContext
        {
            get
            {
                return _dataContext;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Splotter.Common.Persistence
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T,bool>> predicate);
        T GetOne(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll<TSpecs>() where TSpecs : SpecificationBase<T>, new();
        T GetOne<TSpecs>() where TSpecs : SpecificationBase<T>, new();
        IQueryable<T> GetAll<TSpecs, TParam>(TParam param) where TSpecs : ParameterizedSpecificationBase<T, TParam>, new();
        T GetOne<TSpecs, TParam>(TParam param) where TSpecs : ParameterizedSpecificationBase<T, TParam>, new();
        void Save(T entity);
        void Delete(T entity);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Linq.Expressions;

namespace Splotter.Common.Persistence
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public RepositoryBase(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        protected IUnitOfWork UnitOfWork
        {
            get { return _repositoryFactory.CurrentUnitOfWork; }
        }

        public virtual void Delete(T entity)
        {
            UnitOfWork.Delete<T>(entity);
        }

        public virtual IQueryable<T> GetAll()
        {
            return UnitOfWork.GetAll<T>();
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public virtual IQueryable<T> GetAll<TSpecs>() where TSpecs : SpecificationBase<T>, new()
        {
            return new TSpecs().Query(GetAll());
        }

        public virtual IQueryable<T> GetAll<TSpecs, TParam>(TParam param) where TSpecs : ParameterizedSpecificationBase<T, TParam>, new()
        {
            TSpecs specs = new TSpecs();
            specs.InitParam(param);
            return specs.Query(GetAll());
        }


        public virtual T GetOne<TSpecs>() where TSpecs : SpecificationBase<T>, new()
        {
            return new TSpecs().Query(GetAll()).FirstOrDefault();
        }

        public virtual T GetOne<TSpecs, TParam>(TParam param) where TSpecs : ParameterizedSpecificationBase<T, TParam>, new()
        {
            TSpecs specs = new TSpecs();
            specs.InitParam(param);
            return specs.Query(GetAll()).FirstOrDefault();
        }

        public virtual T GetOne(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate).FirstOrDefault();
        }

        public virtual void Save(T entity)
        {
            UnitOfWork.Save<T>(entity);
        }

    }
}

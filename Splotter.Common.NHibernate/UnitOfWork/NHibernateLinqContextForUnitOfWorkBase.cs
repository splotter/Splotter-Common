using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using NHibernate;
using NHibernate.Cfg;

namespace Splotter.Common.Persistence.NHibernate
{
    public abstract class NHibernateLinqContextForUnitOfWorkBase : NHibernateContext, INHibernateUnitOfWork
    {
        public INHibernateUnitOfWork _nhUnitOfWork;

        public NHibernateLinqContextForUnitOfWorkBase(INHibernateUnitOfWork nhUnitOfWork)
            : base(nhUnitOfWork.CurrentSession)
        {
            _nhUnitOfWork = nhUnitOfWork;
        }

        public NHibernateLinqContextForUnitOfWorkBase()
            : this(new NHibernateUnitOfWork())
        {

        }

        Configuration INHibernateUnitOfWork.Configuration
        {
            get { return _nhUnitOfWork.Configuration; }
        }

        ISession INHibernateUnitOfWork.CurrentSession
        {
            get { return _nhUnitOfWork.CurrentSession; }
            set { _nhUnitOfWork.CurrentSession = value; }
        }

        ISessionFactory INHibernateUnitOfWork.SessionFactory
        {
            get { return _nhUnitOfWork.SessionFactory; }
        }


        void IUnitOfWork.Delete<T>(T entity)
        {
            Session.Delete(entity);
        }

        void IUnitOfWork.Flush()
        {
            Session.Flush();
        }

        IQueryable<T> IUnitOfWork.GetAll<T>()
        {
            return Session.Linq<T>();
        }

        void IUnitOfWork.Save<T>(T entity)
        {
            Session.Save(entity);
        }


        void IDisposable.Dispose()
        {
           _nhUnitOfWork.Dispose();
        }


    }
}

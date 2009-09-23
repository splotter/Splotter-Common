using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using System.IO;
using System.Xml;
using System.Reflection;
using Splotter.Common.Persistence;


namespace Splotter.Common.Persistence.NHibernate
{
    public class NHibernateUnitOfWork : UnitOfWorkBase, INHibernateUnitOfWork
    {

        private Configuration _configuration;

        private static ISession _currentSession;

        private ISessionFactory _sessionFactory;

        public NHibernateUnitOfWork()
        {
            _configuration = InitConfiguration();
            InitSession();
        }

        public Configuration Configuration
        {
            get
            {
                return _configuration;
            }
        }

        public ISession CurrentSession
        {
            get
            {
                if (_currentSession == null)
                    throw new InvalidOperationException("You are not in a unit of work.");
                return _currentSession;
            }
            set { _currentSession = value; }
        }

        public ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    _sessionFactory = Configuration.BuildSessionFactory();
                return _sessionFactory;
            }
        }

        public override void Dispose()
        {
            CurrentSession = null;
            base.Dispose();
        }

        public override void Flush()
        {
            _currentSession.Flush();
        }

        protected virtual Configuration InitConfiguration()
        {
            return new Configuration().Configure();
        }

        private ISession CreateSession()
        {
            return SessionFactory.OpenSession();
        }

        private void InitSession()
        {
            ISession session = CreateSession();
            session.FlushMode = FlushMode.Commit;
            _currentSession = session;
        }


        public override void Save<T>(T entity)
        {
            _currentSession.SaveOrUpdate(entity);;
        }

        public override void Delete<T>(T entity)
        {
            _currentSession.Delete(entity);
        }

        public override IQueryable<T> GetAll<T>()
        {
            throw new NotSupportedException("This method is not supported. Use NHibernateLinqContextForUnitOfWorkBase");
        }
    }
}

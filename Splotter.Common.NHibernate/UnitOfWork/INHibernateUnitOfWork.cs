using System;
using System.Linq;
using NHibernate.Cfg;
using NHibernate;
namespace Splotter.Common.Persistence.NHibernate
{
    public interface INHibernateUnitOfWork : IUnitOfWork
    {
        Configuration Configuration { get; }
        ISession CurrentSession { get; set; }
        ISessionFactory SessionFactory { get; }
    }
}

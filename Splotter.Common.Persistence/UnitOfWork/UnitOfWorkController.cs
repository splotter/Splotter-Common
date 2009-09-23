using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Splotter.Common.Persistence
{
    public static class UnitOfWorkController
    {
        public const string CurrentUnitOfWorkKey = "CurrentUnitOfWork.Key";

        public static IUnitOfWork Current
        {
            get
            {
                if (!IsStarted)
                    throw new InvalidOperationException("Unit of Work is not started.");

                return LocalUnitOfWork;
            }
        }

        public static bool IsStarted
        {
            get
            {
                return (LocalUnitOfWork != null);
            }
        }

        private static IUnitOfWork LocalUnitOfWork
        {
            get
            {
                return Local.Data[CurrentUnitOfWorkKey] as IUnitOfWork;
            }
            set
            {
                Local.Data[CurrentUnitOfWorkKey] = value;
            }
        }

        public static void DisposeUnitOfWork(IUnitOfWork unitOfWork)
        {
            LocalUnitOfWork = null;
        }

        /// <summary>
        /// Start Unit of Work specifying IPersistenceManager
        /// </summary>
        /// <param name="manager">IPersistenceManager</param>
        /// <returns></returns>
        public static IUnitOfWork Start(IPersistenceManager manager)
        {
            if (IsStarted)
                throw new InvalidOperationException("Unit of Work is already started.");

            IUnitOfWork unitOfWork = manager.CreateUnitOfWork();

            LocalUnitOfWork = unitOfWork;

            return unitOfWork;
        }

        /// <summary>
        /// Start Unit Of Work relying on IoC to resolve IPersistenceManager instance.
        /// IoC is expeccted to be intialized and IPersistence manager registered.
        /// </summary>
        /// <returns></returns>
        public static IUnitOfWork Start()
        {
            if (IsStarted)
                throw new InvalidOperationException("Unit of Work is already started.");

            IPersistenceManager manager = IoC.ResolveType<IPersistenceManager>();

            IUnitOfWork unitOfWork = manager.CreateUnitOfWork();

            LocalUnitOfWork = unitOfWork;

            return unitOfWork;
        }





    }
}

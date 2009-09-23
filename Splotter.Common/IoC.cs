using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;

namespace Splotter.Common
{
    public static class IoC
    {
        private static IWindsorContainer _container;

        public static IWindsorContainer Container
        {
            get
            {
                if (_container == null)
                    throw new InvalidOperationException("IoC Container not initialized.");
                return _container;
            }
        }
        public static void Initialize(IWindsorContainer container)
        {
            _container = container;
        }

        public static void ReleaseComponent(object instance)
        {
            _container.Release(instance);
        }

        public static T ResolveType<T>(string key)
        {
            return _container.Resolve<T>(key);
        }
        public static T ResolveType<T>()
        {
            return _container.Resolve<T>();
        }

    }
}

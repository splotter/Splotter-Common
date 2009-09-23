using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Castle.Windsor;

namespace Splotter.Common.Test
{
    [TestFixture]
    public class IoCTest
    {
        public void CanResolveType()
        {
            IWindsorContainer windsorContainer = new WindsorContainer();
            windsorContainer.AddComponent<IWindsorContainer, WindsorContainer>();

            IoC.Initialize(windsorContainer);

            IWindsorContainer workFlowFactory = IoC.ResolveType<IWindsorContainer>();

            Assert.IsNotNull(workFlowFactory);
        }

        [Test]
        public void CanResolveTypeByKey()
        {
            IWindsorContainer windsorContainer = new WindsorContainer();
            windsorContainer.AddComponent<IWindsorContainer, WindsorContainer>("windsor");

            IoC.Initialize(windsorContainer);

            IWindsorContainer workFlowFactory = IoC.ResolveType<IWindsorContainer>("windsor");

            Assert.IsNotNull(workFlowFactory);
        }

        [Test]
        public void CanReleaseComponent()
        {
            IWindsorContainer windsorContainer = new WindsorContainer();
            windsorContainer.AddComponent<IWindsorContainer, WindsorContainer>("windsor");

            IoC.Initialize(windsorContainer);

            IWindsorContainer workFlowFactory = IoC.ResolveType<IWindsorContainer>("windsor");

            Assert.IsNotNull(workFlowFactory);

            IoC.ReleaseComponent(workFlowFactory);

        }

        [Test]
        public void CanGetBackContainer()
        {
            IWindsorContainer windsorContainer = new WindsorContainer();
            IoC.Initialize(windsorContainer);
            IWindsorContainer container = IoC.Container;
            Assert.AreSame(windsorContainer, container);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AccessingContainerPropertyThrowsWhenNotInitialized()
        {
            IoC.Initialize(null);
            IWindsorContainer container = IoC.Container;
        }
    }
}

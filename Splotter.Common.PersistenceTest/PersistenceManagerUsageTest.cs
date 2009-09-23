using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Splotter.Common.Persistence;
using Moq;

namespace Splotter.Common.PersistenceTest
{
    [TestFixture]
    public class PersistenceManagerUsageTest
    {

        private Mock<IPersistenceManager> _moqManager;

        [SetUp]
        public void Setup()
        {
            _moqManager = new Mock<IPersistenceManager>();
        }

        [Test]
        public void CanCreateRepositoryFactory()
        {
            IPersistenceManager manager = _moqManager.Object;
            var moqRepositoryFactory = new Mock<RepositoryFactoryBase>(manager);

            _moqManager.ExpectGet(x => x.RepositoryFactory)
                .Returns(moqRepositoryFactory.Object);
            
            IRepositoryFactory repositoryFactory = manager.RepositoryFactory;
            Assert.AreEqual(manager, repositoryFactory.PersistenceManager);
        }

        [Test]
        public void CanStartUnitOfWork()
        {
            var moqUnitOfWork = new Mock<UnitOfWorkBase>();
            moqUnitOfWork.CallBase = true;

            _moqManager.Expect(x => x.CreateUnitOfWork())
                .Returns(moqUnitOfWork.Object);

            IPersistenceManager manager = _moqManager.Object;
            
            using (IUnitOfWork unitOfWork = UnitOfWorkController.Start(manager))
            {
                Assert.IsTrue(UnitOfWorkController.IsStarted);
            }
        }

    }
}


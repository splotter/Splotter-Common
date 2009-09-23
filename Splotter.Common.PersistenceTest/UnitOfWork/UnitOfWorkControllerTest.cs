using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Splotter.Common.Persistence;
using Moq;
using Castle.Windsor;



namespace Splotter.Common.PersistenceTest
{
    [TestFixture]
    public class UnitOfWorkControllerTest
    {
        private IPersistenceManager _manager;
        private IUnitOfWork _unitOfWork;

        [TearDown]
        public void TearDown()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            var moqManager = new Mock<IPersistenceManager>();
            _manager = moqManager.Object;

            var moqUnitOfWork = new Mock<UnitOfWorkBase>();
            moqUnitOfWork.CallBase = true;

            moqManager.Expect(x => x.CreateUnitOfWork())
                .Returns(moqUnitOfWork.Object);
        }

        [Test]
        public void CanStartUnitOfWork()
        {
            _unitOfWork = UnitOfWorkController.Start(_manager);
            Assert.IsNotNull(_unitOfWork);
        }

        [Test]
        public void CanStartUnitOfWorkUsingIoC()
        {
            var moqContainer = new Mock<IWindsorContainer>();
            moqContainer.Expect(x => x.Resolve<IPersistenceManager>())
                .Returns(_manager);

            IoC.Initialize(moqContainer.Object);

            _unitOfWork = UnitOfWorkController.Start();
            Assert.IsNotNull(_unitOfWork);
        }

        [Test]
        public void CanDisposeUnitOfWork()
        {
            _unitOfWork = UnitOfWorkController.Start(_manager);
            _unitOfWork.Dispose();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StartingUnitOfWorkIfAlreadyStartedThrowsInvalidOperationException()
        {
            UnitOfWorkController.Start(_manager);
            UnitOfWorkController.Start(_manager);
        }

        [Test]
        public void CanAccessCurrentUnitOfWork()
        {
            _unitOfWork = UnitOfWorkController.Start(_manager);
            IUnitOfWork currentUnitOfWork = UnitOfWorkController.Current;
            Assert.AreEqual(_unitOfWork, currentUnitOfWork);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AccessingCurrentUnitOfWorkIfNotStartedThrowsInvalidOperationException()
        {
            IUnitOfWork currentUnitOfWork = UnitOfWorkController.Current;
        }

        [Test]
        public void CanCheckIfUnitOfWorkIsStarted()
        {
            Assert.IsFalse(UnitOfWorkController.IsStarted);

            _unitOfWork = UnitOfWorkController.Start(_manager);
            Assert.IsTrue(UnitOfWorkController.IsStarted);
        }
    }
}

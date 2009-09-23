using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Moq;
using Splotter.Common.Persistence;

using System.Linq.Expressions;

namespace Splotter.Common.PersistenceTest.Repository
{
    [TestFixture]
    public class RepositoryTest
    {
        private Mock<IPersistenceManager> _moqPersistenceManager;
        private Mock<RepositoryFactoryBase> _moqRepositoryFactory;
        private Mock<UnitOfWorkBase> _moqUnitOfWork;
        
        private IPersistenceManager _persistenceManager;
        private IRepositoryFactory _repositoryFactory;
        private IUnitOfWork _unitOfWork;

        private void CreateMocks()
        {
            _moqPersistenceManager = new Mock<IPersistenceManager>();
            _persistenceManager = _moqPersistenceManager.Object;

            _moqRepositoryFactory = new Mock<RepositoryFactoryBase>(_persistenceManager);
            _repositoryFactory = _moqRepositoryFactory.Object;

            _moqUnitOfWork = new Mock<UnitOfWorkBase>();
            _moqUnitOfWork.CallBase = true;
            _unitOfWork = _moqUnitOfWork.Object;
        }

        private void SetMocksExpectations()
        {
            _moqPersistenceManager.Expect(x => x.RepositoryFactory)
                            .Returns(_repositoryFactory);

            _moqPersistenceManager.Expect(x => x.CreateUnitOfWork())
                .Returns(_unitOfWork);
           
            //Has to use an actual repository class (FakeRepository)
            //Moq will not be able to mock/stub an IRepository or RepositoryBase due to issue with C# runtime
            //https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=282829
            _moqRepositoryFactory.Expect(x => x.GetRepository<FakeEntity>())
                .Returns(new FakeRepository<FakeEntity>(_repositoryFactory));
        }

        [SetUp]
        public void Setup()
        {
            CreateMocks();
            SetMocksExpectations();
        }

        [Test]
        public void CanSave()
        {
            _moqUnitOfWork.Expect(x => x.Save(It.IsAny<FakeEntity>()))
                .AtMost(2);

            _moqUnitOfWork.Expect(x => x.Flush())
                .AtMost(1);

            IRepository<FakeEntity> repository = _repositoryFactory.GetRepository<FakeEntity>();

            using (IUnitOfWork unitOfWork = UnitOfWorkController.Start(_persistenceManager))
            {
                repository.Save(new FakeEntity() { ID = 1, Data = "one" });
                repository.Save(new FakeEntity() { ID = 2, Data = "two" });
                unitOfWork.Flush();
            }

        }
        
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SaveWithoutUnitOfWorkThrowsInvalidOperation()
        {
            IRepository<FakeEntity> repository = _repositoryFactory.GetRepository<FakeEntity>();
            repository.Save(new FakeEntity() { ID = 1, Data = "one" });
        }

        [Test]
        public void CanDelete()
        {
            _moqUnitOfWork.Expect(x => x.Delete(It.IsAny<FakeEntity>()))
                .AtMost(1);

            _moqUnitOfWork.Expect(x => x.Flush())
                .AtMost(1);

            IRepository<FakeEntity> repository = _repositoryFactory.GetRepository<FakeEntity>();
            using (IUnitOfWork unitOfWork = UnitOfWorkController.Start(_persistenceManager))
            {
                FakeEntity entity = new FakeEntity();
                repository.Delete(entity);
                unitOfWork.Flush();
            }
        }


        [Test]
        public void CanGetAll()
        {
            IQueryable<FakeEntity> fakeEntities = FakeEntity.CreateFakeEntities();
            _moqUnitOfWork.Expect(x => x.GetAll<FakeEntity>())
                .Returns(fakeEntities)
                .AtMostOnce();

            IRepository<FakeEntity> repository = _repositoryFactory.GetRepository<FakeEntity>();
            using (IUnitOfWork unitOfWork = UnitOfWorkController.Start(_persistenceManager))
            {
                IQueryable<FakeEntity> entities = repository.GetAll();
                Assert.AreEqual(fakeEntities, entities);
            }
        }

        [Test]
        public void CanGetAllBySpecification()
        {

            IQueryable<FakeEntity> fakeEntities = FakeEntity.CreateFakeEntities();
            _moqUnitOfWork.Expect(x => x.GetAll<FakeEntity>())
                .Returns(fakeEntities);

            IRepository<FakeEntity> repository = _repositoryFactory.GetRepository<FakeEntity>();
            using (IUnitOfWork unitOfWork = UnitOfWorkController.Start(_persistenceManager))
            {
                IQueryable<FakeEntity> entities = repository.GetAll<FakeEntityWithID1>();
                Assert.GreaterThan(entities.Count(), 0);
                foreach (FakeEntity entity in entities)
                {
                    Assert.AreEqual(1, entity.ID);
                }
            }

        }

        [Test]
        public void CanGetOneBySpecification()
        {
            IQueryable<FakeEntity> fakeEntities = FakeEntity.CreateFakeEntities();
            _moqUnitOfWork.Expect(x => x.GetAll<FakeEntity>())
                .Returns(fakeEntities);

            using (IUnitOfWork unitOfWork = UnitOfWorkController.Start(_persistenceManager))
            {
                IRepository<FakeEntity> repository = _repositoryFactory.GetRepository<FakeEntity>();
                FakeEntity entity = repository.GetOne<FakeEntityWithID1>();
                Assert.AreEqual(1, entity.ID);
            }
        }


        [Test]
        public void CanGeAllByParameterizedSpecification()
        {
            IQueryable<FakeEntity> fakeEntities = FakeEntity.CreateFakeEntities();
            _moqUnitOfWork.Expect(x => x.GetAll<FakeEntity>())
                .Returns(fakeEntities);

            IRepository<FakeEntity> repository = _repositoryFactory.GetRepository<FakeEntity>();
            using (IUnitOfWork unitOfWork = UnitOfWorkController.Start(_persistenceManager))
            {
                string data = "One";
                IQueryable<FakeEntity> entities = repository.GetAll<FakeEntityWithData, string>(data);
                Assert.GreaterThan(entities.Count(), 0);
                foreach (FakeEntity entity in entities)
                {
                    Assert.AreEqual(data, entity.Data);
                }
            }
            
        }

        [Test]
        public void CanGetAllByLambdaExpression()
        {
            IQueryable<FakeEntity> fakeEntities = FakeEntity.CreateFakeEntities();
            _moqUnitOfWork.Expect(x => x.GetAll<FakeEntity>())
                .Returns(fakeEntities);

            Expression<Func<FakeEntity, bool>> predicate = x => x.ID == 1;

            IRepository<FakeEntity> repository = _repositoryFactory.GetRepository<FakeEntity>();
            using (IUnitOfWork unitOfWork = UnitOfWorkController.Start(_persistenceManager))
            {
                IQueryable<FakeEntity> entities = repository.GetAll(predicate);
                Assert.GreaterThan(entities.Count(), 0);
                foreach (FakeEntity entity in entities)
                {
                    Assert.AreEqual(1, entity.ID);
                }
            }
        }


        [Test]
        public void CanGetOneByLambdaExpression()
        {
            IQueryable<FakeEntity> fakeEntities = FakeEntity.CreateFakeEntities();
            _moqUnitOfWork.Expect(x => x.GetAll<FakeEntity>())
                .Returns(fakeEntities);

            Expression<Func<FakeEntity, bool>> predicate = x => x.ID == 1;
            IRepository<FakeEntity> repository = _repositoryFactory.GetRepository<FakeEntity>();
            using (IUnitOfWork unitOfWork = UnitOfWorkController.Start(_persistenceManager))
            {
                FakeEntity entity = repository.GetOne(predicate);
                Assert.AreEqual(1, entity.ID);
            }
        }

    }
}

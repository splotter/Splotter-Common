using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Moq;
using Splotter.Common.Persistence;

namespace Splotter.Common.PersistenceTest.Repository
{
    [TestFixture]
    public class RepositoryFactoryTest
    {
        [Test]
        public void CanCreateRepository()
        {
            IRepositoryFactory repositoryFactory = new Mock<IRepositoryFactory>().Object;
            IRepository<FakeEntity> repository = repositoryFactory.GetRepository<FakeEntity>();
        }
    }
}

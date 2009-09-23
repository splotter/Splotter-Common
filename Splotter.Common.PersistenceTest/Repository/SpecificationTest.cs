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
    public class SpecificationTest
    {


        [Test]
        public void CanQueryBySpecification()
        {
            const string DATA = "One";

            var moqSpecs = new Mock<SpecificationBase<FakeEntity>>();
            moqSpecs.ExpectGet(x => x.Predicate)
                .Returns(x => x.Data == DATA);

            SpecificationBase<FakeEntity> specs = moqSpecs.Object;

            IQueryable<FakeEntity> entities = FakeEntity.CreateFakeEntities();
            
            IQueryable<FakeEntity> entitiesWithDataOne = specs.Query(entities);

            Assert.GreaterThan(entitiesWithDataOne.Count(), 0);

            foreach (FakeEntity entity in entitiesWithDataOne)
            {
                Assert.AreEqual(DATA, entity.Data);
            }
        }   

    }
}

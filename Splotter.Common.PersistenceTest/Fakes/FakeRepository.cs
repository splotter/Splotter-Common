using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Splotter.Common.Persistence;

namespace Splotter.Common.PersistenceTest
{
    //Has to use an actual repository class (FakeRepository)
    //Moq will not be able to mock/stub an IRepository or RepositoryBase due to issue with C# runtime
    //https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=282829
    public class FakeRepository<T> : Splotter.Common.Persistence.RepositoryBase<T>
        where T : class, new()
    {
        public FakeRepository(IRepositoryFactory repositoryFactory)
            : base(repositoryFactory)
        {
        }
    }
}

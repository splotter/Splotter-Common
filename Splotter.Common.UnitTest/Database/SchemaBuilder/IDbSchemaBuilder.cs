
namespace Splotter.Common.UnitTest.Database
{
    public interface IDbSchemaBuilder
    {
        void Execute();
        string ConnectionString { get; set; }
    }
}

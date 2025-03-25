using DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace DeveloperEvaluation.Unit
{
    internal static class BaseTestHelpers
    {
        internal static DefaultContext GetCreateDefaultContextInstance()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DefaultContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            return new DefaultContext(dbContextOptions);
        }
    }
}
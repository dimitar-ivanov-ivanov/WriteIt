namespace WriteIt.Tests.Mocks
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using WriteIt.Data;

    public class MockDbContext
    {
        public static WriteItContext GetContext()
        {
            var options = new DbContextOptionsBuilder<WriteItContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new WriteItContext(options);
        }
    }
}
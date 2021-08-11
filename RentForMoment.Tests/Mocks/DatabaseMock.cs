namespace RentForMoment.Tests.Mocks
{
    using Microsoft.EntityFrameworkCore;
    using RentForMoment.Data;
    using System;

    public static class DatabaseMock
    {

        public static RentForMomentDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<RentForMomentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new RentForMomentDbContext(dbContextOptions);
            }
        }

    }
}
